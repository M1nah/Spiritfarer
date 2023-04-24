using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public MeteorPool pool;

    public Text ScoreText;
    public int Score = 0;

    void Awake()
    {
        if(instance == null)
        {
        instance = this;
        }
    }


    public void Addscore(int score)
    {
        Score += score;
        ScoreText.text = "Score: " + Score;
    }
}
