using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Changed_game.Scripts.Actor
{
    class ArcherZombie : MovingObject, Actor
    {
        public void Damage(GameObject player)
        {
            throw new NotImplementedException();
        }

        public void Move()
        {
            throw new NotImplementedException();
        }

        protected override void OnCantMove<T>(T component)
        {
            throw new NotImplementedException();
        }
    }
}
