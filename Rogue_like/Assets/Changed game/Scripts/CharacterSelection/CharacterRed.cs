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

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnMouseDown()
    {
        isClickedRed = true;
        //DontDestroyOnLoad(isClickedRed); 
        Debug.Log("im choosing the red character");
        SceneManager.LoadScene("Scene1"); 

    }
}
