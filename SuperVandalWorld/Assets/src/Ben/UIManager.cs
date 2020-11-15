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

    SoundManager soundManager;

    //Singleton pointer
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Debug.Log("Multiple UI managers created");
    }

    //Finds sound manager on startup
    void Start()
    {
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    //Method to adjust the player's score
    public void AddScore(int _score)
    {
        score = score + _score;
    }

    //Method to reset the players score to 0
    public void ResetScore()
    {
        score = 0;
    }

    //Method to read the player score
    public int GetScore()
    {
        return score;
    }

    // Draws the current score to the screen every frame
    void Update()
    {
        scoreText.text = score.ToString();
    }
}
