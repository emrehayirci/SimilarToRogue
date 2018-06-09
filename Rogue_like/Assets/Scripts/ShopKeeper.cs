using Assets.Changed_game.Scripts.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : MonoBehaviour {

    public GameObject[] randomItems;
    public Dictionary<int, int> priceList;

	public int maxCost = 10;
	public int minCost = 3;


	// Use this for initialization
	void Start () {
        priceList = new Dictionary<int, int>();

        randomItems = GameManager.instance.boardScript.randomCollectable;

		int randomAmountOfItems = (int)Random.Range (1, 3);

		for (int i = 0; i < randomAmountOfItems; i++) {

			int randomCost = (int)Random.Range (minCost,maxCost);
            GameObject randomObject = randomItems[Random.Range(0, randomItems.Length)];
            Vector3 position = new Vector3(transform.position.x - 1 + i, transform.position.y - 2, transform.position.z);

            GameObject newItem = Instantiate(randomObject, position, Quaternion.identity);
            newItem.GetComponent<PickUpBehavior>().owner = this;

            //Self assignment used because other gameobject start 
            //method haven't started yet! IDs not consistent right now. 
            //So we force to give one id beforehand
            newItem.GetComponent<PickUpBehavior>().id = i;

            //Add item to priceList
            priceList.Add(newItem.GetComponent<PickUpBehavior>().id, randomCost);
            
            //in item: add the cost, and call Player.LoseFood(cost) when Player walks over it


        }
	}

    public void SellItem(PickupItem item)
    {
        //Sellin Item
        Player.instance.LoseFood(priceList[item.id]);
        AnalyticsManager.instance.pickupsCostSpent += priceList[item.id];
    }
}
