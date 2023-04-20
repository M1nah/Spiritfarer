using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonEvnet : MonoBehaviour
{
  public void StartButton()
    {
        //if(Input.GetMouseButtonDown(0))
        SceneManager.LoadScene("Scene01");
    }
}
