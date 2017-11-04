using LitJson;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CharacterDatabase : MonoBehaviour
{
    public List<Character> characterList = new List<Character>();
    public Dictionary<PlayerCharacter, Character> CharacterDictionary { get; set; }

    private JsonData characterData;
    private ItemDatabase itemDatabase;

    void Start()
    {
        itemDatabase = FindObjectOfType<ItemDatabase>();
        characterData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Characters.json"));
        ConstructCharacterList();
        ConstructCharacterDisciontary();
    }

    private void ConstructCharacterList()
    {
        for (int i = 0; i < characterData.Count; i++)
        {
            List<Item> characterItems = new List<Item>();
            int itemCnt = characterData[i]["startingequipment"].Count;
            for (int n = 0; n < itemCnt; n++)
            {
                characterItems.Add(itemDatabase.allItems[(int)characterData[i]["startingequipment"][n]]);
            }
            characterList.Add(new Character((int)characterData[i]["id"],
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
                characterItems));
        }
    }

    private void ConstructCharacterDisciontary()
    {
        CharacterDictionary = new Dictionary<PlayerCharacter, Character>();
        foreach (Character character in characterList)
        {
            CharacterDictionary.Add(character.PlayerCharacter, character);
        }
    }

    public Character FetchCharacterByID(int id)
    {
        for (int i = 0; i < characterList.Count; i++)
        {
            if (characterList[i].Id == id)
            {
                return characterList[i];
            }
        }
        return null;
    }
}