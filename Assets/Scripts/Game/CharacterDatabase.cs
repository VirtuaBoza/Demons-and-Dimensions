using LitJson;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CharacterDatabase : MonoBehaviour
{
    public List<PlayerCharacter> CharacterList { get { return characterList; } }
    

    private JsonData characterData;
    private List<PlayerCharacter> characterList = new List<PlayerCharacter>();

    void Start()
    {
        characterData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Characters.json"));
        ConstructCharacterList();
    }

    private void ConstructCharacterList()
    {
        for (int i = 0; i < characterData.Count; i++)
        {
            characterList.Add(new PlayerCharacter(
                characterData[i]["name"].ToString(),
                characterData[i]["spritesheetname"].ToString(),
                characterData[i]["animationcategories"].ToString(),
                characterData[i]["class"].ToString(),
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
                (bool)characterData[i]["throwprofs"]["cha"]));
        }
    }

    public Dictionary<PlayerCharacterName, PlayerCharacter> CharacterDictionary
    {
        get
        {
            var characterDictionary = new Dictionary<PlayerCharacterName, PlayerCharacter>();
            foreach (PlayerCharacter character in CharacterList)
            {
                characterDictionary.Add(character.PlayerCharacterName, character);
            }
            return characterDictionary;
        }
    }
}