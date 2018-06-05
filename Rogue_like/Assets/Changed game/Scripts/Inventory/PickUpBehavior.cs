using Assets.Changed_game.Scripts.Inventory;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBehavior : MonoBehaviour {

    public PickupItem objectDetails;
    public PickupType itemType;
    public int maxDurability;
    public int effect;
    public int PickupTypeId = 0;
    public int id = -1;

    public ShopKeeper owner = null;

	// Use this for initialization
	void Start () {
        objectDetails = new PickupItem(itemType, PickupTypeId, this.gameObject.GetComponent<SpriteRenderer>().sprite, maxDurability, effect);
        if(id != -1)
        {
            objectDetails.id = id;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            //Checks if shopkeper selling item
            if(owner != null)
            {
                owner.SellItem(objectDetails);
            }
        }
    }
}
