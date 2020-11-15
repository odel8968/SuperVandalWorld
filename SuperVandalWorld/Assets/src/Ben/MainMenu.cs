using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	SoundManager soundManager;

	GameObject[] mainMenuObjects;
	GameObject[] helpObjects;

	public bool level1, level2, level3;
	int levelSelected = 1;

	// Start is called before the first frame update
	void Start()
	{
		soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
		soundManager.PlaySoundLooping("Theme0");

		mainMenuObjects = GameObject.FindGameObjectsWithTag("MainMenu");
		helpObjects = GameObject.FindGameObjectsWithTag("ShowOnHelp");
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void LoadLevel()
	{
		//if (level1)
		//levelSelected = 1;
		SceneManager.LoadScene(levelSelected);
	}

	public void SetLevel1(bool value)
	{
		if (value)
		{
			levelSelected = 1;
		}
	}

	public void SetLevel2(bool value)
	{
		if (value)
		{
			levelSelected = 2;
		}
	}

	public void SetLevel3(bool value)
	{
		if (value)
		{
			levelSelected = 3;
		}
	}

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
}
