using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Changed_game.Scripts.Level
{   
    public class MapTile
    {
        public int X;
        public int Y;
        public TileType type;
    }

    public enum TileType
    {
        Empty,
        Wall,
        Food,
        Enemy
    }
}
