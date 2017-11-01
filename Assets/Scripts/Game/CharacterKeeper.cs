using System.Collections.Generic;
using UnityEngine;

public class CharacterKeeper : MonoBehaviour
{
    public Dictionary<PlayerCharacter, Character> characters = new Dictionary<PlayerCharacter, Character>();

    void Start()
    {
        CharacterDatabase database = GetComponent<CharacterDatabase>();
        for (int i = 0; i < database.characterDatabase.Count; i++)
        {
            characters.Add(database.FetchCharacterByID(i).character, database.FetchCharacterByID(i));
        }
    }
}
