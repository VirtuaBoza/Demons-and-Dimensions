using UnityEngine;
using UnityEngine.UI;

public class EquipPanel : MonoBehaviour
{

    public Text nameFrame;
    public GameObject crystalPanel, teddyPanel, hunterPanel, damienPanel;
    public Toggle blueToggle, orangeToggle, greenToggle, redToggle;

    private GameObject[] characterPanels;

    void Start()
    {
        characterPanels = new GameObject[] { crystalPanel, teddyPanel, hunterPanel, damienPanel };
        Toggle[] toggles = new Toggle[] { blueToggle, orangeToggle, greenToggle, redToggle };
        toggles[0].isOn = true;
    }

    public void SwitchCharacter(int index)
    {
        switch (index)
        {
            case 0:
                ShowAppropriatePanel(crystalPanel);
                nameFrame.text = "Crystal";
                break;
            case 1:
                ShowAppropriatePanel(teddyPanel);
                nameFrame.text = "Teddy";
                break;
            case 2:
                ShowAppropriatePanel(hunterPanel);
                nameFrame.text = "Hunter";
                break;
            case 3:
                ShowAppropriatePanel(damienPanel);
                nameFrame.text = "Damien";
                break;
        }
    }

    void ShowAppropriatePanel(GameObject correctPanel)
    {
        foreach (GameObject panel in characterPanels)
        {
            if (panel == correctPanel)
            {
                panel.SetActive(true);
            }
            else
            {
                panel.SetActive(false);
            }
        }
    }

}
