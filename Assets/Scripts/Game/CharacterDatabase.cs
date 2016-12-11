using UnityEngine;
using System.Collections;
using LitJson;
using System.Collections.Generic;
using System.IO;

public class CharacterDatabase : MonoBehaviour {

	public List<Character> characterDatabase = new List<Character>();
	private JsonData characterData;

	private ItemDatabase database;

	// Use this for initialization
	void Start () {
		database = FindObjectOfType<ItemDatabase>();
		characterData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Characters.json"));
		ConstructCharacterDatabase();
	}

	private void ConstructCharacterDatabase() {
		for (int i = 0; i < characterData.Count; i++) {
			List<Item> list = new List<Item>();
			int itemCnt = characterData[i]["startingequipment"].Count;
			for (int n = 0; n < itemCnt; n++) {
				list.Add(database.itemDatabase[(int)characterData[i]["startingequipment"][n]]);
			}
			characterDatabase.Add(new Character((int)characterData[i]["id"],
				characterData[i]["name"].ToString(),
				characterData[i]["class"].ToString(),
				characterData[i]["race"].ToString(),
				(int)characterData[i]["speed"],
				(int)characterData[i]["basehp"],
				(int)characterData[i]["hitdice"],
				(int)characterData[i]["baseabilityscores"]["str"],
				(int)characterData[i]["baseabilityscores"]["dex"],
				(int)characterData[i]["baseabilityscores"]["con"],
				(int)characterData[i]["baseabilityscores"]["int"],
				(int)characterData[i]["baseabilityscores"]["wis"],
				(int)characterData[i]["baseabilityscores"]["cha"],
				(bool)characterData[i]["throwprofs"]["str"],
				(bool)characterData[i]["throwprofs"]["dex"],
				(bool)characterData[i]["throwprofs"]["con"],
				(bool)characterData[i]["throwprofs"]["int"],
				(bool)characterData[i]["throwprofs"]["wis"],
				(bool)characterData[i]["throwprofs"]["cha"],
				list));
		}
	}

	public Character FetchCharacterByID (int id){
		for (int i = 0; i < characterDatabase.Count; i++) {
			if (characterDatabase[i].Id == id) {
				return characterDatabase[i];
			}
		}
		return null;
	}

}

public class Character {

	public int Id {	get;set;}
	public string Name {get;set;}
	public string Class {get;set;}
	public string Race {get;set;}
	public int Speed {get;set;}
	public int BaseHp {get;set;}
	public int HitDice {get;set;}
	public int StrScore {get;set;}
	public int DexScore {get;set;}
	public int ConScore {get;set;}
	public int IntScore {get;set;}
	public int WisScore {get;set;}
	public int ChaScore {get;set;}
	public bool StrProf {get;set;}
	public bool DexProf {get;set;}
	public bool ConProf {get;set;}
	public bool IntProf {get;set;}
	public bool WisProf {get;set;}
	public bool ChaProf {get;set;}
	public List<Item> Equipment {get;set;}
	public int Xp {get;set;}
	public int CurrentHP {get;set;}

	public int _lvl;
	public int _profBonus;
	public int _ac;
	public int _strModifier;
	public int _dexModifier;
	public int _conModifier;
	public int _intModifier;
	public int _wisModifier;
	public int _chaModifier;
	public int _strThrowMod;
	public int _dexThrowMod;
	public int _conThrowMod;
	public int _intThrowMod;
	public int _wisThrowMod;
	public int _chaThrowMod;

	public Character (int id, string cname, string cclass, string race, int speed, int basehp, int hitdice,
		int strscore, int dexscore, int conscore, int intscore, int wisscore, int chascore,
		bool strprof, bool dexprof, bool conprof, bool intprof, bool wisprof, bool chaprof, List<Item> startingequipment) {
		this.Id = id;
		this.Name = cname;
		this.Class = cclass;
		this.Race = race;
		this.Speed = speed;
		this.BaseHp = basehp;
		this.HitDice = hitdice;
		this.StrScore = strscore;
		this.DexScore = dexscore;
		this.ConScore = conscore;
		this.IntScore = intscore;
		this.WisScore = wisscore;
		this.ChaScore = chascore;
		this.StrProf = strprof;
		this.DexProf = dexprof;
		this.ConProf = conprof;
		this.IntProf = intprof;
		this.WisProf = wisprof;
		this.ChaProf = chaprof;
		this.Equipment = startingequipment;
		this.Xp = 0;
		this.CurrentHP = basehp;
	}

	public int Lvl {
		get {
			if (this.Xp < 300) {return _lvl = 1;}
			else if (this.Xp < 900) {return _lvl = 2;}
			else if (this.Xp < 2700) {return _lvl = 3;}
			else if (this.Xp < 6500) {return _lvl = 4;}
			else if (this.Xp < 14000) {return _lvl = 5;}
			else if (this.Xp < 23000) {return _lvl = 6;}
			else if (this.Xp < 34000) {return _lvl = 7;}
			else if (this.Xp < 48000) {return _lvl = 8;}
			else if (this.Xp < 64000) {return _lvl = 9;}
			else if (this.Xp < 85000) {return _lvl = 10;}
			else if (this.Xp < 100000) {return _lvl = 11;}
			else if (this.Xp < 120000) {return _lvl = 12;}
			else if (this.Xp < 140000) {return _lvl = 13;}
			else if (this.Xp < 165000) {return _lvl = 14;}
			else if (this.Xp < 195000) {return _lvl = 15;}
			else if (this.Xp < 225000) {return _lvl = 16;}
			else if (this.Xp < 265000) {return _lvl = 17;}
			else if (this.Xp < 305000) {return _lvl = 18;}
			else if (this.Xp < 355000) {return _lvl = 19;}
			else {return _lvl = 20;}
		}
	}

	public int ProfBonus {
		get {
			if (this.Xp < 6500) {return _profBonus = 2;}
			else if (this.Xp < 48000) {return _profBonus = 3;}
			else if (this.Xp < 120000) {return _profBonus = 4;}
			else if (this.Xp < 225000) {return _profBonus = 5;}
			else {return _profBonus = 6;}
		}
	}

	public int Ac {
		get {
			int ac = 10;
			foreach(Item item in this.Equipment){
				if(item.Itemtype == "Armor" || item.Itemtype == "Shield") {
					ac += item.Ac;
				}
			}
			return ac;
		}
	}

	public void AddXp (int amount) {
		this.Xp += amount;
	}

	public void AddEquipment (Item item) {
		this.Equipment.Add(item);
	}

	public void RemoveEquipment (int itemId) {
		foreach(Item item in this.Equipment){
			if (itemId == item.ID){
				this.Equipment.Remove(item);
				break;
			}
		}
	}

	public void Heal(int points){
		this.CurrentHP = Mathf.Min(this.BaseHp,this.CurrentHP + points);
	}

	public void Hurt(int points){
		this.CurrentHP -= points;
	}

	public int StrModifier {
		get {
			return DetermineModifier(this.StrScore);
		}
	}

	public int DexModifier {
		get {
			return DetermineModifier(this.DexScore);
		}
	}

	public int ConModifier {
		get {
			return DetermineModifier(this.ConScore);
		}
	}

	public int IntModifier {
		get {
			return DetermineModifier(this.IntScore);
		}
	}

	public int WisModifier {
		get {
			return DetermineModifier(this.WisScore);
		}
	}

	public int ChaModifier {
		get {
			return DetermineModifier(this.ChaScore);
		}
	}

	private int DetermineModifier(int score) {
		if (score < 2) {return -5;}
		else if (score < 4) {return -4;}
		else if (score < 6) { return -3;}
		else if (score < 8) {return -2;}
		else if (score < 10) {return -1;}
		else if (score < 12) {return 0;}
		else if (score < 14) {return 1;}
		else if (score < 16) {return 2;}
		else if (score < 18) {return 3;}
		else if (score < 20) {return 4;}
		else if (score < 22) {return 5;}
		else if (score < 24) {return 6;}
		else if (score < 26) {return 7;}
		else if (score < 28) {return 8;}
		else if (score < 30) {return 9;}
		else {return 10;}
	}

	public int StrThrowMod {
		get {
			if (this.StrProf){return this.StrModifier + this.ProfBonus;}
			else {return this.StrModifier;}
		}
	}

	public int DexThrowMod {
		get {
			if (this.DexProf){return this.DexModifier + this.ProfBonus;}
			else {return this.DexModifier;}
		}
	}

	public int ConThrowMod {
		get {
			if (this.ConProf){return this.ConModifier + this.ProfBonus;}
			else {return this.ConModifier;}
		}
	}

	public int IntThrowMod {
		get {
			if (this.IntProf){return this.IntModifier + this.ProfBonus;}
			else {return this.IntModifier;}
		}
	}

	public int WisThrowMod {
		get {
			if (this.WisProf){return this.WisModifier + this.ProfBonus;}
			else {return this.WisModifier;}
		}
	}

	public int ChaThrowMod {
		get {
			if (this.ChaProf){return this.ChaModifier + this.ProfBonus;}
			else {return this.ChaModifier;}
		}
	}
		
}