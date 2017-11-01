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
    public PlayerCharacter character;
    public string Class { get; set; }
    public string Race { get; set; }
    public int Speed { get; set; }
    public int BaseHp { get; set; }
    public int HitDice { get; set; }
    public Dictionary<AbilityType, int> abilityScoreDictionary = new Dictionary<AbilityType, int>();
    public Dictionary<AbilityType, bool> abilityProfDictionary = new Dictionary<AbilityType, bool>();
    public List<Item> Equipment { get; set; }
    public int Xp { get; set; }
    public int CurrentHP { get; set; }

    public Character(int id, string characterName, string characterClass, string race, int speed, int basehp, int hitdice,
        int strscore, int dexscore, int conscore, int intscore, int wisscore, int chascore,
        bool strprof, bool dexprof, bool conprof, bool intprof, bool wisprof, bool chaprof, List<Item> startingequipment)
    {
        this.Id = id;
        this.Name = characterName;
        if (characterName.ToLower().Contains("crystal")) character = PlayerCharacter.Crystal;
        else if (characterName.ToLower().Contains("damien")) character = PlayerCharacter.Damien;
        else if (characterName.ToLower().Contains("hunter")) character = PlayerCharacter.Hunter;
        else if (characterName.ToLower().Contains("teddy")) character = PlayerCharacter.Teddy;
        else Debug.LogWarning("Character constructor doesn't recognize PlayerCharacter");
        this.Class = characterClass;
        this.Race = race;
        this.Speed = speed;
        this.BaseHp = basehp;
        this.HitDice = hitdice;
        abilityScoreDictionary[AbilityType.Str] = strscore;
        abilityScoreDictionary[AbilityType.Dex] = dexscore;
        abilityScoreDictionary[AbilityType.Con] = conscore;
        abilityScoreDictionary[AbilityType.Int] = intscore;
        abilityScoreDictionary[AbilityType.Wis] = wisscore;
        abilityScoreDictionary[AbilityType.Cha] = chascore;
        abilityProfDictionary[AbilityType.Str] = strprof;
        abilityProfDictionary[AbilityType.Dex] = dexprof;
        abilityProfDictionary[AbilityType.Con] = conprof;
        abilityProfDictionary[AbilityType.Int] = intprof;
        abilityProfDictionary[AbilityType.Wis] = wisprof;
        abilityProfDictionary[AbilityType.Cha] = chaprof;
        this.Equipment = startingequipment;
        this.Xp = 0;
        this.CurrentHP = basehp;
    }

    public int GetLvl()
    {
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

    public int GetProfBonus()
    {
        if (this.Xp < 6500) return 2;
        else if (this.Xp < 48000) return 3;
        else if (this.Xp < 120000) return 4;
        else if (this.Xp < 225000) return 5;
        else return 6;
    }

    public int GetAc()
    {
        int ac = 10;
        bool dexMods = true;
        bool dexModsMax2 = false;
        foreach (Item item in this.Equipment)
        {
            if (item.Itemtype == ItemType.Armor || item.Itemtype == ItemType.Shield)
            {
                ac += item.Ac;
                if (!item.Dexmodifies) { dexMods = false; }
                else if (item.Modifiermax2) { dexModsMax2 = true; }
                if (item.Str != 0)
                {
                    if (this.GetLvl() < item.Str) { this.Speed -= 10; }
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
        this.Xp += amount;
    }

    public void AddEquipment(Item item)
    {
        this.Equipment.Add(item);
    }

    public void RemoveEquipment(int itemId)
    {
        foreach (Item item in this.Equipment)
        {
            if (itemId == item.ID)
            {
                this.Equipment.Remove(item);
                break;
            }
        }
    }

    public void Heal(int points)
    {
        this.CurrentHP = Mathf.Min(this.BaseHp, this.CurrentHP + points);
    }

    public void Hurt(int points)
    {
        this.CurrentHP -= points;
    }

    public int GetAbilityScoreModifier(AbilityType ability)
    {
        return DetermineModifier(abilityScoreDictionary[ability]);
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
        if (abilityProfDictionary[ability]) return GetAbilityScoreModifier(ability) + GetProfBonus();
        else return GetAbilityScoreModifier(ability);
    }
}
