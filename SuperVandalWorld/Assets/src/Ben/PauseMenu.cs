using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
	GameObject[] pauseObjects;
	GameObject[] helpObjects;

	GameObject player;

	public Rigidbody rb;


	void Start()
	{
		Time.timeScale = 1;
		pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
		helpObjects = GameObject.FindGameObjectsWithTag("ShowOnHelp");
		player = GameObject.Find("Player");

		
		rb = player.GetComponent<Rigidbody>();

		//rb.isKinematic = false;
		//rb.detectCollisions = false;

		foreach (GameObject g in helpObjects)
		{
			g.SetActive(false);
		}

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
			CloseHelpMenu();
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
		Debug.Log("Help menu opened");

		foreach (GameObject g in pauseObjects)
		{
			g.SetActive(false);
		}

		foreach (GameObject g in helpObjects)
		{
			g.SetActive(true);
		}
	}

	public void CloseHelpMenu()
	{
		Debug.Log("Help menu closed");

		foreach (GameObject g in pauseObjects)
		{
			g.SetActive(true);
		}

		foreach (GameObject g in helpObjects)
		{
			g.SetActive(false);
		}

		//player.GetComponent<Rigidbody>().detectCollisions = false;
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
