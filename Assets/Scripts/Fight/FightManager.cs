using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour {

	private FightMenuFrame fightMenuFrame;
	private List<Combatant> currentCombatants = new List<Combatant>();

	void Start() {
		fightMenuFrame = FindObjectOfType<FightMenuFrame>();
	}

	public void StartFight(List<Combatant> combatants) {
		currentCombatants = combatants;
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
}
