using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActionsButton : MonoBehaviour {

	private Text buttonText;
	private Toggle toggle;

	// Use this for initialization
	void Awake () {
		buttonText = GetComponentInChildren<Text>();
		toggle = GetComponent<Toggle>();
	}

/*	void Update () {
		if(Input.GetKeyDown("right") && GetComponent<FightMenuButton>().isSelected){
			toggle.isOn = true;
			GetComponentInChildren<ToggleGroup>().SetAllTogglesOff();
			GetComponentInChildren<AttackButton>().GetComponent<Toggle>().Select();
		}
	}*/

	public void UpdateText(int actions) {
		buttonText.text = "Actions (" + actions.ToString() + ")";
		if (actions > 0) toggle.interactable = true;
		else toggle.interactable = false;
	}

}
