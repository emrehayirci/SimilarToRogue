using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour {

    public static bool isClickedRed = false;
    public static bool characterSelected = false;

    public void OnClickGreen()
    {
        isClickedRed = false;
        characterSelected = true;
        SceneManager.LoadScene("Scene1");
    }

    public void OnClickRed()
    {
        isClickedRed = true;
        characterSelected = true;
        SceneManager.LoadScene("Scene1");
    }
}
