using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipSprites : MonoBehaviour {


    public SpriteRenderer mySpriteRenderer;
    public static FlipSprites instance; 

    public void Awake()
    {
        instance = this;

        
    }

    // Use this for initialization
    void Start ()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        FlipTheSprite(); 
    }

    public void FlipTheSprite()
    {
        if (mySpriteRenderer != null)
        {
            //Debug.Log("Flipping the sprite"); 
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                // flip the sprite
                mySpriteRenderer.flipX = true;
            }

            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                mySpriteRenderer.flipX = false;
            }
        }
    }
}
