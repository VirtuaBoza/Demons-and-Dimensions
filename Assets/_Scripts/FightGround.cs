using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FightGround : MonoBehaviour {

	private bool spellMode = false;
	private bool buffMode = false;


	public void EnterSpellMode() {
		spellMode = true;
	}

	public void EnterBuffMode() {
		spellMode = true;
		buffMode = true;
	}

	//TODO filter target options unless spell mode is true
	public void EnterTargetSelection (){
		Button[] buttons = GetComponentsInChildren<Button>();
		foreach(Button button in buttons){
			button.interactable = true;
		}
		buttons[0].Select();
	}
		
	public void ExitTargetSelection () {
		Button[] buttons = GetComponentsInChildren<Button>();
		foreach(Button button in buttons){
			button.interactable = false;
		}

		if(spellMode){
			if(buffMode){
				ActivateOptions(2);
			} else {
				ActivateOptions(1);
			}
		} else {
			ActivateOptions(0);
		}

		spellMode = false;
		buffMode = false;

	}

	private void ActivateOptions(int index){
		var child = GameObject.Find("Parent").transform.GetChild(index);
		Button[] options = child.GetComponentsInChildren<Button>();
		options[0].Select();
	}

}
