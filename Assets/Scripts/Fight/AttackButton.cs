using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AttackButton : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("right") && GetComponent<FightMenuButton>().isSelected){
			GetComponent<Toggle>().isOn = true;
			FindObjectOfType<FightManager>().EnterTargetSelection(ACTION.Attacking);
			FindObjectOfType<FightMenuFrame>().ActivateTargetPanel(true);
			FindObjectOfType<FightMenuFrame>().ActivateFightMenu(false);
		}
	}
}
