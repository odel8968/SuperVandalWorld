using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepObjBetweenLevels : MonoBehaviour
{
    //object to keep between scenes
    GameObject keepObject;

    //varialbe to adjust for camera position
    Vector3 tempPos;

    //keep track of objects in the scene
    private GameObject[] players;
    private GameObject[] mainCamera;


    // Start is called before the first frame update
    void Start()
    {
        //find the gameObject the script is attached to
        keepObject = this.gameObject;

        //keep gameObject between scenes
        DontDestroyOnLoad(keepObject);
    }
    void OnLevelWasLoaded(int level)
    {
        FindStartPos();

        players = GameObject.FindGameObjectsWithTag("Player");
        mainCamera = GameObject.FindGameObjectsWithTag("MainCamera");

        if(players.Length > 1)
        {
            Destroy(players[1]);
        }

        if(mainCamera.Length > 1)
        {
            Destroy(mainCamera[1]);
        }
    }

    void FindStartPos()
    {
        //If the object kept is the main camera
        if(this.gameObject.name == "Main Camera")
        {
            //move position on z axis back to -1 from the start position
            tempPos = GameObject.FindWithTag("StartPos").transform.position;
            tempPos.z -= 1;

            //set the camera position in the scene
            keepObject.transform.position = tempPos;
        }
        else
        {
            //if object is the player, set the position to the start position
            keepObject.transform.position = GameObject.FindWithTag("StartPos").transform.position;
        }
        
    }

}
