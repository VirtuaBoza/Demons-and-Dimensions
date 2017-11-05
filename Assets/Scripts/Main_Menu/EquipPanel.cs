using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class EquipPanel : MonoBehaviour
{
    public Text nameFrame;
    public GameObject crystalPanel, teddyPanel, hunterPanel, damienPanel;
    public Toggle blueToggle, orangeToggle, greenToggle, redToggle;

    private Dictionary<PlayerCharacter, GameObject> playerToPanelDictionary;
    Toggle[] toggles;

    void Awake()
    {
        RigPlayersToPanels();
        toggles = new Toggle[] { blueToggle, orangeToggle, greenToggle, redToggle };
    }

    void OnEnable()
    {
        ShowAppropriateInventory(FindObjectOfType<GameManager>().currentCharacter);
        toggles[(int)FindObjectOfType<GameManager>().currentCharacter].isOn = true;     // This is matching the integer value of the PlayerCharacter enum to the index position of the toggles... less than ideal.
    }

    private void RigPlayersToPanels()
    {
        playerToPanelDictionary = new Dictionary<PlayerCharacter, GameObject>();
        playerToPanelDictionary.Add(PlayerCharacter.Crystal, crystalPanel);
        playerToPanelDictionary.Add(PlayerCharacter.Damien, damienPanel);
        playerToPanelDictionary.Add(PlayerCharacter.Hunter, hunterPanel);
        playerToPanelDictionary.Add(PlayerCharacter.Teddy, teddyPanel);
    }

    void ShowAppropriateInventory(PlayerCharacter playerCharacter)
    {
        Dictionary<PlayerCharacter, Character> characterDictionary = FindObjectOfType<CharacterDatabase>().CharacterDictionary;
        foreach (KeyValuePair<PlayerCharacter, GameObject> pair in playerToPanelDictionary)
        {
            if (pair.Key == playerCharacter)
            {
                pair.Value.SetActive(true);
                nameFrame.text = characterDictionary[pair.Key].Name;
            }
            else
            {
                pair.Value.SetActive(false);
            }
        }
    }

    // Forced to use index here due to limitations in Inspector.
    public void SwitchCharacter(int index)
    {
        switch (index)
        {
            case 0:
                ShowAppropriateInventory(PlayerCharacter.Crystal);
                break;
            case 1:
                ShowAppropriateInventory(PlayerCharacter.Teddy);
                break;
            case 2:
                ShowAppropriateInventory(PlayerCharacter.Hunter);
                break;
            case 3:
                ShowAppropriateInventory(PlayerCharacter.Damien);
                break;
            default:
                Debug.LogWarning("Trying to switch to a PlayerCharacter in InventoryPanel that is out of range.");
                break;
        }
    }
}
