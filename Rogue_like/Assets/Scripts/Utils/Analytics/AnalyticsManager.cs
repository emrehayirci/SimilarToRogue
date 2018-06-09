using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Analytics;
using GameAnalyticsSDK;
using GameAnalyticsSDK.Events;
using GameAnalyticsSDK.Wrapper;

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

        ////Required check for firebase
        //Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
        //    var dependencyStatus = task.Result;
        //    if (dependencyStatus == Firebase.DependencyStatus.Available)
        //    {
        //        // Set a flag here indiciating that Firebase is ready to use by your
        //        // application.
        //    }
        //    else
        //    {
        //        UnityEngine.Debug.LogError(System.String.Format(
        //          "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
        //        // Firebase Unity SDK is not safe to use here.
        //    }
        //});

        GA_Wrapper.Initialize("57f8c0071a40aef6826e3dc591e079b3", "de8f5f2a80351fdb5e140beea096085f3183d6d7");

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
            Debug.LogError("Analytics Failed");
			return false;
		}

        //SendFlurry();
        SendGameAnalytics();
    }

    //public bool SendFlurry()
    //{
    //    Flurry.Instance.StartSession("9T5TMQKY3DFFVPNJNNBR", "4CM72DV54GJYJMM2HTFD");
    //    Flurry.Instance.LogUserID(CharacterSelection.sessionId);
    //    Flurry.Instance.LogEvent("event", new Dictionary<string, string>
    //        {
    //            {"Days survived",day.ToString()},
    //            {"Walls in level",wallCount.ToString()},
    //            {"enemies in level",enemyCount.ToString()},
    //            {"amount of food in level",foodCount.ToString()},
    //            {"Is the character red?",isCharacterRed.ToString()},
    //            {"Is the room a shop?",isShop.ToString()},
    //            {"Starting amount of food of level",startFood.ToString()},
    //            {"Ending amount of food of level",endFood.ToString()},
    //            {"Amount of pickups in level",pickUpCount.ToString()},
    //            {"Amount of food spent on pickup in shop",pickupsCostSpent.ToString()}
    //        });
    //    return false;
    //}

    //Using GameAnalyticsSDK
    public bool SendGameAnalytics()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete,
            wallCount.ToString(),
            enemyCount.ToString(),
            day);

        GameAnalytics.NewDesignEvent("wallCount", wallCount);
        GameAnalytics.NewDesignEvent("foodCountStart", startFood);
        GameAnalytics.NewDesignEvent("foodCountEnd", endFood);
        GameAnalytics.NewDesignEvent("pickUpCount", pickUpCount);
        GameAnalytics.NewDesignEvent("wallCount", wallCount);
        GameAnalytics.NewDesignEvent("wallCount", wallCount);
        GameAnalytics.NewDesignEvent("wallCount", wallCount);
        return false;
    }
}
