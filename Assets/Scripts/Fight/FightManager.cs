using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightManager : MonoBehaviour {

	private FightMenuFrame fightMenuFrame;
	private List<Combatant> currentCombatants = new List<Combatant>();
	private Dictionary<Combatant, Button> combatantButtons = new Dictionary<Combatant, Button>();

	private bool spellMode = false;
	private bool buffMode = false;

	void Start() {
		fightMenuFrame = FindObjectOfType<FightMenuFrame>();
	}

	public void StartFight(List<Combatant> combatants) {
		currentCombatants = combatants;
		foreach (Combatant combatant in combatants) {
			combatantButtons.Add(combatant,combatant.GetComponentInChildren<Button>());
			combatant.GetComponentInChildren<Button>().gameObject.SetActive(false);
		}
		RunGame();
	}

	public void RunGame() {
		currentCombatants[0].StartTurn();
		if (currentCombatants[0].isFriendly) {
			fightMenuFrame.ActivateWaitPanel(false);
			fightMenuFrame.ActivateFightMenu(true);
		} else {
			fightMenuFrame.ActivateWaitPanel(true);
			fightMenuFrame.ActivateFightMenu(false);
		}
	}

	public void EndTurn() {
		Combatant thisOne = currentCombatants[0];
		thisOne.EndTurn();
		currentCombatants.RemoveAt(0);
		currentCombatants.Add(thisOne);
		RunGame();
	}

	public void EnterTargetSelection(string AorBorC) {
		if (AorBorC.Contains("A")) EnterTargetSelection(ACTION.Attacking);
		else if (AorBorC.Contains("B")) EnterTargetSelection(ACTION.Buffing);
		else if (AorBorC.Contains("C")) EnterTargetSelection(ACTION.Casting);
		else Debug.Log("You sent something other than A, B, or C");
	}

	public void EnterTargetSelection (ACTION action){
		bool oneIsSelected = false;
		if(action == ACTION.Attacking || action == ACTION.Casting){
			foreach (KeyValuePair<Combatant, Button> entry in combatantButtons) {
				if (!entry.Key.isFriendly) {
					entry.Value.gameObject.SetActive(true);
					entry.Value.interactable = true;
					if (!oneIsSelected) {
						entry.Value.Select();
						oneIsSelected = true;
					}
				}
			}
		} else if (action == ACTION.Buffing) {
			foreach (KeyValuePair<Combatant, Button> entry in combatantButtons) {
				if (entry.Key.isFriendly) {
					entry.Value.gameObject.SetActive(true);
					entry.Value.interactable = true;
					if (!oneIsSelected) {
						entry.Value.Select();
						oneIsSelected = true;
					}
				}
			}
			buffMode = true;
		}

		if (action == ACTION.Casting) {
			spellMode = true;
		}

	}

	public void ExitTargetSelection () {
		
		Debug.Log("Resolve the selection.");

		fightMenuFrame.ActivateFightMenu(true);
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
