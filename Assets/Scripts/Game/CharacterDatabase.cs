using LitJson;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CharacterDatabase : MonoBehaviour {

	public List<Character> characterDatabase = new List<Character>();

	private JsonData characterData;
	private ItemDatabase itemDatabase;

	void Start () {
		itemDatabase = FindObjectOfType<ItemDatabase>();
		characterData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Characters.json"));
		ConstructCharacterDatabase();
	}

	private void ConstructCharacterDatabase() {
		for (int i = 0; i < characterData.Count; i++) {
			List<Item> list = new List<Item>();
			int itemCnt = characterData[i]["startingequipment"].Count;
			for (int n = 0; n < itemCnt; n++) {
				list.Add(itemDatabase.allItems[(int)characterData[i]["startingequipment"][n]]);
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