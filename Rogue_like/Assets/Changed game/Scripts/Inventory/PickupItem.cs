using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Changed_game.Scripts.Inventory
{
    public class PickupItem
    {
        public static int lastItem;

        public int id;
        public PickupType type;
        public Sprite itemPicture;


        //Used to determine place on the map
        public Vector2 location;


        public PickupItem(PickupType type, Sprite itemPicture)
        {
            lastItem++;
            id = lastItem;

            this.type = type;
            this.itemPicture = itemPicture;

        }
    }
}
