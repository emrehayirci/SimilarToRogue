using Assets.Changed_game.Scripts.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Changed_game.Scripts.Actor
{
    class Zombie: MovingObject, Actor
    {
        public int playerDamage;
        public AudioClip enemyAttack1;
        public AudioClip enemyAttack2;

        private Animator animator;
        private Transform target;
        private bool skipMove;
        private bool lastDirectionRight;
        private SpriteRenderer spriteRenderer;

        public void Damage(GameObject target)
        {
            
        }

        public void Move()
        {
            if (!skipMove)
            {
                Stack<Vector2> moveStack = PathFinding.Calculate(transform.position, target.position);

                if (moveStack.Count != 0)
                {
                    RaycastHit2D hit;
                    Vector2 nextMove = moveStack.Pop();

                    Debug.Log("Next");
                    Debug.Log(nextMove);

                    int xDir = (int)(nextMove.x - transform.position.x);
                    int yDir = (int)(nextMove.y - transform.position.y);

                    lastDirectionRight = (xDir > 0 ? true : false);
                    spriteRenderer.flipX = lastDirectionRight;

                    //Tries to move to location, Moves if it can move
                    if (!Move(xDir, yDir, out hit))
                    {
                        //if colides with something determine if its wall or player
                        Component hitComponent = hit.transform.GetComponent<Wall>();
                        if (hitComponent == null)
                        {
                            hitComponent = hit.transform.GetComponent<Player>();
                        }

                        //If determined as wall or Player
                        if (hitComponent != null)
                        {
                            OnCantMove(hitComponent);
                        }
                    }

                    //for next turn
                    skipMove = true;
                }
            }
            else
            {
                skipMove = false;
            }
        }

        protected override void OnCantMove<T>(T component)
        {
            if (component.gameObject.name == "Player")
            {
                Player hitPlayer = component as Player;
                hitPlayer.LoseFood(playerDamage);
                SoundManager.instance.RandomizeSFX(enemyAttack1, enemyAttack2);
            }
            else
            {
                Wall hitWall = component as Wall;
                hitWall.DamageWall(playerDamage / 10);
                SoundManager.instance.RandomizeSFX(enemyAttack1);
            }

            animator.SetTrigger("enemyAttack");
        }
    }
}
