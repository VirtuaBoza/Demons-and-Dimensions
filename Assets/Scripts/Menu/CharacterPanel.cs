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
		nameText.text = characters[currentCharacter].Name;
		lvlText.text = "Lvl " + characters[currentCharacter].GetLvl().ToString();
		classText.text = characters[currentCharacter].Class;
		xpText.text = characters[currentCharacter].Xp.ToString() + " XP";
		hpText.text = characters[currentCharacter].CurrentHP.ToString();
		maxHpText.text = "/" + characters[currentCharacter].BaseHp.ToString();
		acText.text = characters[currentCharacter].GetAc().ToString();
		profText.text = "+" + characters[currentCharacter].GetProfBonus().ToString();
		speedText.text = characters[currentCharacter].Speed.ToString() + "ft";

		strModText.text = DisplayAbilityScoreModifier(characters[currentCharacter], ABILITY.Str);
		dexModText.text = DisplayAbilityScoreModifier(characters[currentCharacter], ABILITY.Dex);
		conModText.text = DisplayAbilityScoreModifier(characters[currentCharacter], ABILITY.Con);
		intModText.text = DisplayAbilityScoreModifier(characters[currentCharacter], ABILITY.Int);
		wisModText.text = DisplayAbilityScoreModifier(characters[currentCharacter], ABILITY.Wis);
		chaModText.text = DisplayAbilityScoreModifier(characters[currentCharacter], ABILITY.Cha);

		strScoreText.text = characters[currentCharacter].abilityScoreDictionary[ABILITY.Str].ToString();
		dexScoreText.text = characters[currentCharacter].abilityScoreDictionary[ABILITY.Dex].ToString();
		conScoreText.text = characters[currentCharacter].abilityScoreDictionary[ABILITY.Con].ToString();
		intScoreText.text = characters[currentCharacter].abilityScoreDictionary[ABILITY.Int].ToString();
		wisScoreText.text = characters[currentCharacter].abilityScoreDictionary[ABILITY.Wis].ToString();
		chaScoreText.text = characters[currentCharacter].abilityScoreDictionary[ABILITY.Cha].ToString();

		strThrowModText.text = DisplayThrowModifier(characters[currentCharacter], ABILITY.Str);
		dexThrowModText.text = DisplayThrowModifier(characters[currentCharacter], ABILITY.Dex);
		conThrowModText.text = DisplayThrowModifier(characters[currentCharacter], ABILITY.Con);
		intThrowModText.text = DisplayThrowModifier(characters[currentCharacter], ABILITY.Int);
		wisThrowModText.text = DisplayThrowModifier(characters[currentCharacter], ABILITY.Wis);
		chaThrowModText.text = DisplayThrowModifier(characters[currentCharacter], ABILITY.Cha);

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
