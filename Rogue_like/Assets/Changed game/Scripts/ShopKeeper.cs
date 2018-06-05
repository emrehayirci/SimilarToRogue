using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : MonoBehaviour {

	public GameObject[] saleItems = {};
	int randomAmountOfItems;
	int randomCost;
	public int maxCost;
	public int minCost;



	// Use this for initialization
	void Start () {



		randomAmountOfItems = (int)Random.Range (1, 3);

		for (int i = 0; i < randomAmountOfItems; i++) {

			randomCost = (int)Random.Range (minCost,maxCost);
			//random Item from total list of existing items
			//drop item
			if(i == 0)
			{
				//drop item right from shopKeeper
			}
			else if(i == 1)
			{
				//drop below shopKeeper
			}
			else if(i == 2)
			{
				//drop left of ShopKeeper
			}
			//give the item a cost
			//give item an index
			//in item: add bool for shopItem
			//in item: add the cost, and call Player.LoseFood(cost) when Player walks over it
		}
	}
}
