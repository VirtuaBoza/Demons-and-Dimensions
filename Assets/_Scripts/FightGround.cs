using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FightGround : MonoBehaviour {

	private bool spellMode = false;
	private bool targetIsFriendly = false;

	public void EnterSpellMode() {
		spellMode = true;
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
			if(targetIsFriendly){
				print("Exiting in spell mode, target friendly");
				ActivateOptions(2);
			} else {
				print("Exiting in spell mode, target enemy");
				ActivateOptions(1);
			}
		} else {
			print("Exiting not in spell mode");
			ActivateOptions(0);
		}

		spellMode = false;
		targetIsFriendly = false;

	}

	private void ActivateOptions(int index){
		var child = GameObject.Find("Parent").transform.GetChild(index);
		child.gameObject.SetActive(true);
		Button[] options = child.GetComponentsInChildren<Button>();
		options[0].Select();
	}

}
