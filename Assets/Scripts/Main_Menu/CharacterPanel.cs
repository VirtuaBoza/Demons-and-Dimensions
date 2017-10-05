using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CharacterPanel : MonoBehaviour {

	public Text nameText, lvlText, classText, xpText, hpText, maxHpText, acText, profText, speedText,
	strModText, dexModText, conModText, intModText, wisModText, chaModText,
	strScoreText, dexScoreText, conScoreText, intScoreText, wisScoreText, chaScoreText,
	strThrowModText, dexThrowModText, conThrowModText, intThrowModText, wisThrowModText, chaThrowModText;
	public CHARACTER currentCharacter;
	public Dictionary<CHARACTER,Character> characters = new Dictionary<CHARACTER, Character>();

	void Start () {
		characters = FindObjectOfType<CharacterKeeper>().characters;
	}

	public void SwitchCharacter (int index) {
		switch (index) {
		case 0:
			currentCharacter = CHARACTER.Crystal;
			break;
		case 1:
			currentCharacter = CHARACTER.Teddy;
			break;
		case 2:
			currentCharacter = CHARACTER.Hunter;
			break;
		case 3:
			currentCharacter = CHARACTER.Damien;
			break;
		default:
			Debug.LogWarning("Trying to switch to a character in CharacterPanel that is out of range.");
			break;
		}
		UpdateCharacterStats(currentCharacter);
	}

	public void UpdateCharacterStats(CHARACTER character) {
		nameText.text = characters[character].Name;
		lvlText.text = "Lvl " + characters[character].GetLvl().ToString();
		classText.text = characters[character].Class;
		xpText.text = characters[character].Xp.ToString() + " XP";
		hpText.text = characters[character].CurrentHP.ToString();
		maxHpText.text = "/" + characters[character].BaseHp.ToString();
		acText.text = characters[character].GetAc().ToString();
		profText.text = "+" + characters[character].GetProfBonus().ToString();
		speedText.text = characters[character].Speed.ToString() + "ft";

		strModText.text = DisplayAbilityScoreModifier(characters[character], ABILITY.Str);
		dexModText.text = DisplayAbilityScoreModifier(characters[character], ABILITY.Dex);
		conModText.text = DisplayAbilityScoreModifier(characters[character], ABILITY.Con);
		intModText.text = DisplayAbilityScoreModifier(characters[character], ABILITY.Int);
		wisModText.text = DisplayAbilityScoreModifier(characters[character], ABILITY.Wis);
		chaModText.text = DisplayAbilityScoreModifier(characters[character], ABILITY.Cha);

		strScoreText.text = characters[character].abilityScoreDictionary[ABILITY.Str].ToString();
		dexScoreText.text = characters[character].abilityScoreDictionary[ABILITY.Dex].ToString();
		conScoreText.text = characters[character].abilityScoreDictionary[ABILITY.Con].ToString();
		intScoreText.text = characters[character].abilityScoreDictionary[ABILITY.Int].ToString();
		wisScoreText.text = characters[character].abilityScoreDictionary[ABILITY.Wis].ToString();
		chaScoreText.text = characters[character].abilityScoreDictionary[ABILITY.Cha].ToString();

		strThrowModText.text = DisplayThrowModifier(characters[character], ABILITY.Str);
		dexThrowModText.text = DisplayThrowModifier(characters[character], ABILITY.Dex);
		conThrowModText.text = DisplayThrowModifier(characters[character], ABILITY.Con);
		intThrowModText.text = DisplayThrowModifier(characters[character], ABILITY.Int);
		wisThrowModText.text = DisplayThrowModifier(characters[character], ABILITY.Wis);
		chaThrowModText.text = DisplayThrowModifier(characters[character], ABILITY.Cha);
	}

	private string DisplayAbilityScoreModifier(Character character, ABILITY ability) {
		string result = character.GetAbilityScoreModifier(ability).ToString();
		if (character.GetAbilityScoreModifier(ability) > 0) result = "+" + result;
		return result;
	}

	private string DisplayThrowModifier(Character character, ABILITY ability) {
		string result = character.GetThrowMod(ability).ToString();
		if (character.GetThrowMod(ability) > 0) result = "+" + result;
		return result;
	}

}
