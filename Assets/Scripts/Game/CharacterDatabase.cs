using UnityEngine;
using System.Collections;
using LitJson;
using System.Collections.Generic;
using System.IO;

public class CharacterDatabase : MonoBehaviour {

	private List<Character> characterDatabase = new List<Character>();
	private JsonData characterData;

	// Use this for initialization
	void Start () {
		characterData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Characters.json"));
		ConstructCharacterDatabase();
	}

	private void ConstructCharacterDatabase() {
		for (int i = 0; i < characterData.Count; i++) {
			List<int> list = new List<int>();
			int itemCnt = characterData[i]["startingequipment"].Count;
			for (int n = 0; n < itemCnt; n++) {
				list.Add((int)characterData[i]["startingequipment"][n]);
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
	public List<int> StartingEquipment {get;set;}

	public Character (int id, string cname, string cclass, string race, int speed, int basehp, int hitdice,
		int strscore, int dexscore, int conscore, int intscore, int wisscore, int chascore,
		bool strprof, bool dexprof, bool conprof, bool intprof, bool wisprof, bool chaprof, List<int> startingequipment) {
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
		this.StartingEquipment = startingequipment;
	}
}