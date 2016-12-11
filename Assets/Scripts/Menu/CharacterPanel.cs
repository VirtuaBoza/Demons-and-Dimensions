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
	public Toggle blueToggle, orangeToggle, greenToggle, redToggle;

	public List<Character> characters = new List<Character>();


	CharacterDatabase database;

	void Start () {
		
		database = FindObjectOfType<CharacterDatabase>();
		for (int i = 0; i < database.characterDatabase.Count; i++) {
			characters.Add(database.FetchCharacterByID(i));
		}

		Toggle[] toggles = new Toggle[] {blueToggle, orangeToggle, greenToggle, redToggle};
		toggles[(int)currentCharacter].isOn = true;

	}

	public void SwitchCharacter (int index) {

		currentCharacter = (CHARACTER)index;

		nameText.text = characters[index].Name;
		lvlText.text = "Lvl " + characters[index].Lvl.ToString();
		classText.text = characters[index].Class;
		xpText.text = characters[index].Xp.ToString() + " XP";
		hpText.text = characters[index].CurrentHP.ToString();
		maxHpText.text = "/" + characters[index].BaseHp.ToString();
		acText.text = characters[index].Ac.ToString();
		profText.text = "+" + characters[index].ProfBonus.ToString();
		speedText.text = characters[index].Speed.ToString() + "ft";
		if (characters[index].StrModifier < 0) {strModText.text = characters[index].StrModifier.ToString();}
		else {strModText.text = "+" + characters[index].StrModifier.ToString();}
		if (characters[index].DexModifier < 0) {dexModText.text = characters[index].DexModifier.ToString();}
		else {dexModText.text = "+" + characters[index].DexModifier.ToString();}
		if (characters[index].ConModifier < 0) {conModText.text = characters[index].ConModifier.ToString();}
		else {conModText.text = "+" + characters[index].ConModifier.ToString();}
		if (characters[index].IntModifier < 0) {intModText.text = characters[index].IntModifier.ToString();}
		else {intModText.text = "+" + characters[index].IntModifier.ToString();}
		if (characters[index].WisModifier < 0) {wisModText.text = characters[index].WisModifier.ToString();}
		else {wisModText.text = "+" + characters[index].WisModifier.ToString();}
		if (characters[index].ChaModifier < 0) {chaModText.text = characters[index].ChaModifier.ToString();}
		else {chaModText.text = "+" + characters[index].ChaModifier.ToString();}
		strScoreText.text = characters[index].StrScore.ToString();
		dexScoreText.text = characters[index].DexScore.ToString();
		conScoreText.text = characters[index].ConScore.ToString();
		intScoreText.text = characters[index].IntScore.ToString();
		wisScoreText.text = characters[index].WisScore.ToString();
		chaScoreText.text = characters[index].ChaScore.ToString();
		if (characters[index].StrThrowMod < 0) {strThrowModText.text = characters[index].StrThrowMod.ToString();}
		else {strThrowModText.text = "+" + characters[index].StrThrowMod.ToString();}
		if (characters[index].DexThrowMod < 0) {dexThrowModText.text = characters[index].DexThrowMod.ToString();}
		else {dexThrowModText.text = "+" + characters[index].DexThrowMod.ToString();}
		if (characters[index].ConThrowMod < 0) {conThrowModText.text = characters[index].ConThrowMod.ToString();}
		else {conThrowModText.text = "+" + characters[index].ConThrowMod.ToString();}
		if (characters[index].IntThrowMod < 0) {intThrowModText.text = characters[index].IntThrowMod.ToString();}
		else {intThrowModText.text = "+" + characters[index].IntThrowMod.ToString();}
		if (characters[index].WisThrowMod < 0) {wisThrowModText.text = characters[index].WisThrowMod.ToString();}
		else {wisThrowModText.text = "+" + characters[index].WisThrowMod.ToString();}
		if (characters[index].ChaThrowMod < 0) {chaThrowModText.text = characters[index].ChaThrowMod.ToString();}
		else {chaThrowModText.text = "+" + characters[index].ChaThrowMod.ToString();}

	}

}
