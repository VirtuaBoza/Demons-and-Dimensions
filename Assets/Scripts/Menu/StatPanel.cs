using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class StatPanel : MonoBehaviour {


	public Text nameFrameText, lvlFrameText, classFrameText, xpFrameText;
	public CHARACTER currentCharacter;
	public Toggle blueToggle, orangeToggle, greenToggle, redToggle;

	void OnEnable ()
	{
		Toggle[] toggles = new Toggle[] {blueToggle, orangeToggle, greenToggle, redToggle};
		toggles[(int)currentCharacter].isOn = true;
	}

	public void SwitchCharacter (int index) {
		switch (index) {
		case 0:
			currentCharacter = CHARACTER.Crystal;
			nameFrameText.text = "Crystal";
			classFrameText.text = "Mage";
			break;
		case 1:
			currentCharacter = CHARACTER.Teddy;
			nameFrameText.text = "Teddy";
			classFrameText.text = "Warrior";
			break;
		case 2:
			currentCharacter = CHARACTER.Hunter;
			nameFrameText.text = "Hunter";
			classFrameText.text = "Thief";
			break;
		case 3:
			currentCharacter = CHARACTER.Damien;
			nameFrameText.text = "Damien";
			classFrameText.text = "DM";
			break;
		}
	}
}