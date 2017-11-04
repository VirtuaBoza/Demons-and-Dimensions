using UnityEngine;
using UnityEngine.UI;

public enum CharacterPanelType { None, Stats, Inventory, Spells }

public class ShowPanels : MonoBehaviour
{
    public GameObject optionsPanel; //Store a reference to the Game Object OptionsPanel 
    public GameObject optionsTint;  //Store a reference to the Game Object OptionsTint 
    public GameObject menuPanel;    //Store a reference to the Game Object MenuPanel 
    public GameObject pausePanel;   //Store a reference to the Game Object PausePanel 
    public GameObject characterPanel;
    public GameObject inventoryPanel;
    public GameObject statPanel;
    public GameObject spellPanel;
    public GameObject backgroundBlocker;
    public Toggle statToggle, inventoryToggle, spellsToggle;
    public bool inFight = false;
    public CharacterPanelType currentPanel;

    private StartOptions startScript;
    private Pause pauseScript;

    void Awake()
    {
        startScript = GetComponent<StartOptions>();
        pauseScript = GetComponent<Pause>();
    }

    void Update()
    {

        if (Input.GetButtonDown("Stats") && (currentPanel != CharacterPanelType.Stats) && !startScript.inMainMenu && !pauseScript.isPaused)
        {
            ShowCharacterPanel();
            statToggle.isOn = true;
            currentPanel = CharacterPanelType.Stats;
        }
        else if (Input.GetButtonDown("Inventory") && (currentPanel != CharacterPanelType.Inventory) && !startScript.inMainMenu && !pauseScript.isPaused)
        {
            if (inFight) Debug.Log("Inventory is disabled during combat.");
            else
            {
                ShowCharacterPanel();
                inventoryToggle.isOn = true;
                currentPanel = CharacterPanelType.Inventory;
            }
        }
        else if (Input.GetButtonDown("Spells") && (currentPanel != CharacterPanelType.Spells) && !startScript.inMainMenu && !pauseScript.isPaused)
        {
            ShowCharacterPanel();
            spellsToggle.isOn = true;
            currentPanel = CharacterPanelType.Spells;
        }
        else if (((Input.GetButtonDown("Stats") && (currentPanel == CharacterPanelType.Stats)) ||
          (Input.GetButtonDown("Inventory") && (currentPanel == CharacterPanelType.Inventory)) ||
          (Input.GetButtonDown("Spells") && (currentPanel == CharacterPanelType.Spells))) && !startScript.inMainMenu && !pauseScript.isPaused)
        {
            UnPauseFromCharacterPanel();
        }
    }

    public void ShowCharacterPanel()
    {
        Time.timeScale = 0;
        characterPanel.SetActive(true);
        if (inFight) inventoryToggle.interactable = false;
        else inventoryToggle.interactable = true;
        backgroundBlocker.SetActive(true);
    }

    public void UnPauseFromCharacterPanel()
    {
        if (inFight && currentPanel == CharacterPanelType.Inventory)
        {
            inventoryToggle.interactable = false;
            statToggle.interactable = true;
            spellsToggle.interactable = true;
            FindObjectOfType<FightManager>().ExitEquip();
        }
        Time.timeScale = 1;
        characterPanel.SetActive(false);
        backgroundBlocker.SetActive(false);
        currentPanel = CharacterPanelType.None;
    }

    public void EnterEquipMode()
    {
        ShowCharacterPanel();
        inventoryToggle.interactable = true;
        statToggle.interactable = false;
        spellsToggle.interactable = false;
        inventoryToggle.isOn = true;
        currentPanel = CharacterPanelType.Inventory;
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
        menuPanel.SetActive(true);
    }

    //Call this function to deactivate and hide the main menu panel during the main menu
    public void HideMenu()
    {
        menuPanel.SetActive(false);
    }

    //Call this function to activate and display the Pause panel during game play
    public void ShowPausePanel()
    {
        pausePanel.SetActive(true);
        optionsTint.SetActive(true);
    }

    //Call this function to deactivate and hide the Pause panel during game play
    public void HidePausePanel()
    {
        pausePanel.SetActive(false);
        optionsTint.SetActive(false);

    }
}
