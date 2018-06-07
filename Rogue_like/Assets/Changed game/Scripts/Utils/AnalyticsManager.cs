using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Analytics;

public class AnalyticsManager : MonoBehaviour
{
    public static AnalyticsManager instance;
    public int day = 0;
    public int wallCount = 0;
    public int enemyCount = 0;
    public int foodCount = 0;
    public bool isCharacterRed = false;
    public bool isShop = false;
    public int startFood = 0;
    public int endFood = 0;
    public int pickUpCount = 0;
    public int pickupsCostSpent = 0;


    void Awake()
    {
		if(instance == null)
		{
			instance = this;
		}
		else if(instance != this)
		{
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);
    }

    public void getVariablesAtStart()
    {
        startFood = Player.instance.food;
        day = GameManager.instance.level;
        isCharacterRed = !CharacterSelection.characterSelected;
    }

    public void getVariablesAtEnd()
    {
        endFood = Player.instance.food;
    }

    public bool SendAnalytics()
    {
		AnalyticsResult result = AnalyticsEvent.Custom("played level", new Dictionary<string, object>
			{ 
				{"Days survived",day},
				{"Walls in level",wallCount},
				{"enemies in level",enemyCount},
				{"amount of food in level",foodCount},
				{"Is the character red?",isCharacterRed},
				{"Is the room a shop?",isShop},
				{"Starting amount of food of level",startFood},
				{"Ending amount of food of level",endFood},
				{"Amount of pickups in level",pickUpCount},
				{"Amount of food spent on pickup in shop",pickupsCostSpent}
			});
		if(result == AnalyticsResult.Ok){
			return true;
		} else {
			return false;
		}
    }
}
