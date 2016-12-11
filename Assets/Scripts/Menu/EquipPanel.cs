using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum CHARACTER {Crystal, Teddy, Hunter, Damien}

public class EquipPanel : MonoBehaviour {

	public Text nameFrameText;
	public GameObject crystalPanel, teddyPanel, hunterPanel, damienPanel;
	public GameObject[] characterPanels; 
	public CHARACTER currentCharacter;
	public Toggle blueToggle, orangeToggle, greenToggle, redToggle;

	void Start () {
		characterPanels = new GameObject[] { crystalPanel, teddyPanel, hunterPanel, damienPanel};
		Toggle[] toggles = new Toggle[] {blueToggle, orangeToggle, greenToggle, redToggle};
		toggles[(int)currentCharacter].isOn = true;
	}

	public void SwitchCharacter (int index) {
		switch (index) {
		case 0:
			currentCharacter = CHARACTER.Crystal;
			ShowAppropriatePanel(crystalPanel);
			nameFrameText.text = "Crystal";
			break;
		case 1:
			currentCharacter = CHARACTER.Teddy;
			ShowAppropriatePanel(teddyPanel);
			nameFrameText.text = "Teddy";
			break;
		case 2:
			currentCharacter = CHARACTER.Hunter;
			ShowAppropriatePanel(hunterPanel);
			nameFrameText.text = "Hunter";
			break;
		case 3:
			currentCharacter = CHARACTER.Damien;
			ShowAppropriatePanel(damienPanel);
			nameFrameText.text = "Damien";
			break;
		}
	}

	void ShowAppropriatePanel(GameObject rightPanel) {
		foreach (GameObject panel in characterPanels) {
			if (panel == rightPanel) {
				panel.SetActive (true);
			} else {
				panel.SetActive (false);
			}
		}
	}

}
