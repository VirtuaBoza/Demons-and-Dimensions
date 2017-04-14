using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBat : Enemy {

	private Combatant myCombatant,target;

	void Start() {
		myCombatant = GetComponent<Combatant>();
	}

	public override void Attack() {
		target = GetComponent<AI>().target;
		Vector3 myPosition = new Vector3(transform.localPosition.x,transform.localPosition.y);
		bool withinMeleeRange = false;
		for (int x = -1; x <= 1; x++) {
			for (int y = -1; y <= 1; y++) {
				if (myPosition.Equals(new Vector3(target.transform.localPosition.x + x, target.transform.localPosition.y + y))) {
					withinMeleeRange = true;
					break;
				}
			}
			if (withinMeleeRange) break;
		}
		if (withinMeleeRange) Melee();
	}

	private void Melee() {
		target.TakeDamage(1);
		myCombatant.PopUpText(Color.black,"Bite");
	}
}
