using UnityEngine;
using System.Collections;
using LitJson;
using System.Collections.Generic;
using System.IO;

public class CharacterDatabase : MonoBehaviour {

	public List<Character> characterDatabase = new List<Character>();
	private JsonData characterData;

	private ItemDatabase database;

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

public enum ABILITY {
	Str,
	Dex,
	Con,
	Int,
	Wis,
	Cha
}

public class Character {

	public int Id {	get;set;}
	public string Name {get;set;}
	public string Class {get;set;}
	public string Race {get;set;}
	public int Speed {get;set;}
	public int BaseHp {get;set;}
	public int HitDice {get;set;}
	public Dictionary<ABILITY,int> abilityScoreDictionary = new Dictionary<ABILITY,int>();
	public Dictionary<ABILITY,bool> abilityProfDictionary = new Dictionary<ABILITY, bool>();
	public List<Item> Equipment {get;set;}
	public int Xp {get;set;}
	public int CurrentHP {get;set;}

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
		abilityScoreDictionary[ABILITY.Str] = strscore;
		abilityScoreDictionary[ABILITY.Dex] = dexscore;
		abilityScoreDictionary[ABILITY.Con] = conscore;
		abilityScoreDictionary[ABILITY.Int] = intscore;
		abilityScoreDictionary[ABILITY.Wis] = wisscore;
		abilityScoreDictionary[ABILITY.Cha] = chascore;
		abilityProfDictionary[ABILITY.Str] = strprof;
		abilityProfDictionary[ABILITY.Dex] = dexprof;
		abilityProfDictionary[ABILITY.Con] = conprof;
		abilityProfDictionary[ABILITY.Int] = intprof;
		abilityProfDictionary[ABILITY.Wis] = wisprof;
		abilityProfDictionary[ABILITY.Cha] = chaprof;
		this.Equipment = startingequipment;
		this.Xp = 0;
		this.CurrentHP = basehp;
	}

	public int GetLvl() {
		if (this.Xp < 300) return 1;
		else if (this.Xp < 900) return 2;
		else if (this.Xp < 2700) return 3;
		else if (this.Xp < 6500) return 4;
		else if (this.Xp < 14000) return 5;
		else if (this.Xp < 23000) return 6;
		else if (this.Xp < 34000) return 7;
		else if (this.Xp < 48000) return 8;
		else if (this.Xp < 64000) return 9;
		else if (this.Xp < 85000) return 10;
		else if (this.Xp < 100000) return 11;
		else if (this.Xp < 120000) return 12;
		else if (this.Xp < 140000) return 13;
		else if (this.Xp < 165000) return 14;
		else if (this.Xp < 195000) return 15;
		else if (this.Xp < 225000) return 16;
		else if (this.Xp < 265000) return 17;
		else if (this.Xp < 305000) return 18;
		else if (this.Xp < 355000) return 19;
		else return 20;
	}

	public int GetProfBonus() {
		if (this.Xp < 6500) return 2;
		else if (this.Xp < 48000) return 3;
		else if (this.Xp < 120000) return 4;
		else if (this.Xp < 225000) return 5;
		else return 6;
	}

	public int GetAc() {
		int ac = 10;
		bool dexMods = true;
		bool dexModsMax2 = false;
		foreach(Item item in this.Equipment){
			if(item.Itemtype == ITEMTYPE.Armor || item.Itemtype == ITEMTYPE.Shield) {
				ac += item.Ac;
				if(!item.Dexmodifies) {dexMods = false;}
				else if (item.Modifiermax2) {dexModsMax2 = true;}
				if(item.Str != 0){
					if(this.GetLvl() < item.Str) {this.Speed -= 10;}
				}
			}
		}
		if (dexMods){
			if (dexModsMax2) ac += Mathf.Min(GetAbilityScoreModifier(ABILITY.Dex),2);
			else ac += GetAbilityScoreModifier(ABILITY.Dex);
		}
		return ac;
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

	public int GetAbilityScoreModifier(ABILITY ability) {
		return DetermineModifier(abilityScoreDictionary[ability]);
	}

	private int DetermineModifier(int score) {
		if (score < 2) return -5;
		else if (score < 4) return -4;
		else if (score < 6) return -3;
		else if (score < 8) return -2;
		else if (score < 10) return -1;
		else if (score < 12) return 0;
		else if (score < 14) return 1;
		else if (score < 16) return 2;
		else if (score < 18) return 3;
		else if (score < 20) return 4;
		else if (score < 22) return 5;
		else if (score < 24) return 6;
		else if (score < 26) return 7;
		else if (score < 28) return 8;
		else if (score < 30) return 9;
		else return 10;
	}

	public int GetThrowMod(ABILITY ability) {
		if (abilityProfDictionary[ability]) return GetAbilityScoreModifier(ability) + GetProfBonus();
		else return GetAbilityScoreModifier(ability);
	}
		
}