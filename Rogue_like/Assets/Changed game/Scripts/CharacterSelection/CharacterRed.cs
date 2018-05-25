using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class CharacterRed : MonoBehaviour {

    public static CharacterRed instance;
    public bool isClickedRed = false;  

    private void Awake()
    {
        instance = this; 
    }

    public void OnMouseOver()
    {
        //change alpha to show selection
    }

    public void OnMouseDown()
    {
        isClickedRed = true;
        Debug.Log("im choosing the red character");
        SceneManager.LoadScene("Scene1"); 

    }
}
