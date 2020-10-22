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

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionGameObject = collision.gameObject;

        if(collisionGameObject.name == "Player")
        {
            envObject.LoadNextLevel();
        }

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
