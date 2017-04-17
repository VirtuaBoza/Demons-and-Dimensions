using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackOptionPrefab : MonoBehaviour {

	public Text nameText,bonusText,damageAndTypeText; //Assigned in inspector

	public string title;
	public DIE damageRange;
	public int damageMulti;
	public DAMAGETYPE damageType;
	public bool finesse;
	public bool heavy;
	public bool isLight;
	public bool reach;
	public bool twoHanded;
	public int range;
	public int maxRange;

	public void UpdateFields() {
		nameText.text = title;
		if (TargetsInRange()) {
			bonusText.text = "+" + GetProfBonus();
			damageAndTypeText.text = damageMulti.ToString() + GetDieTypeInString(damageRange) + " " + GetDamageTypeInString(damageType);
			GetComponent<Button>().interactable = true; // This is probably not necessary
		} else {
			bonusText.text = "No targets in range";
			damageAndTypeText.text = "";
			GetComponent<Button>().interactable = false;
		}

	}

	public void DisplayNoWeapon() {
		nameText.text = "No weapons equipped";
		bonusText.text = "";
		damageAndTypeText.text = "";
		GetComponent<Button>().interactable = false;
	}

	string GetDieTypeInString(DIE die) {
		switch (die) {
		case DIE.d00:
			return "d00";
		case DIE.d10:
			return "d10";
		case DIE.d12:
			return "d12";
		case DIE.d20:
			return "d20";
		case DIE.d4:
			return "d4";
		case DIE.d6:
			return "d6";
		case DIE.d8:
			return "d8";
		default:
			return "AttackOptionPrefab doesn't recognize Die type";
		}
	}

	string GetDamageTypeInString(DAMAGETYPE damType) {
		switch (damType) {
		case DAMAGETYPE.Acid:
			return "Acid";
		case DAMAGETYPE.Bludgeoning:
			return "Bludgeoning";
		case DAMAGETYPE.Cold:
			return "Cold";
		case DAMAGETYPE.Fire:
			return "Fire";
		case DAMAGETYPE.Force:
			return "Force";
		case DAMAGETYPE.Lightning:
			return "Lightning";
		case DAMAGETYPE.Necrotic:
			return "Necrotic";
		case DAMAGETYPE.Piercing:
			return "Piercing";
		case DAMAGETYPE.Poison:
			return "Poison";
		case DAMAGETYPE.Psychic:
			return "Psychic";
		case DAMAGETYPE.Radiant:
			return "Radiant";
		case DAMAGETYPE.Slashing:
			return "Slashing";
		case DAMAGETYPE.Thunder:
			return "Thunder";
		default:
			return "AttackOptionPrefab doesn't recognize DamageType";
		}
	}

	bool TargetsInRange() {
		int trueRange = maxRange;
		if (reach) trueRange++;
		FightManager fightManager = FindObjectOfType<FightManager>();
		List<Vector3> enemyPositions = new List<Vector3>();

		foreach (Combatant combatant in fightManager.currentCombatants) {
			if (!combatant.isFriendly) enemyPositions.Add(combatant.transform.localPosition);
		}

		for (int x = -trueRange; x <= trueRange; x++) {
			for (int y = -trueRange; y <= trueRange; y++) {
				if (enemyPositions.Contains(new Vector3(fightManager.currentPlayer.transform.localPosition.x + x,fightManager.currentPlayer.transform.localPosition.y + y))) {
					return true;
				} 
			}
		}
		return false;
	}

	int GetProfBonus() {
		Debug.Log("Trying to find " + FindObjectOfType<FightManager>().currentPlayer.character);
		foreach (Character character in FindObjectOfType<CharacterKeeper>().characters) {
			Debug.Log("Comparing to " + character.character);
			if (character.character == FindObjectOfType<FightManager>().currentPlayer.character) {
				return character.GetProfBonus();
			} 
		}
		Debug.LogWarning("AttackOptionsPrefab doesn't recognize player to get ProfBonus");
		return 0;
	}
}
