using Assets.Changed_game.Scripts.Inventory;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBehavior : MonoBehaviour {

    public PickupItem objectDetails;
    public PickupType itemType = PickupType.Random;

	// Use this for initialization
	void Start () {
        objectDetails = new PickupItem(itemType, this.gameObject.GetComponent<SpriteRenderer>().sprite);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
