using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(0))
        {
            GameQuit();
        }
    }

    public void GameQuit()
    {
        Application.Quit();
    }
}
