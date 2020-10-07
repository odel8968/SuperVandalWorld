using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public Text scoreText;
    private int score;

    public void addScore(int _score)
    {
        score = score + _score;
    }

    public void resetScore()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //score++;
        scoreText.text = score.ToString();
    }
}
