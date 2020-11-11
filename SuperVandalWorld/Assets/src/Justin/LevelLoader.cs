using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    /*public int iLevelToLoad;
    public string sLevelToLoad;

    public bool useIntToLoadLevel = false;*/

    public EnvObject envObject;
    public bool nxtLevel = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionGameObject = collision.gameObject;

        if(collisionGameObject.name == "Player")
        {
            envObject.LoadNextLevel();
            nxtLevel = true;
        }

    }

    public bool SetToCurrentLevel()
    {
        nxtLevel = false;
        return nxtLevel;
    }

   /* void LoadScene()
    {
        if(useIntToLoadLevel)
        {
            SceneManager.LoadScene(iLevelToLoad);
        }
        else
        {
            SceneManager.LoadScene(sLevelToLoad);
        }
    }*/
}
