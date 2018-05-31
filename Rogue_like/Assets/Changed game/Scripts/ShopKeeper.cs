using Assets.Changed_game.Scripts.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : MonoBehaviour {

    private GameObject[] randomItems;
    public Dictionary<int, float> priceList = new Dictionary<int, float>();

    int randomAmountOfItems;
	int randomCost;
	public int maxCost;
	public int minCost;


	// Use this for initialization
	void Start () {
        randomItems = GameManager.instance.boardScript.randomCollectable;

		randomAmountOfItems = (int)Random.Range (1, 3);

		for (int i = 0; i < randomAmountOfItems; i++) {

			randomCost = (int)Random.Range (minCost,maxCost);
            GameObject randomObject = randomItems[Random.Range(0, randomItems.Length)];
            Vector3 position = new Vector3(transform.position.x - 1 + i, transform.position.y - 2, transform.position.z);

            GameObject newItem = Instantiate(randomObject, position, Quaternion.identity);
            newItem.GetComponent<PickUpBehavior>().owner = this;

            //Add item to priceList
            priceList.Add(newItem.GetComponent<PickUpBehavior>().objectDetails.id, randomCost);

            //in item: add the cost, and call Player.LoseFood(cost) when Player walks over it


        }
	}

    public void SellItem(PickupItem item)
    {
        //Sellin Item
        Player.instance.LoseFood((int)priceList[item.id]);
    }
}
