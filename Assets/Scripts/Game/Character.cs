using System;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerCharacter
{
    Crystal,
    Teddy,
    Hunter,
    Damien
}

public enum AbilityType
{
    Str,
    Dex,
    Con,
    Int,
    Wis,
    Cha
}

public class Character
{
    public int Id { get; set; }
    public string Name { get; set; }
    public PlayerCharacter PlayerCharacter { get; set; }
    public string Class { get; set; }
    public string Race { get; set; }
    public int Speed { get; set; }
    public int BaseHp { get; set; }
    public int HitDice { get; set; }
    public Dictionary<AbilityType, int> AbilityScoreDictionary { get; set; }
    public Dictionary<AbilityType, bool> AbilityProfDictionary { get; set; }
    public List<Item> Equipment { get; set; }
    public int Xp { get; set; }
    public int CurrentHP { get; set; }

    public Character(int id, string characterName, string characterClass, string race, int speed, int basehp, int hitdice,
        int strscore, int dexscore, int conscore, int intscore, int wisscore, int chascore,
        bool strprof, bool dexprof, bool conprof, bool intprof, bool wisprof, bool chaprof, List<Item> startingequipment)
    {
        Id = id;
        Name = characterName;
        
        // Leaving below code commented out bc I'm not sure if my handmade 'TryParse' below it works yet
        /*
        if (characterName.ToLower().Contains("crystal")) PlayerCharacter = PlayerCharacter.Crystal;
        else if (characterName.ToLower().Contains("damien")) PlayerCharacter = PlayerCharacter.Damien;
        else if (characterName.ToLower().Contains("hunter")) PlayerCharacter = PlayerCharacter.Hunter;
        else if (characterName.ToLower().Contains("teddy")) PlayerCharacter = PlayerCharacter.Teddy;
        else Debug.LogWarning("Character constructor doesn't recognize PlayerCharacter");
        */
        if(!Enum.IsDefined(typeof(PlayerCharacter),characterName))
        {
            Debug.Log("Non PlayerCharacter created");
        }
        else
        {
            PlayerCharacter = (PlayerCharacter)Enum.Parse(typeof(PlayerCharacter), characterName);
        }

        Class = characterClass;
        Race = race;
        Speed = speed;
        BaseHp = basehp;
        HitDice = hitdice;

        AbilityScoreDictionary = new Dictionary<AbilityType, int>();
        AbilityScoreDictionary[AbilityType.Str] = strscore;
        AbilityScoreDictionary[AbilityType.Dex] = dexscore;
        AbilityScoreDictionary[AbilityType.Con] = conscore;
        AbilityScoreDictionary[AbilityType.Int] = intscore;
        AbilityScoreDictionary[AbilityType.Wis] = wisscore;
        AbilityScoreDictionary[AbilityType.Cha] = chascore;

        AbilityProfDictionary = new Dictionary<AbilityType, bool>();
        AbilityProfDictionary[AbilityType.Str] = strprof;
        AbilityProfDictionary[AbilityType.Dex] = dexprof;
        AbilityProfDictionary[AbilityType.Con] = conprof;
        AbilityProfDictionary[AbilityType.Int] = intprof;
        AbilityProfDictionary[AbilityType.Wis] = wisprof;
        AbilityProfDictionary[AbilityType.Cha] = chaprof;
        Equipment = startingequipment;
        Xp = 0;
        CurrentHP = basehp;
    }

    public int GetLvl()
    {
        if (Xp < 300) return 1;
        else if (Xp < 900) return 2;
        else if (Xp < 2700) return 3;
        else if (Xp < 6500) return 4;
        else if (Xp < 14000) return 5;
        else if (Xp < 23000) return 6;
        else if (Xp < 34000) return 7;
        else if (Xp < 48000) return 8;
        else if (Xp < 64000) return 9;
        else if (Xp < 85000) return 10;
        else if (Xp < 100000) return 11;
        else if (Xp < 120000) return 12;
        else if (Xp < 140000) return 13;
        else if (Xp < 165000) return 14;
        else if (Xp < 195000) return 15;
        else if (Xp < 225000) return 16;
        else if (Xp < 265000) return 17;
        else if (Xp < 305000) return 18;
        else if (Xp < 355000) return 19;
        else return 20;
    }

    public int GetProfBonus()
    {
        if (Xp < 6500) return 2;
        else if (Xp < 48000) return 3;
        else if (Xp < 120000) return 4;
        else if (Xp < 225000) return 5;
        else return 6;
    }

    public int GetAc()
    {
        int ac = 10;
        bool dexMods = true;
        bool dexModsMax2 = false;
        foreach (Item item in Equipment)
        {
            if (item.Itemtype == ItemType.Armor || item.Itemtype == ItemType.Shield)
            {
                ac += item.Ac;
                if (!item.Dexmodifies) { dexMods = false; }
                else if (item.Modifiermax2) { dexModsMax2 = true; }
                if (item.Str != 0)
                {
                    if (GetLvl() < item.Str) { Speed -= 10; }
                }
            }
        }
        if (dexMods)
        {
            if (dexModsMax2) ac += Mathf.Min(GetAbilityScoreModifier(AbilityType.Dex), 2);
            else ac += GetAbilityScoreModifier(AbilityType.Dex);
        }
        return ac;
    }

    public void AddXp(int amount)
    {
        Xp += amount;
    }

    public void AddEquipment(Item item)
    {
        Equipment.Add(item);
    }

    public void RemoveEquipment(int itemId)
    {
        foreach (Item item in Equipment)
        {
            if (itemId == item.ID)
            {
                Equipment.Remove(item);
                break;
            }
        }
    }

    public void Heal(int points)
    {
        CurrentHP = Mathf.Min(BaseHp, CurrentHP + points);
    }

    public void Hurt(int points)
    {
        CurrentHP -= points;
    }

    public int GetAbilityScoreModifier(AbilityType ability)
    {
        return DetermineModifier(AbilityScoreDictionary[ability]);
    }

    private int DetermineModifier(int score)
    {
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

    public int GetThrowMod(AbilityType ability)
    {
        if (AbilityProfDictionary[ability]) return GetAbilityScoreModifier(ability) + GetProfBonus();
        else return GetAbilityScoreModifier(ability);
    }
}
