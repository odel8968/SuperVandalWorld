using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

	GameObject[] mainMenuObjects;
	GameObject[] helpObjects;

	public bool level1, level2, level3;
	int levelSelected = 1;

	// Start is called before the first frame update
	void Start()
	{
		Destroy(GameObject.Find("CheckpointManager"));
		Debug.Log("Checkpoint manager removed");

		mainMenuObjects = GameObject.FindGameObjectsWithTag("MainMenu");
		helpObjects = GameObject.FindGameObjectsWithTag("ShowOnHelp");
		CloseHelpMenu();
	}

	// Update is called once per frame
	void Update()
	{

	}

	//Loads into the level selected by the button group
	public void LoadLevel()
	{
		SceneManager.LoadScene(levelSelected);
	}

	//Level 1 button switch
	public void SetLevel1(bool value)
	{
		if (value)
		{
			levelSelected = 1;
		}
	}

	//Level 2 button switch
	public void SetLevel2(bool value)
	{
		if (value)
		{
			levelSelected = 2;
		}
	}

	//Level 3 button switch
	public void SetLevel3(bool value)
	{
		if (value)
		{
			levelSelected = 3;
		}
	}

	//Hides all the main menu objects and displays the help menu objects
	public void HelpMenu()
	{
		Debug.Log("Help menu opened");

		foreach (GameObject g in mainMenuObjects)
		{
			g.SetActive(false);
		}

		foreach (GameObject g in helpObjects)
		{
			g.SetActive(true);
		}
	}

	//Hides all the help menu objects and displays the main menu objects
	public void CloseHelpMenu()
	{
		Debug.Log("Help menu closed");

		foreach (GameObject g in mainMenuObjects)
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
}
