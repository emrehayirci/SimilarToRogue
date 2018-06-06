using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MovingObject {

    public static Player instance;

    public Sprite mySprite; 

    public int wallDamage = 1;
    public int pointsPerFood = 10;
    public int pointsPerSoda = 20;
    public float restartLevelDelay = 0f;
    public Text foodText;

    public AudioClip moveSound1;
    public AudioClip moveSound2;
    public AudioClip eatSound1;
    public AudioClip eatSound2;
    public AudioClip drinkSound1;
    public AudioClip drinkSound2;
    public AudioClip gameOverSound;

    private Animator animator;
    public int food;

    private SpriteRenderer mySpriteRenderer;
    public Animator anims;
    public RuntimeAnimatorController anim1;

    private void Awake()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        this.GetComponent<SpriteRenderer>().sprite = mySprite;
        instance = this; 
    }

    // Use this for initialization
    protected override void Start () {
        animator = GetComponent<Animator>();

        food = GameManager.instance.playerFoodPoints;

        foodText.text = "Food: " + food;

        //if the character chosen was red, it should be stronger
        if (CharacterSelection.isClickedRed == true)
        {
            wallDamage = 3;
            //display teh red character controller; 
            Animator animator = transform.gameObject.GetComponent<Animator>();
            this.GetComponent<Animator>().runtimeAnimatorController = anim1 as RuntimeAnimatorController;

            if (Input.GetKeyDown((KeyCode.RightArrow)))
            {

                mySpriteRenderer.flipX = true;
            }
        }
		AnalyticsManager.instance.getVariablesAtStart ();
		AnalyticsManager.instance.SendAnalytics ();
        base.Start();
	}

    private void OnDisable()
    {
        Debug.Log("OnDisable player "+food);
        GameManager.instance.playerFoodPoints = food;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.playerTurn)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            RangedAttack();
            return;
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            MeleeAtack();
            return;
        }

        int horizontal = 0;
        int vertical = 0;

        horizontal = (int)Input.GetAxisRaw("Horizontal");
        vertical = (int)Input.GetAxisRaw("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
            AttemptMove<Wall>(horizontal, vertical);
        }

        //prevents diagonal movement
        if (horizontal != 0)
        {
            vertical = 0;
        }

        if (mySpriteRenderer != null || CharacterSelection.isClickedRed == false)
        {
            if (Input.GetKeyDown((KeyCode.LeftArrow)))
            {
                // flip the sprite
                mySpriteRenderer.flipX = true;
            }
            
            if (Input.GetKeyDown((KeyCode.RightArrow)))
            {
                mySpriteRenderer.flipX = false; 
            }
        }
        
    }

    protected override int AttemptMove<T>(int xDir, int yDir)
    {
        food--;
        foodText.text = "Food: " + food;

        base.AttemptMove<T>(xDir,yDir);

        RaycastHit2D hit;
        if(Move(xDir,yDir, out hit))
        {
            SoundManager.instance.RandomizeSFX(moveSound1,moveSound2);
        }

        CheckIfGameOver();

        GameManager.instance.playerTurn = false;
        return MOVE_ATTEMPT_NO_HIT;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Exit")
        {
            Debug.Log(food);
            GameManager.instance.playerFoodPoints = food;
            GameManager.instance.SaveItems(GetComponent<Inventory>().items);

            Invoke("Restart",restartLevelDelay);
            enabled = false;
        }
        else if(other.tag == "Food")
        {
            food += pointsPerFood;
            foodText.text = "+ " + pointsPerFood + " Food: " + food;
            SoundManager.instance.RandomizeSFX(eatSound1, eatSound2);
            other.gameObject.SetActive(false);
        }
        else if(other.tag == "Soda")
        {
            food += pointsPerSoda;
            foodText.text = "+ " + pointsPerSoda + " Food: " + food;
            SoundManager.instance.RandomizeSFX(drinkSound1, drinkSound2);
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "PickUp")
        {
            this.GetComponent<Inventory>().AddItem(other.GetComponent<PickUpBehavior>().objectDetails);
            // remove object from map

            other.gameObject.SetActive(false);

        }
    }

    protected override void OnCantMove<T>(T component)
    {
        Wall hitWall = component as Wall;
        hitWall.DamageWall(wallDamage);
        if (CharacterSelection.isClickedRed == true)
            animator.SetTrigger("Zombie1Attack");
        else
            animator.SetTrigger("playerChop");
        //animator.SetTrigger("playerChop");
    }

    private void Restart()
    {
        Debug.Log("Restart");
		AnalyticsManager.instance.getVariablesAtEnd ();
		AnalyticsManager.instance.SendAnalytics ();
        //turn on level image and increase the level in here before loading the next level
        GameManager.instance.ShowLevelImageBeforeLoad(GameManager.instance.levelText, GameManager.instance.levelImage);
        SceneManager.LoadScene("Scene1");
        
    }

    public void LoseFood(int loss)
    {
        if (CharacterSelection.isClickedRed == true)
            animator.SetTrigger("Zombie1Attack");
        else
            animator.SetTrigger("playerHit");

        int armorBonus = GetComponent<Inventory>().UseItem(Assets.Changed_game.Scripts.Inventory.PickupType.Armor) + 1;

        food -= loss / armorBonus;
        
        foodText.text = "- " + (loss / armorBonus)+ " Food: " + food;
        CheckIfGameOver();
    }

    private void CheckIfGameOver()
    {
        if(food <= 0)
        {
			AnalyticsManager.instance.getVariablesAtEnd ();
			AnalyticsManager.instance.SendAnalytics ();
            SoundManager.instance.PlaySingle(gameOverSound);
            SoundManager.instance.musicSource.Stop();
            GameManager.instance.GameOver();
        }
    }

    private void MeleeAtack()
    {
        GameObject target;
        float distance;

        if (GetTargetObjects(out target, out distance))
        {
            if(distance <= 1.5)
                target.GetComponent<Actor>().LoseHealth(1 + GetComponent<Inventory>().UseItem(Assets.Changed_game.Scripts.Inventory.PickupType.Weapon));

            if (CharacterSelection.isClickedRed == true)
                animator.SetTrigger("Zombie1Attack");
            else
                animator.SetTrigger("playerChop");
        }

        //Calling to skip turn;
        AttemptMove<Wall>(0, 0);
    }

    private void RangedAttack()
    {
        GameObject target;
        float distance;

        if (GetTargetObjects(out target, out distance))
        {
            target.GetComponent<Actor>().LoseHealth(1 + GetComponent<Inventory>().UseItem(Assets.Changed_game.Scripts.Inventory.PickupType.Spell));

            if (CharacterSelection.isClickedRed == true)
                animator.SetTrigger("Zombie1Attack");
            else
                animator.SetTrigger("playerChop");
        }

        //Calling to skip turn;
        AttemptMove<Wall>(0, 0);
    }


    //Returns closest object to atack
    private bool GetTargetObjects(out GameObject Target, out float range)
    {
        GameObject target = null;

        float minDistance = float.MaxValue;
        foreach(var enemy in GameManager.instance.enemies)
        {
            Vector2 targetLocation = enemy.getObject().transform.position;
            float distance = Vector2.Distance(targetLocation, transform.position);
            if(distance < minDistance)
            {
                target = enemy.getObject();
                minDistance = distance;
            }
        }

        Target = target;
        range = minDistance;
        return (target != null);
    }
}
