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


    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Debug.Log("Multiple UI managers created");
    }

    void Start()
    {
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    public void AddScore(int _score)
    {
        score = score + _score;
        //soundManager.PlaySound("mClose");
    }

    public void ResetScore()
    {
        score = 0;
    }

    public int GetScore()
    {
        return score;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();
    }
}
