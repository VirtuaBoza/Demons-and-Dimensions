using UnityEngine;
using System.Collections;
using System;

public enum AbilityName {Str,Dex,Con,Int,Wis,Cha}

public class AbilityScore {
	public int BaseValue {get;set;}
	public bool Proficient {get;set;}
	public AbilityScore () {
		BaseValue = 0;
		Proficient = false;
	}
}

public struct ModifyingSkill {
	public AbilityScore skill;
	public int change;
}

public class BaseCharacter : MonoBehaviour {

	public string characterName;
	public int lvl;
	public int xp;
	public int profBonus;

	private AbilityScore[] skills;

	void Awake() {
		characterName = string.Empty;
		lvl = 1;
		xp = 0;
		profBonus = 2;
		skills = new AbilityScore[Enum.GetValues(typeof(AbilityName)).Length];
	}

	public void AddXp (int amount) {
		xp += amount;
		CalculateLevelAndProfBonus();
	}

	public void CalculateLevelAndProfBonus () {
		if (xp < 300) {lvl = 1;}
		else if (xp < 900) {lvl = 2;}
		else if (xp < 2700) {lvl = 3;}
		else if (xp < 6500) {lvl = 4;}
		else if (xp < 14000) {lvl = 5;}
		else if (xp < 23000) {lvl = 6;}
		else if (xp < 34000) {lvl = 7;}
		else if (xp < 48000) {lvl = 8;}
		else if (xp < 64000) {lvl = 9;}
		else if (xp < 85000) {lvl = 10;}
		else if (xp < 100000) {lvl = 11;}
		else if (xp < 120000) {lvl = 12;}
		else if (xp < 140000) {lvl = 13;}
		else if (xp < 165000) {lvl = 14;}
		else if (xp < 195000) {lvl = 15;}
		else if (xp < 225000) {lvl = 16;}
		else if (xp < 265000) {lvl = 17;}
		else if (xp < 305000) {lvl = 18;}
		else if (xp < 355000) {lvl = 19;}
		else {lvl = 20;}

		if (lvl < 5) {profBonus = 2;}
		else if (lvl < 9) {profBonus = 3;}
		else if (lvl < 13) {profBonus = 4;}
		else if (lvl < 17) {profBonus = 5;}
		else {profBonus = 6;}
	}

	private void SetupSkills() {
		for (int i = 0; i < skills.Length; i++) {
			skills[i] = new AbilityScore();
		}
	}

}
