using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BuffSpellButton : MonoBehaviour {

	void Update () {
		if(Input.GetKeyDown("right") && GetComponent<FightMenuButton>().isSelected){
			GetComponent<Toggle>().isOn = true;
			FindObjectOfType<Arena>().EnterTargetSelection(ACTION.Buffing);
			FindObjectOfType<FightMenuFrame>().ActivateTargetPanel(true);
			FindObjectOfType<FightMenuFrame>().ActivateFightMenu(false);
		}
	}

}
