using Assets.Changed_game.Scripts.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryHudScript : MonoBehaviour {

    public static InventoryHudScript instance;

    public Image ArmorImage;
    public Image SwordImage;
    public Image SpellImage;

    public Sprite placeholderSprite;

    public void Awake()
    {
        instance = this;
    }


    public void UpdateItems(List<PickupItem> items)
    {
        ArmorImage.sprite = placeholderSprite;
        SwordImage.sprite = placeholderSprite;
        SpellImage.sprite = placeholderSprite;

        foreach(var item in items)
        {
            if(item.type == PickupType.Armor){
                ArmorImage.sprite = item.itemPicture;
            }
            if (item.type == PickupType.Weapon)
            {
                SwordImage.sprite = item.itemPicture;
            }
            if (item.type == PickupType.Spell)
            {
                SpellImage.sprite = item.itemPicture;
            }
        }
    }
}
