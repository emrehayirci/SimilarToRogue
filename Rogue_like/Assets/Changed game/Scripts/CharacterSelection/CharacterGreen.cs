using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class CharacterGreen : MonoBehaviour {

    public static CharacterGreen instance;
    public bool isClickedGreen = true;

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
        isClickedGreen = true; 
        Debug.Log("im choosing the green character");
        SceneManager.LoadScene("Scene1"); 
    }
}
