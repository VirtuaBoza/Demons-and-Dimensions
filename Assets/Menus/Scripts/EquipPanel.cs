using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EquipPanel : MonoBehaviour {

	public Text nameFrameText;
	public GameObject crystalPanel, teddyPanel, hunterPanel, damienPanel;
	public GameObject[] characterPanels; 
	public enum Character {Crystal, Teddy, Hunter, Damien}
	public Character currentCharacter = Character.Crystal;

	void Start () {
		characterPanels = new GameObject[] { crystalPanel, teddyPanel, hunterPanel, damienPanel};
		FindRightPanel (Character.Crystal);
	}

	public void FindRightPanel(Character character){
		switch (character) {
		case Character.Crystal:
			ShowAppropriatePanel (crystalPanel);
			break;
		case Character.Teddy:
			ShowAppropriatePanel (teddyPanel);
			break;
		case Character.Hunter:
			ShowAppropriatePanel (hunterPanel);
			break;
		case Character.Damien:
			ShowAppropriatePanel (damienPanel);
			break;
		}
	}

	public void ShowAppropriatePanel(GameObject rightPanel) {
		foreach (GameObject panel in characterPanels) {
			if (panel == rightPanel) {
				panel.SetActive (true);
			} else {
				panel.SetActive (false);
			}
		}
	}
}
