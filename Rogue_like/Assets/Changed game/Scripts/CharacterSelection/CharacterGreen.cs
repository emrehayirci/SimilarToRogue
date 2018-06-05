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
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnMouseDown()
    {
        isClickedGreen = true; 
        Debug.Log("im choosing the green character");
        SceneManager.LoadScene("Scene1"); 
    }
}
