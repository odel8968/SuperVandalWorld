using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;

public class SingleCharManager : MonoBehaviour
{
    public static SingleCharManager instance { get; private set; } //singleton pattern

    public Player_Movement theCharacter;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //if a character already exists, destroy this
            Destroy(gameObject);
        }   
    }

    void Start()
    {
        theCharacter = GameObject.Find("Player").GetComponent<Player_Movement>();
    }
}
