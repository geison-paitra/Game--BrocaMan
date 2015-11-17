using UnityEngine;
using System.Collections;

public class EnterGame : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) Application.LoadLevel("Level 1");
        else if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
                
    }

}
