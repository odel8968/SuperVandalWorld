using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text scoreText;
    private int score;

    public static UIManager instance;


    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Debug.Log("Multiple sound managers created");
    }

    public void addScore(int _score)
    {
        score = score + _score;
    }

    public void resetScore()
    {
        score = 0;
    }

    public int getScore()
    {
        return score;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();
    }
}
