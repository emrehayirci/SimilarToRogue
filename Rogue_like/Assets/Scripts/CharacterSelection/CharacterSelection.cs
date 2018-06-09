using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour {

    public static bool isClickedRed = false;
    public static bool characterSelected = false;
    public static string sessionId;
    public void OnClickGreen()
    {
        isClickedRed = false;
        characterSelected = true;
        //sessionId = GUID.Generate().ToString();
        SceneManager.LoadScene("Scene1");

    }

    public void OnClickRed()
    {
        isClickedRed = true;
        characterSelected = true;
        //sessionId = UnityEditor.GUID.Generate().ToString();
        SceneManager.LoadScene("Scene1");
    }
}
