using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum CHARACTERPANEL {Stats, Inventory, Spells, None}

public class ShowPanels : MonoBehaviour {

	public GameObject optionsPanel;							//Store a reference to the Game Object OptionsPanel 
	public GameObject optionsTint;							//Store a reference to the Game Object OptionsTint 
	public GameObject menuPanel;							//Store a reference to the Game Object MenuPanel 
	public GameObject pausePanel;							//Store a reference to the Game Object PausePanel 
	public GameObject characterPanel;
	public GameObject inventoryPanel;
	public GameObject statPanel;
	public GameObject spellPanel;
	public GameObject backgroundBlocker;

	public Toggle statToggle, inventoryToggle, spellsToggle;
	public bool inFight = false;

	public CHARACTERPANEL currentPanel;
	private StartOptions startScript;
	private Pause pauseScript;

	void Awake() {
		startScript = GetComponent<StartOptions>();
		pauseScript = GetComponent<Pause>();
	}

	void Update() {

		if (Input.GetKeyDown (KeyCode.C) && (currentPanel != CHARACTERPANEL.Stats) && !startScript.inMainMenu && !pauseScript.isPaused) 
		{
			ShowStats();
			statToggle.isOn = true;
			currentPanel = CHARACTERPANEL.Stats;
		} else if (Input.GetKeyDown (KeyCode.I) && (currentPanel != CHARACTERPANEL.Inventory) && !startScript.inMainMenu && !pauseScript.isPaused) 
		{
			if (inFight) Debug.Log("Inventory is disabled during fights.");
			else {
				ShowStats();
				inventoryToggle.isOn = true;
				currentPanel = CHARACTERPANEL.Inventory;
			}

		} else if (Input.GetKeyDown (KeyCode.S) && (currentPanel != CHARACTERPANEL.Spells) && !startScript.inMainMenu && !pauseScript.isPaused) 
		{
			ShowStats();
			spellsToggle.isOn = true;
			currentPanel = CHARACTERPANEL.Spells;
		} else if (((Input.GetKeyDown (KeyCode.C) && (currentPanel == CHARACTERPANEL.Stats)) ||
			(Input.GetKeyDown (KeyCode.I) && (currentPanel == CHARACTERPANEL.Inventory)) || 
			(Input.GetKeyDown (KeyCode.S) && (currentPanel == CHARACTERPANEL.Spells))) && !startScript.inMainMenu && !pauseScript.isPaused) 
		{
			UnPauseFromCharacterPanel ();
		}

	}

	public void ShowStats() {
		Time.timeScale = 0;
		characterPanel.SetActive (true);
		if (inFight) inventoryToggle.interactable = false;
		else inventoryToggle.interactable = true;
		backgroundBlocker.SetActive(true);
	}

	public void UnPauseFromCharacterPanel()
	{
		if (inFight && currentPanel == CHARACTERPANEL.Inventory) {
			inventoryToggle.interactable = false;
			statToggle.interactable = true;
			spellsToggle.interactable = true;
			FindObjectOfType<FightManager>().ExitEquip();
		} 
		Time.timeScale = 1;
		characterPanel.SetActive (false);
		backgroundBlocker.SetActive(false);
		currentPanel = CHARACTERPANEL.None;
	}

	public void EnterEquipMode() {
		ShowStats ();
		inventoryToggle.interactable = true;
		statToggle.interactable = false;
		spellsToggle.interactable = false;
		inventoryToggle.isOn = true;
		currentPanel = CHARACTERPANEL.Inventory;
	}

	//Call this function to activate and display the Options panel during the main menu
	public void ShowOptionsPanel()
	{
		optionsPanel.SetActive(true);
		optionsTint.SetActive(true);
	}

	//Call this function to deactivate and hide the Options panel during the main menu
	public void HideOptionsPanel()
	{
		optionsPanel.SetActive(false);
		optionsTint.SetActive(false);
	}

	//Call this function to activate and display the main menu panel during the main menu
	public void ShowMenu()
	{
		menuPanel.SetActive (true);
	}

	//Call this function to deactivate and hide the main menu panel during the main menu
	public void HideMenu()
	{
		menuPanel.SetActive (false);
	}

	//Call this function to activate and display the Pause panel during game play
	public void ShowPausePanel()
	{
		pausePanel.SetActive (true);
		optionsTint.SetActive(true);
	}

	//Call this function to deactivate and hide the Pause panel during game play
	public void HidePausePanel()
	{
		pausePanel.SetActive (false);
		optionsTint.SetActive(false);

	}

		
}
