using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Changed_game.Scripts.Actor
{
    public interface Actor
    {
        void Move();
        void Damage(GameObject player);
    }
}
