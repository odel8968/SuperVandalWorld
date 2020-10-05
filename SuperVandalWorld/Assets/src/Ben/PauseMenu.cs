using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
	GameObject[] pauseObjects;

	//public float volumeSlider = 1.0f;

	void Start()
	{
		Time.timeScale = 1;
		pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
		hidePaused();
	}

	// Update is called once per frame
	void Update()
	{

		//uses the escape button to pause and unpause the game
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			pauseControl();
		}
	}


	//controls the pausing of the scene
	public void pauseControl()
	{
		if (Time.timeScale == 1)
		{
			Time.timeScale = 0;
			SoundManager.instance.PlaySound("mOpen");
			showPaused();
		}
		else if (Time.timeScale == 0)
		{
			Time.timeScale = 1;
			SoundManager.instance.PlaySound("mClose");
			hidePaused();
		}
	}

	//shows objects with ShowOnPause tag
	public void showPaused()
	{
		foreach (GameObject g in pauseObjects)
		{
			g.SetActive(true);
		}
	}

	//hides objects with ShowOnPause tag
	public void hidePaused()
	{
		foreach (GameObject g in pauseObjects)
		{
			g.SetActive(false);
		}
	}

	//loads inputted level
	public void LoadLevel(string level)
	{
		//Application.LoadLevel(level);
	}

	public void HelpMenu()
	{
		Debug.Log("Help command received");
	}

	public void QuitGame()
    {
		Debug.Log("Quit command received");
		Application.Quit();
    }

	public void SetVolume(float newVolume)
    {
		AudioListener.volume = newVolume;
		//Debug.Log(newVolume);
	}
}
