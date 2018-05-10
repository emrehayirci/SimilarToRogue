using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MovingObject {

    public int playerDamage;
    public AudioClip enemyAttack1;
    public AudioClip enemyAttack2;
    
    private Animator animator;
    private Transform target;
    private bool skipMove;
    
	protected override void Start () {
        GameManager.instance.AddEnemyToList(this);
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        base.Start();
	}

    protected override int AttemptMove<T>(int xDir, int yDir)
    {
        if (skipMove)
        {
            //skipMove = false;
            return MOVE_ATTEMPT_NO_HIT;
        }
        else
        {
            //skipMove = true;
            return base.AttemptMove<T>(xDir, yDir);
        }
    }

    //pathfinding
    public void MoveEnemy()
    {
        int xDir = 0;
        int yDir = 0;

        if(Mathf.Abs(target.position.x - transform.position.x) < float.Epsilon)
        {
            yDir = target.position.y > transform.position.y ? 1 : -1;
        }
        else
        {
            xDir = target.position.x > transform.position.x ? 1 : -1;
        }
        
        int hitCode = AttemptMove<Player>(xDir, yDir);

        if (hitCode == MOVE_ATTEMPT_HIT)
        {
            skipMove = true;
            return;
        }

        else if (hitCode == MOVE_ATTEMPT_NO_HIT)
        {
            AttemptMove<Wall>(xDir, yDir);
        }
        skipMove = true;
    }

    protected override void OnCantMove<T>(T component)
    {  
        if(component.gameObject.name == "Player")
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
