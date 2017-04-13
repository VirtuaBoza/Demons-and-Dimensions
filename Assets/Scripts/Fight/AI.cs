using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {

	private Combatant myCombatant, target;
	private FightManager fightManager;

	void Awake () {
		myCombatant = GetComponent<Combatant>();
		fightManager = FindObjectOfType<FightManager>();
	}
	
	public void StartTurn() {
		// TODO Decide what to do. For now, you're going to move as close as you can to a Friendly
		SelectNearestTarget();
		MoveForMelee();
	}

	void SelectNearestTarget() {
		Combatant[] combatants = FindObjectsOfType<Combatant>();
		foreach (Combatant combatant in combatants) {
			if (combatant.isFriendly) {
				if (target == null) target = combatant;
				else {
					if (Vector3.Distance(combatant.transform.localPosition,transform.localPosition) < Vector3.Distance(target.transform.localPosition,transform.localPosition)) target = combatant;
				}
			}
		}

	}

	void MoveForMelee() {
		int spread = 1;
		while (spread < 7) {
			List<Vector3> nextClosePositions = new List<Vector3>();
			for (int x = -spread; x <= spread; x++) {
				for (int y = -spread; y <= spread; y++) {
					Vector3 position = new Vector3(target.transform.localPosition.x + x, target.transform.localPosition.y + y);
					if (position.x >= 1 && position.x <= 8 && position.y >= 1 && position.y <= 8 && !fightManager.currentPositionList.Contains(position)) {
						nextClosePositions.Add(new Vector3(target.transform.localPosition.x + x, target.transform.localPosition.y + y));
					}
				}
			}
			Vector3 bestPosition = new Vector3();
			foreach (Vector3 position in nextClosePositions) {
				if (bestPosition.Equals(Vector3.zero)) bestPosition = position;
				else {
					if (Vector3.Distance(position,transform.localPosition) < Vector3.Distance(bestPosition,transform.localPosition)) bestPosition = position;
				}
			}
			if (Mathf.Abs(bestPosition.x - transform.localPosition.x) + Mathf.Abs(bestPosition.y - transform.localPosition.y) <= myCombatant.remainingMoves) {
				fightManager.UpdatePositionList(transform.localPosition,bestPosition);
				myCombatant.MoveCombatant(bestPosition);
				break;
			} else {
				spread++;
			}	
		}
	}
}
