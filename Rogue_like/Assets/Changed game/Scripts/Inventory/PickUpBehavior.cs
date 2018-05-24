using Assets.Changed_game.Scripts.Inventory;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBehavior : MonoBehaviour {

    public PickupItem objectDetails;
    public PickupType itemType;
    public int maxDurability;
    public int PickupTypeId = 0;

	// Use this for initialization
	void Start () {
        objectDetails = new PickupItem(itemType, PickupTypeId, this.gameObject.GetComponent<SpriteRenderer>().sprite, maxDurability);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
