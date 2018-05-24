using Assets.Changed_game.Scripts.Level;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raider : MovingObject, Actor {

    public int playerDamage;
    public AudioClip enemyAttack1;
    public AudioClip enemyAttack2;
	public int range;
	public int health = 2;
    
    private Animator raiderAnimator;
    private Transform target;
    private bool skipMove;
	private bool raiderLastDirectionRight;
	private SpriteRenderer raiderRenderer; 



	protected override void Start () {

        GameManager.instance.AddEnemyToList(this);
        raiderRenderer = GetComponent<SpriteRenderer>();
		raiderAnimator = GetComponent<Animator>();

		target = GameObject.FindGameObjectWithTag("Enemy").transform;
		if(target.transform == null || target.transform == this.transform)
		{
			target = GameObject.FindGameObjectWithTag ("Player").transform;
		}

        base.Start();
	}

    //pathfinding
    public void MoveEnemy()
    {
        if (!skipMove)
        {
            Stack<Vector2> moveStack = PathFinding.Calculate(transform.position, target.position);

            if (moveStack.Count != 0)
            {
				RaycastHit2D hit;
				if(moveStack.Count <= range)
				{
					if(target.tag == "Player"){
						OnCantMove (target.transform.GetComponent<Player>());
					//attack when in range
					} else if(target.tag == "Enemy"){
						OnCantMove (target.transform.GetComponent<Enemy>());
					}
				}
            	Vector2 nextMove = moveStack.Pop();

                Debug.Log("Next");
                //Debug.Log(nextMove);

                int xDir = (int)(nextMove.x - transform.position.x);
                int yDir = (int)(nextMove.y - transform.position.y);

                raiderLastDirectionRight = (xDir > 0 ? true : false);
                raiderRenderer.flipX = raiderLastDirectionRight;

                //Tries to move to location, Moves if it can move
                if (!Move(xDir, yDir, out hit)){
                    //if colides with something determine if its wall or player
                    Component hitComponent = hit.transform.GetComponent<Wall>();
                    if(hitComponent == null)
                    {
                        hitComponent = hit.transform.GetComponent<Player>();
                    }

                    //If determined as wall or Player
                    if (hitComponent != null)
                    {
                        OnCantMove(hitComponent);
                    }
                }
                
                //for next turn
                skipMove = true;
            }
        }
        else
        {
            skipMove = false;
        }
        
    }

    protected override void OnCantMove<T>(T component)
    {  
		if(component.gameObject.name == "Player")
        {
            Player hitPlayer = component as Player;
            hitPlayer.LoseFood(playerDamage);
            SoundManager.instance.RandomizeSFX(enemyAttack1, enemyAttack2);
        }
		else if(component.gameObject.tag == "Enemy"){
			//attack enemy
			Enemy hitEnemy = component as Enemy;
			hitEnemy.LoseHealth (playerDamage / 10);
			SoundManager.instance.RandomizeSFX (enemyAttack1,enemyAttack2);
		}
        else
        {
            Wall hitWall = component as Wall;
            hitWall.DamageWall(playerDamage / 10);
            SoundManager.instance.RandomizeSFX(enemyAttack1);
        }

        raiderAnimator.SetTrigger("enemyAttack");
		if(target.gameObject.tag == "Enemy" && target.gameObject.GetComponent<Enemy>().health <= 0)
		{
			target = GameObject.FindGameObjectWithTag ("Enemy").transform;
			if(target.transform == null || target.transform == this.transform)
			{
				target = GameObject.FindGameObjectWithTag ("Player").transform;
			}
		}
    }


	public void LoseHealth(int loss)
	{
		raiderAnimator.SetTrigger("");
		health -= loss;
		if(health <= 0)
		{
			this.gameObject.SetActive(false);
		}
	}

	public float GetMoveTime()
	{
		return base.raiderMoveTime;
	}
}
