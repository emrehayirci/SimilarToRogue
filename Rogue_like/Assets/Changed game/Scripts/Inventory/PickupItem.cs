using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Changed_game.Scripts.Inventory
{
    public class PickupItem
    {
        public static int lastItem = 0;

        public int id;
        public int effect;
        public int PickUpTypeId;
        public int Durability;
        public PickupType type;
        public Sprite itemPicture;


        //Used to determine place on the map
        public Vector2 location;


        public PickupItem(PickupType type, int pickupId, Sprite itemPicture, int maxDurability, int effect)
        {
            lastItem++;
            id = lastItem;
            PickUpTypeId = pickupId;
            this.type = type;
            Durability = maxDurability;
            this.itemPicture = itemPicture;
            this.effect = effect;
        }
    }
}
