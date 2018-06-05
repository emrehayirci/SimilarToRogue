using Assets.Changed_game.Scripts.Inventory;
using Assets.Changed_game.Scripts.Level;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public float levelStartDelay = 0f;
    public static GameManager instance = null;
    public float turnDelay = .1f;
    public NewBoardManager boardScript;

    public int playerFoodPoints = 100;
    [HideInInspector] public bool playerTurn = true;

    public Text levelText;
    public GameObject levelImage;
    private int level = 1;
    private List<PickupItem> itemsSaved = null;
	public List<Actor> enemies;
    private bool enemiesMoving;
    private bool doingSetup;

    public bool characterChosenRed;
    public bool characterChosenGreen; 

	// Use this for initialization
	void Awake () {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }

        //characterChosenRed = CharacterRed.instance.isClickedRed;
        //aracterChosenGreen = CharacterGreen.instance.isClickedGreen; 

        DontDestroyOnLoad(gameObject);


        

		enemies = new List<Actor>();
        boardScript = GetComponent<NewBoardManager>();
        //  InitGame();
	}

    void InitGame()
    {
        doingSetup = true;

        levelImage = GameObject.Find("Level Image");
        levelText = GameObject.Find("LevelText").GetComponent<Text>();

        ShowLevelImageAfterLoad();
        Invoke("HideLevelImage",levelStartDelay);

        enemies.Clear();
        boardScript.SetupScene(level);
    }

    public void ShowLevelImageAfterLoad()
    {
        levelText.text = "Day " + level;
        levelImage.SetActive(true);
    }

    public void ShowLevelImageBeforeLoad(Text newlevelText, GameObject newLevelImage)
    {
        newlevelText.text = "Day " + (level + 1);
        newLevelImage.SetActive(true);
    }

    private void HideLevelImage()
    {
        levelImage.SetActive(false);
        doingSetup = false;
    }

    public void GameOver()
    {
        levelText.text = "After " + level + " days, you starved.";
        levelImage.SetActive(true);
        enabled = false;
    }

	// Update is called once per frame
	void Update () {
		if(playerTurn || enemiesMoving || doingSetup)
        {
            return;
        }
        StartCoroutine(MoveEnemies());
	}

	public void AddEnemyToList(Actor script)
    {
		enemies.Add(script);
    }

    IEnumerator MoveEnemies()
    {
        enemiesMoving = true;
        yield return new WaitForSeconds(turnDelay);
        if(enemies.Count == 0)
        {
            yield return new WaitForSeconds(turnDelay);
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].MoveEnemy();
			yield return new WaitForSeconds(enemies[i].GetMoveTime());
        }

        playerTurn = true;
        enemiesMoving = false;
    }
    
    public void SaveItems(List<PickupItem> items)
    {
        this.itemsSaved = items;
    }

    public List<PickupItem> GetSavedItems()
    {
        List<PickupItem> items = new List<PickupItem>();
        if(level != 1 && itemsSaved != null)
        {
            items = this.itemsSaved;
        }

        return items;
    }

    void OnLevelWasLoaded(int index)
    {
        level++;
        InitGame();
    }

    /*

    public void WhichCharacter()
    {
        if (characterChosenGreen == true)
            Debug.Log("Oui green");

        if (characterChosenRed == true)
            Debug.Log("Oui red"); 
    }
    */

    /*
    public void WhichCharacterRed()
    {
        Debug.Log("red character");

        SceneManager.LoadScene("Scene1"); 

        //isClickedRed = true;
    }

    public void WhichCharacterGreen()
    {
        Debug.Log("green charizard");

        SceneManager.LoadScene("Scene1");
    }
    */
}
