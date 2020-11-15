using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	GameObject[] pauseObjects;
	GameObject[] helpObjects;
	GameObject[] mainMenuObjects;

	GameObject player;
	SoundManager soundManager;

	Toggle drBCToggle;

	Rigidbody2D rb;

	private bool easyMode = false;


	void Start()
	{
		Time.timeScale = 1;
		pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
		helpObjects = GameObject.FindGameObjectsWithTag("ShowOnHelp");
		mainMenuObjects = GameObject.FindGameObjectsWithTag("MainMenu");

		player = GameObject.Find("Player");

		soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();

		drBCToggle = GameObject.Find("DrBCModeToggle").GetComponent<Toggle>();

		rb = GameObject.Find("Player").GetComponent<Rigidbody2D>();

		//rb.gravityScale = 0;
		//rb.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
		//rb.bodyType = RigidbodyType2D.Kinematic;

		CloseHelpMenu();
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


	//controls the pausing/unpausing of the scene and displaying/hiding the pause menu
	public void pauseControl()
	{
		if (Time.timeScale == 1)
		{
			Time.timeScale = 0;
			soundManager.PlaySound("MenuOpen");
			showPaused();
		}
		else if (Time.timeScale == 0)
		{
			Time.timeScale = 1;
			soundManager.PlaySound("MenuClose");
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


	//Hides the pause menu and displays the help menu
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

	//Hides the help menu and displays the pause menu
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

		if(easyMode)
        {
			drBCToggle.isOn = true;
        }

	}

	//Exits the application when quit button pressed
	public void QuitGame()
    {
		Debug.Log("Quit command received");
		Application.Quit();
    }

	//Sets the global volume of the game from the menu slider
	public void setVolume(float newVolume)
    {
		AudioListener.volume = newVolume;
		Debug.Log(newVolume);
	}

	public bool DrBCMode()
    {
		return easyMode;
    }

	//Checks the status of the easy moddle menu toggle
	public void easyModeToggle()
    {
		if (drBCToggle.isOn)
		{
			Debug.Log("Dr BC mode enabled");
			easyMode = true;
		}

		else
		{
			Debug.Log("Dr BC mode disabled");
			easyMode = false;
		}
	}

	//Returns to the main menu
	public void LoadMainMenu()
    {
		SceneManager.LoadScene(0, LoadSceneMode.Single);
	}
}
