using Assets.Changed_game.Scripts.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {


    public List<PickupItem> items;
    public int itemCount = 0;
    public GameObject hud;

	// Use this for initialization
	void Start () {
        items = new List<PickupItem>();
	}
	


    /*
     * Returns true if Item picked
     */ 
    public void AddItem(PickupItem groundItem)
    {
        //Items must be unique by type dropping items in same type
        foreach(var item in items)
        {
            if(item.type == groundItem.type)
            {
                DropItem(item);
                break;
            }
        }
        items.Add(groundItem);
        itemCount++;
        //show it in screen
        DisplayItems();
    }

    public bool DropItem(PickupItem itemToDrop)
    {
        //Level Manager add item to ground
        GameManager.instance.boardScript.AddPickupItem(itemToDrop, transform.position);
        itemCount--;
        return items.Remove(itemToDrop);    
    }

    void DisplayItems()
    {
        InventoryHudScript.instance.UpdateItems(items);
    }

}
