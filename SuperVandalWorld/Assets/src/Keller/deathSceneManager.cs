using System.Reflection.Emit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*scene manager for when a player dies in the game. Created by
  Keller and Justin */

public class deathSceneManager : MonoBehaviour
{

    public static string lastActiveScene = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void continueGame()
    {
        SceneManager.LoadScene(lastActiveScene);
    }

    public void goToMenu()
    {
        SceneManager.LoadScene("TitleScene");
        Destroy(GameObject.Find("CheckpointManger"));
        Debug.Log("main menu called, checkpoint manager destroyed");
    }

    public void quitGame()
    {
        Debug.Log("quitting game");
		Application.Quit();
    }
}
