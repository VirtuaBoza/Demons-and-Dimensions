using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FightGround : MonoBehaviour {

	private bool spellMode = false;
	private bool buffMode = false;

	private FightMenu fightMenu;
	private FightMenuFrame fightMenuFrame;

	void Start () {
		fightMenu = FindObjectOfType<FightMenu>();
		fightMenuFrame = FindObjectOfType<FightMenuFrame>();
	}

	public void EnterTargetSelection (string AorSorB){
		if(AorSorB == "A"){
			//something
		} else if (AorSorB == "S") {
			spellMode = true;
		} else if (AorSorB == "B") {
			buffMode = true;
		}
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

		fightMenu.ActivateMenu(true);
		fightMenuFrame.ActivateTargetPanel(false);

		if(spellMode){
			FindObjectOfType<AttackSpellOptions>().GetComponentsInChildren<Button>()[0].Select();
		} else if(buffMode){
				FindObjectOfType<BuffSpellOptions>().GetComponentsInChildren<Button>()[0].Select();
		} else {
			FindObjectOfType<AttackOptions>().GetComponentsInChildren<Button>()[0].Select();
		}

		spellMode = false;
		buffMode = false;

	}

}
