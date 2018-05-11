using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipSprites : MonoBehaviour {


    public SpriteRenderer mySpriteRenderer;
    public static FlipSprites instance; 

    public void Awake()
    {
        instance = this;

        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Use this for initialization
    void Start ()
    {
		
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
            if (Input.GetKeyDown((KeyCode.LeftArrow)))
            {
                // flip the sprite
                mySpriteRenderer.flipX = true;
            }

            if (Input.GetKeyDown((KeyCode.RightArrow)))
            {
                mySpriteRenderer.flipX = false;
            }
        }
    }
}
