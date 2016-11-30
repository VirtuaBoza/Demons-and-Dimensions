using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActionsButton : MonoBehaviour {

	private Text buttonText;
	private Toggle toggle;

	// Use this for initialization
	void Start () {
		buttonText = GetComponentInChildren<Text>();
		buttonText.text = "Actions (" + 1.ToString() + ")"; //TODO This will pull the acting player's action count

		toggle = GetComponent<Toggle>();

	}

	void Update () {
		if(Input.GetKeyDown("right") && GetComponent<FightMenuButton>().isSelected){
			toggle.isOn = true;
			GetComponentInChildren<ToggleGroup>().SetAllTogglesOff();
			GetComponentInChildren<AttackButton>().GetComponent<Toggle>().Select();
		}
	}

}
