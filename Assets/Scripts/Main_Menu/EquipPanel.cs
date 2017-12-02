using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class EquipPanel : MonoBehaviour
{
    public Text nameFrame;
    public GameObject crystalPanel, teddyPanel, hunterPanel, damienPanel;
    public Toggle blueToggle, orangeToggle, greenToggle, redToggle;

    private Dictionary<PlayerCharacterName, GameObject> playerToPanelDictionary;
    Toggle[] toggles;

    void Awake()
    {
        RigPlayersToPanels();
        toggles = new Toggle[] { blueToggle, orangeToggle, greenToggle, redToggle };
    }

    void OnEnable()
    {
        ShowAppropriateInventory(FindObjectOfType<GameManager>().currentCharacter);
        toggles[(int)FindObjectOfType<GameManager>().currentCharacter - 1].isOn = true;     // This is matching the integer value of the PlayerCharacter enum to the index position of the toggles... less than ideal.
    }

    private void RigPlayersToPanels()
    {
        playerToPanelDictionary = new Dictionary<PlayerCharacterName, GameObject>();
        playerToPanelDictionary.Add(PlayerCharacterName.Crystal, crystalPanel);
        playerToPanelDictionary.Add(PlayerCharacterName.Damien, damienPanel);
        playerToPanelDictionary.Add(PlayerCharacterName.Hunter, hunterPanel);
        playerToPanelDictionary.Add(PlayerCharacterName.Teddy, teddyPanel);
    }

    void ShowAppropriateInventory(PlayerCharacterName playerCharacter)
    {
        var characterDictionary = FindObjectOfType<CharacterDatabase>().CharacterDictionary;
        foreach (KeyValuePair<PlayerCharacterName, GameObject> pair in playerToPanelDictionary)
        {
            if (pair.Key == playerCharacter)
            {
                pair.Value.SetActive(true);
                nameFrame.text = characterDictionary[pair.Key].PlayerCharacterName.ToString();
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
                ShowAppropriateInventory(PlayerCharacterName.Crystal);
                break;
            case 1:
                ShowAppropriateInventory(PlayerCharacterName.Teddy);
                break;
            case 2:
                ShowAppropriateInventory(PlayerCharacterName.Hunter);
                break;
            case 3:
                ShowAppropriateInventory(PlayerCharacterName.Damien);
                break;
            default:
                Debug.LogWarning("Trying to switch to a PlayerCharacter in InventoryPanel that is out of range.");
                break;
        }
    }
}
