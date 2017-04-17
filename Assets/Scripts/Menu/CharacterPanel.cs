using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CharacterPanel : MonoBehaviour {

	public Text nameText, lvlText, classText, xpText, hpText, maxHpText, acText, profText, speedText,
	strModText, dexModText, conModText, intModText, wisModText, chaModText,
	strScoreText, dexScoreText, conScoreText, intScoreText, wisScoreText, chaScoreText,
	strThrowModText, dexThrowModText, conThrowModText, intThrowModText, wisThrowModText, chaThrowModText;
	public Toggle blueToggle, orangeToggle, greenToggle, redToggle;

	public List<Character> characters = new List<Character>();

	private CHARACTER currentCharacter;

	void Start () {

		characters = FindObjectOfType<CharacterKeeper>().characters;

		Toggle[] toggles = new Toggle[] {blueToggle, orangeToggle, greenToggle, redToggle};
		toggles[(int)currentCharacter].isOn = true;

	}

	public void SwitchCharacter (int index) {

		currentCharacter = (CHARACTER)index;

		nameText.text = characters[index].Name;
		lvlText.text = "Lvl " + characters[index].GetLvl().ToString();
		classText.text = characters[index].Class;
		xpText.text = characters[index].Xp.ToString() + " XP";
		hpText.text = characters[index].CurrentHP.ToString();
		maxHpText.text = "/" + characters[index].BaseHp.ToString();
		acText.text = characters[index].GetAc().ToString();
		profText.text = "+" + characters[index].GetProfBonus().ToString();
		speedText.text = characters[index].Speed.ToString() + "ft";

		strModText.text = DisplayAbilityScoreModifier(characters[index], ABILITY.Str);
		dexModText.text = DisplayAbilityScoreModifier(characters[index], ABILITY.Dex);
		conModText.text = DisplayAbilityScoreModifier(characters[index], ABILITY.Con);
		intModText.text = DisplayAbilityScoreModifier(characters[index], ABILITY.Int);
		wisModText.text = DisplayAbilityScoreModifier(characters[index], ABILITY.Wis);
		chaModText.text = DisplayAbilityScoreModifier(characters[index], ABILITY.Cha);

		strScoreText.text = characters[index].abilityScoreDictionary[ABILITY.Str].ToString();
		dexScoreText.text = characters[index].abilityScoreDictionary[ABILITY.Dex].ToString();
		conScoreText.text = characters[index].abilityScoreDictionary[ABILITY.Con].ToString();
		intScoreText.text = characters[index].abilityScoreDictionary[ABILITY.Int].ToString();
		wisScoreText.text = characters[index].abilityScoreDictionary[ABILITY.Wis].ToString();
		chaScoreText.text = characters[index].abilityScoreDictionary[ABILITY.Cha].ToString();

		strThrowModText.text = DisplayThrowModifier(characters[index], ABILITY.Str);
		dexThrowModText.text = DisplayThrowModifier(characters[index], ABILITY.Dex);
		conThrowModText.text = DisplayThrowModifier(characters[index], ABILITY.Con);
		intThrowModText.text = DisplayThrowModifier(characters[index], ABILITY.Int);
		wisThrowModText.text = DisplayThrowModifier(characters[index], ABILITY.Wis);
		chaThrowModText.text = DisplayThrowModifier(characters[index], ABILITY.Cha);

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
