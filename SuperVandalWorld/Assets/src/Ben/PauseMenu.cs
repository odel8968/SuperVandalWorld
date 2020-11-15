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

	Toggle easyToggle;

	Rigidbody2D rb;


	void Start()
	{
		Time.timeScale = 1;
		pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
		helpObjects = GameObject.FindGameObjectsWithTag("ShowOnHelp");
		mainMenuObjects = GameObject.FindGameObjectsWithTag("MainMenu");

		player = GameObject.Find("Player");

		soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();

		easyToggle = GameObject.Find("DrBCModeToggle").GetComponent<Toggle>();

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


	//controls the pausing of the scene
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

	}

	public void QuitGame()
    {
		Debug.Log("Quit command received");
		Application.Quit();
    }

	public void SetVolume(float newVolume)
    {
		AudioListener.volume = newVolume;
		Debug.Log(newVolume);
	}

	public void DrBCMode(bool value)
    {
		if (easyToggle.isOn)
		{
			Debug.Log("Dr BC mode enabled");
		}

		else
		{
			Debug.Log("Dr BC mode disabled");
		}
	}

	public void LoadMainMenu()
    {
		SceneManager.LoadScene(0, LoadSceneMode.Single);
	}
}
