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
	public Sprite brightBlueBubble, brightOrangeBubble, brightGreenBubble, brightRedBubble,
	dullBlueBubble, dullOrangeBubble, dullGreenBubble, dullRedBubble;

	void Start () {
		characterPanels = new GameObject[] { crystalPanel, teddyPanel, hunterPanel, damienPanel};
		SwitchCharacter ((int)currentCharacter);
	}

	public void SwitchCharacter (int index) {
		switch (index) {
		case 0:
			currentCharacter = CHARACTER.Crystal;
			ShowAppropriatePanel(crystalPanel);
			nameFrameText.text = "Crystal";
			IlluminateToggle (blueToggle);
			break;
		case 1:
			currentCharacter = CHARACTER.Teddy;
			ShowAppropriatePanel(teddyPanel);
			nameFrameText.text = "Teddy";
			IlluminateToggle (orangeToggle);
			break;
		case 2:
			currentCharacter = CHARACTER.Hunter;
			ShowAppropriatePanel(hunterPanel);
			nameFrameText.text = "Hunter";
			IlluminateToggle (greenToggle);
			break;
		case 3:
			currentCharacter = CHARACTER.Damien;
			ShowAppropriatePanel(damienPanel);
			nameFrameText.text = "Damien";
			IlluminateToggle (redToggle);
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

	public void IlluminateToggle (Toggle toggle) {
		if (toggle == blueToggle) {
			blueToggle.GetComponent<Image>().sprite = brightBlueBubble;

			orangeToggle.GetComponent<Image>().sprite = dullOrangeBubble;
			greenToggle.GetComponent<Image>().sprite = dullGreenBubble;
			redToggle.GetComponent<Image>().sprite = dullRedBubble;
		} else if (toggle == orangeToggle) {
			orangeToggle.GetComponent<Image>().sprite = brightOrangeBubble;

			blueToggle.GetComponent<Image>().sprite = dullBlueBubble;
			greenToggle.GetComponent<Image>().sprite = dullGreenBubble;
			redToggle.GetComponent<Image>().sprite = dullRedBubble;
		} else if (toggle == greenToggle) {
			greenToggle.GetComponent<Image>().sprite = brightGreenBubble;

			orangeToggle.GetComponent<Image>().sprite = dullOrangeBubble;
			blueToggle.GetComponent<Image>().sprite = dullBlueBubble;
			redToggle.GetComponent<Image>().sprite = dullRedBubble;
		} else if (toggle == redToggle) {
			redToggle.GetComponent<Image>().sprite = brightRedBubble;

			orangeToggle.GetComponent<Image>().sprite = dullOrangeBubble;
			greenToggle.GetComponent<Image>().sprite = dullGreenBubble;
			blueToggle.GetComponent<Image>().sprite = dullBlueBubble;
		}
	}

}
