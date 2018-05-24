using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour {

    public float moveTime = 0.1f;
    public LayerMask blockingLayer;
    public const int MOVE_ATTEMPT_NO_HIT = 0;
    public const int MOVE_ATTEMPT_HIT = 1;

    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2D;
    private float inverseMoveTime;

    public float greenSpeed = 2; 

	
	protected virtual void Start ()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        inverseMoveTime = 1f / moveTime;
	}
	
    protected bool Move(int xDir, int yDir, out RaycastHit2D hit)
    {
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(xDir, yDir);

        boxCollider.enabled = false;
        hit = Physics2D.Linecast(start,end, blockingLayer);
        boxCollider.enabled = true;

        if(hit.transform == null)
        {
            StartCoroutine(SmoothMovement(end));
            return true;
        }
        return false;
    }

    protected IEnumerator SmoothMovement (Vector3 end)
    {
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        while (sqrRemainingDistance > float.Epsilon)
        //for green character
        {
            /*
            if (CharacterRed.instance.isClickedRed == true)
            {
                Debug.Log("alas red char"); 
                Vector3 newPosition = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);
                rb2D.MovePosition(newPosition);
                sqrRemainingDistance = (transform.position - end).sqrMagnitude;
                //yield return null;
            }
            else if (CharacterGreen.instance.isClickedGreen == true)
            {
            */
                //Debug.Log("alas green char"); 
                Vector3 newPosition = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);
                rb2D.MovePosition(newPosition);
                sqrRemainingDistance = (transform.position - end).sqrMagnitude;
                yield return null;


            //}

            //
        }
    }


    //if the character is green it should move faster
    protected virtual int AttemptMove <T> (int xDir, int yDir)
        where T : Component
    {
        RaycastHit2D hit;
        bool canMove = Move(xDir, yDir, out hit);

        if (CharacterGreen.instance.isClickedGreen == true && CharacterRed.instance.isClickedRed == false)
        {
            Move(xDir * 2, yDir * 2, out hit); 
        }

        if (hit.transform == null)
            return MOVE_ATTEMPT_NO_HIT;

        T hitComponent = hit.transform.GetComponent<T>();

        if (!canMove && hitComponent != null)
        {
            OnCantMove(hitComponent);
            return MOVE_ATTEMPT_HIT;
        }

        return MOVE_ATTEMPT_NO_HIT;
    }

    protected abstract void OnCantMove<T>(T component)
        where T : Component;
}
