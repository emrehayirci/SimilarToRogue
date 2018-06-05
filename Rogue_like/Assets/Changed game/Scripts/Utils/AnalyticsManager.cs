using UnityEngine;
using System.Collections;

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


    void Start()
    {
        instance = this;
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

    public void SendAnalytics()
    {

    }
}
