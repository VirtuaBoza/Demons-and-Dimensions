using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BuffSpellButton : MonoBehaviour {

	void Update () {
		if(Input.GetKeyDown("right") && GetComponent<FightMenuButton>().isSelected){
			GetComponent<Toggle>().isOn = true;
			Buff();
		}
	}

	public void Buff() {
		FindObjectOfType<FightManager>().EnterTargetSelection(ACTION.Buffing);
	}
}
