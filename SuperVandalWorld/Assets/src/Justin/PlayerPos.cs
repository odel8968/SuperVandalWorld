using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPos : MonoBehaviour
{
    //reference to checkpoint manager
    private CheckpointManager cm;
    void Start()
    {
        cm = GameObject.FindGameObjectWithTag("CM").GetComponent<CheckpointManager>();
        transform.position = cm.lastCheckPointPos;
    }

    void Update()
    {
        //test checkpoints
        if(Input.GetKeyDown("y"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
