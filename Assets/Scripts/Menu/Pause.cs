using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum CHARACTERPANEL {Stats, Inventory, Spells, None}

public class Pause : MonoBehaviour {

	public Toggle statToggle, inventoryToggle, spellsToggle;

	private ShowPanels showPanels;						//Reference to the ShowPanels script used to hide and show UI panels
	private bool isPaused;								//Boolean to check if the game is paused or not
	private StartOptions startScript;					//Reference to the StartButton script
	private CHARACTERPANEL currentPanel;
	
	//Awake is called before Start()
	void Awake()
	{
		currentPanel = CHARACTERPANEL.None;
		//Get a component reference to ShowPanels attached to this object, store in showPanels variable
		showPanels = GetComponent<ShowPanels> ();
		//Get a component reference to StartButton attached to this object, store in startScript variable
		startScript = GetComponent<StartOptions> ();
	}

	// Update is called once per frame
	void Update () {

		//Check if the Cancel button in Input Manager is down this frame (default is Escape key) and that game is not paused, and that we're not in main menu
		if (Input.GetButtonDown ("Cancel") && !isPaused && !startScript.inMainMenu) 
		{
			//Call the DoPause function to pause the game
			DoPause();
		} 
		//If the button is pressed and the game is paused and not in main menu
		else if (Input.GetButtonDown ("Cancel") && isPaused && !startScript.inMainMenu) 
		{
			//Call the UnPause function to unpause the game
			UnPause ();
		}

		if (Input.GetKeyDown (KeyCode.C) && (currentPanel != CHARACTERPANEL.Stats) && !startScript.inMainMenu && !isPaused) 
		{
			statToggle.isOn = true;

		} else if (Input.GetKeyDown (KeyCode.I) && (currentPanel != CHARACTERPANEL.Inventory) && !startScript.inMainMenu && !isPaused) 
		{
			inventoryToggle.isOn = true;

		} else if (Input.GetKeyDown (KeyCode.S) && (currentPanel != CHARACTERPANEL.Spells) && !startScript.inMainMenu && !isPaused) 
		{
			spellsToggle.isOn = true;
		} else if (((Input.GetKeyDown (KeyCode.C) && (currentPanel == CHARACTERPANEL.Stats)) ||
			(Input.GetKeyDown (KeyCode.I) && (currentPanel == CHARACTERPANEL.Inventory)) || 
			(Input.GetKeyDown (KeyCode.S) && (currentPanel == CHARACTERPANEL.Spells))) && !startScript.inMainMenu && !isPaused) 
		{
			statToggle.isOn = false;
			inventoryToggle.isOn = false;
			spellsToggle.isOn = false;
			UnPauseFromCharacterPanel ();
		}

		if(Input.GetKeyDown(KeyCode.N)) Debug.Log(currentPanel);
			
	}

	public void DoPause()
	{
		//Set isPaused to true
		isPaused = true;
		//Set time.timescale to 0, this will cause animations and physics to stop updating
		Time.timeScale = 0;
		//call the ShowPausePanel function of the ShowPanels script
		showPanels.ShowPausePanel ();
	}


	public void UnPause()
	{
		//Set isPaused to false
		isPaused = false;
		//Set time.timescale to 1, this will cause animations and physics to continue updating at regular speed
		Time.timeScale = 1;
		//call the HidePausePanel function of the ShowPanels script
		showPanels.HidePausePanel ();
	}

	public void ShowStats() {
		currentPanel = CHARACTERPANEL.Stats;
		statToggle.interactable = false;
		inventoryToggle.interactable = true;
		spellsToggle.interactable = true;
		Time.timeScale = 0;
		showPanels.ShowStatPanel();
	}

	public void ShowInventory()
	{
		currentPanel = CHARACTERPANEL.Inventory;
		statToggle.interactable = true;
		inventoryToggle.interactable = false;
		spellsToggle.interactable = true;
		//Set time.timescale to 0, this will cause animations and physics to stop updating
		Time.timeScale = 0;
		//call the ShowPausePanel function of the ShowPanels script
		showPanels.ShowInventoryPanel();
	}

	public void ShowSpells() {
		currentPanel = CHARACTERPANEL.Spells;
		statToggle.interactable = true;
		inventoryToggle.interactable = true;
		spellsToggle.interactable = false;
		Time.timeScale = 0;
		showPanels.ShowSpellPanel();
	}

	public void UnPauseFromCharacterPanel()
	{
		currentPanel = CHARACTERPANEL.None;
		//Set time.timescale to 1, this will cause animations and physics to continue updating at regular speed
		Time.timeScale = 1;
		//call the HidePausePanel function of the ShowPanels script
		showPanels.HideCharacterPanel ();
	}

}
