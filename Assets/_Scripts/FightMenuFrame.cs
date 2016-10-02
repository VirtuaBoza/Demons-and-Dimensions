using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FightMenuFrame : MonoBehaviour {

	public void DisableFightMenu (){
		Button[] buttons = GetComponentsInChildren<Button>();
		foreach(Button button in buttons){
			button.interactable = false;
		}
	}

	public void EnableFightMenu () {
		Button[] buttons = GetComponentsInChildren<Button>();
		foreach(Button button in buttons){
			button.interactable = true;
		}
	}

}
