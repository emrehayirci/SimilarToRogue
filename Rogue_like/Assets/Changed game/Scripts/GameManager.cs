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
	private List<Actor> enemies;
    private bool enemiesMoving;
    private bool doingSetup;

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

        DontDestroyOnLoad(gameObject);

        

		enemies = new List<Actor>();
        boardScript = GetComponent<NewBoardManager>();
        InitGame();
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
    
    void OnLevelWasLoaded(int index)
    {
        level++;
        InitGame();
    }
}
