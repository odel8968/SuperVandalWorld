using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class PauseMenu : MonoBehaviour
{
	GameObject[] pauseObjects;
	GameObject[] helpObjects;
	GameObject[] mainMenuObjects;

	public UIElement[] pauseMenuItem = new UIElement[10];
	public UIElement[] helpMenuItem = new UIElement[10];

	GameObject player;
	SoundManager soundManager;

	Toggle drBCToggle;
	Slider volumeSlider;
	GameObject sliderObject;

	Rigidbody2D rb;

	public static PauseMenu instance;

	private bool easyMode = false;


	//On game start, sets timescale to 1 and finds all required gameobjects and scripts and creates the UI
	void Start()
	{
		player = GameObject.Find("Player");
		soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
		drBCToggle = GameObject.Find("DrBCModeToggle").GetComponent<Toggle>();
		volumeSlider = GameObject.Find("VolumeSlider").GetComponent<Slider>();
		sliderObject = GameObject.Find("VolumeSlider");

		pauseMenuItem[0] = new UIElement("Game Paused", 60, Color.yellow, .9f, "ShowOnPause");
		pauseMenuItem[1] = new InteractableUIElement("Resume", 40, Color.white, .75f, "ShowOnPause", PauseMenu.instance.pauseControl);
		pauseMenuItem[2] = new InteractableUIElement("Help", 40, Color.white, .6f, "ShowOnPause", PauseMenu.instance.HelpMenu);
		pauseMenuItem[3] = new InteractableUIElement("Main Menu", 40, Color.white, .2f, "ShowOnPause", PauseMenu.instance.LoadMainMenu);
		pauseMenuItem[4] = new InteractableUIElement("Quit", 40, Color.white, .1f, "ShowOnPause", PauseMenu.instance.QuitGame);
		pauseMenuItem[5] = new SliderUIElement("Volume", 40, Color.white, .4f, "ShowOnPause", sliderObject);

		helpMenuItem[0] = new UIElement("Help Menu", 60, Color.yellow, .9f, "ShowOnHelp");
		helpMenuItem[1] = new UIElement("Controls \nW / Left Arrow: Move Left \nD / Right Arrow: Move Right \nSpace Bar: Jump \nJ: Special", 35, Color.green, .65f, "ShowOnHelp");
		helpMenuItem[2] = new InteractableUIElement("Resume", 40, Color.white, .1f, "ShowOnHelp", PauseMenu.instance.CloseHelpMenu);

		volumeSlider.value = AudioListener.volume;
		drBCToggle.isOn = easyMode;

		RectTransform m_RectTransform = drBCToggle.GetComponent<RectTransform>();
		m_RectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, (Screen.width * .5f) - 80, 160);
		m_RectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, Screen.height * .75f, 20);

		//m_RectTransform = volumeSlider.GetComponent<RectTransform>();
		//m_RectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, (Screen.width * .5f)-150, 300);
		//m_RectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, Screen.height * .6f, 20);

		Time.timeScale = 1;
		pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
		helpObjects = GameObject.FindGameObjectsWithTag("ShowOnHelp");
		mainMenuObjects = GameObject.FindGameObjectsWithTag("MainMenu");

		CloseHelpMenu();
		hidePaused();
	}

	//Singleton set instance
	void Awake()
	{
		if (instance == null)
			instance = this;
		else
			Debug.Log("Multiple pause menus created");
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


	//Cycles through the pause menu objects and displays them
	public void showPaused()
	{
		for (int i = 0; i < 10; i++)
		{
			if (pauseMenuItem[i] != null)
				pauseMenuItem[i].Show();
		}
		SliderUIElement test = (SliderUIElement)pauseMenuItem[5];
		test.Show();
	}

	//Cycles through the pause menu objects and hides them
	public void hidePaused()
	{
		for (int i = 0; i < 10; i++)
		{
			if (pauseMenuItem[i] != null)
				pauseMenuItem[i].Hide();
		}
		SliderUIElement test = (SliderUIElement)pauseMenuItem[5];
		test.Hide();
	}


	//Hides the pause menu and displays the help menu
	public void HelpMenu()
	{
		Debug.Log("Help menu opened");

		hidePaused();

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


public class UIElement
{
	protected int screenHeight, screenWidth, lineHeight;
	protected GameObject UI, textObject;

	//Base class constructor
	public UIElement(string text, int fontSize, Color color, float yPosition, string tag)
	{
		screenHeight = Screen.height;
		screenWidth = Screen.width;

		int numLines = text.Split('\n').Length;
		lineHeight = numLines * (fontSize + 10);

		UI = GameObject.Find("UI");

		textObject = new GameObject("Text Object");
		textObject.AddComponent<RectTransform>();
		textObject.transform.SetParent(UI.transform);

		RectTransform m_RectTransform = textObject.GetComponent<RectTransform>();

		m_RectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, screenWidth / 2 - screenWidth * .75f / 2, screenWidth * .75f);
		m_RectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, screenHeight * (1f - yPosition) - lineHeight / 2f, lineHeight);

		Text myText = textObject.AddComponent<Text>();
		myText.text = text;
		Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
		myText.font = ArialFont;
		myText.material = ArialFont.material;
		myText.fontSize = fontSize;
		myText.fontStyle = FontStyle.Bold;
		myText.alignment = TextAnchor.MiddleCenter;
		myText.tag = tag;
		myText.color = color;
		myText.transform.Translate(Vector3.up, Space.Self);

		Shadow myShadow = textObject.AddComponent<Shadow>();
		myShadow.effectDistance = new Vector2(3, -3);

	}

	//Displays the parent object
	public virtual void Show()
	{
		textObject.SetActive(true);
	}

	//Hides the parent object
	public virtual void Hide()
	{
		textObject.SetActive(false);
	}
}


public class InteractableUIElement : UIElement
{
	//Derived class constructor, called after base
	public InteractableUIElement(string text, int fontSize, Color color, float yPosition, string tag, UnityAction function) : base(text, fontSize, color, yPosition, tag)
	{
		Button myButton = textObject.AddComponent<Button>();
		ColorBlock cb = myButton.colors;
		cb.highlightedColor = Color.black;
		myButton.colors = cb;

		myButton.onClick.AddListener(function);
	}
}


public class SliderUIElement : UIElement
{
	GameObject sliderObject;

	public SliderUIElement(string text, int fontSize, Color color, float yPosition, string tag, GameObject slider) : base(text, fontSize, color, yPosition, tag)
	{
		//sliderObject = new GameObject("Slider Object");
		//Slider mySlider = sliderObject.AddComponent<Slider>();
		//sliderObject.transform.SetParent(UI.transform);

		sliderObject = slider;



		RectTransform m_RectTransform = slider.GetComponent<RectTransform>();

		m_RectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, (Screen.width * .5f) - 150, 300);
		m_RectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, (Screen.height * (1 - yPosition)) + .15f * yPosition * screenHeight, 20);

		//slider.SetActive(false);
	}

	//Displays the parent object
	public void Show()
	{
		textObject.SetActive(true);
		sliderObject.SetActive(true);
		Debug.Log("slider show");
	}

	//Hides the parent object
	public void Hide()
	{
		textObject.SetActive(false);
		sliderObject.SetActive(false);
	}
}