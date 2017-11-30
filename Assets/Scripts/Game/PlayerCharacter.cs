using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter
{
    private string playerCharacterName;
    private string spriteSheetName;
    private string characterClass;
    private int speed;
    private int baseHP;
    private int currentHP;
    private int hitDice;
    private int strScore;
    private int dexScore;
    private int conScore;
    private int intScore;
    private int wisScore;
    private int chaScore;
    private bool strProf;
    private bool dexProf;
    private bool conProf;
    private bool intProf;
    private bool wisProf;
    private bool chaProf;
    private int xp = 0;
    private Dictionary<EquipType, IEquipable> equippedItems = new Dictionary<EquipType, IEquipable>();

    public PlayerCharacter(string characterName, string spriteSheetName, string characterClass, int speed, int baseHP, int hitDice,
        int strScore, int dexScore, int conScore, int intScore, int wisScore, int chaScore,
        bool strProf, bool dexProf, bool conProf, bool intProf, bool wisProf, bool chaProf)
    {
        playerCharacterName = characterName;
        this.spriteSheetName = spriteSheetName;
        this.characterClass = characterClass;
        this.speed = speed;
        this.baseHP = baseHP;
        currentHP = baseHP;
        this.hitDice = hitDice;
        this.strScore = strScore;
        this.dexScore = dexScore;
        this.conScore = conScore;
        this.intScore = intScore;
        this.wisScore = wisScore;
        this.chaScore = chaScore;
        this.strProf = strProf;
        this.dexProf = dexProf;
        this.conProf = conProf;
        this.intProf = intProf;
        this.wisProf = wisProf;
        this.chaProf = chaProf;
    }

    public PlayerCharacterName PlayerCharacterName
    {
        get
        {
            if (!Enum.IsDefined(typeof(PlayerCharacterName), playerCharacterName))
            {
                Debug.LogWarning("Character constructor did not recognize characterName.");
                return PlayerCharacterName.Crystal;
            }
            else
            {
                return (PlayerCharacterName)Enum.Parse(typeof(PlayerCharacterName), playerCharacterName, true);
            }
        }
    }

    public Dictionary<AnimationType, AnimationClip> AnimClipDictionary
    {
        get
        {
            return AnimationGenerator.CreateAnimationClips(spriteSheetName);
        }
    }

    public CharacterClass? Class
    {
        get
        {
            if (!Enum.IsDefined(typeof(CharacterClass), characterClass))
            {
                Debug.LogWarning("Character constructor did not recognize characterClass.");
                return null;
            }
            else
            {
                return (CharacterClass)Enum.Parse(typeof(CharacterClass), characterClass, true);
            }
        }
    }
    public int Speed { get { return speed; } } //TODO adjust speed according to equipped armor
    public int BaseHP { get { return baseHP; } }
    public DieType HitDice
    {
        get
        {
            if (!Enum.IsDefined(typeof(DieType), hitDice))
            {
                Debug.LogWarning("Character constructor did not recognize characterClass.");
                return DieType.d1;
            }
            else
            {
                return (DieType)Enum.Parse(typeof(DieType), hitDice.ToString(), true);
            }
        }
    }
    public Dictionary<AbilityType, int> AbilityScoreDictionary
    {
        get
        {
            var abilityScoreDictionary = new Dictionary<AbilityType, int>();
            abilityScoreDictionary.Add(AbilityType.Str, strScore);
            abilityScoreDictionary.Add(AbilityType.Dex, dexScore);
            abilityScoreDictionary.Add(AbilityType.Con, conScore);
            abilityScoreDictionary.Add(AbilityType.Int, intScore);
            abilityScoreDictionary.Add(AbilityType.Wis, wisScore);
            abilityScoreDictionary.Add(AbilityType.Cha, chaScore);
            return abilityScoreDictionary;
        }
    }

    public Dictionary<AbilityType, bool> AbilityProfDictionary
    {
        get
        {
            var abilityProfDictionary = new Dictionary<AbilityType, bool>();
            abilityProfDictionary.Add(AbilityType.Str, strProf);
            abilityProfDictionary.Add(AbilityType.Dex, dexProf);
            abilityProfDictionary.Add(AbilityType.Con, conProf);
            abilityProfDictionary.Add(AbilityType.Int, intProf);
            abilityProfDictionary.Add(AbilityType.Wis, wisProf);
            abilityProfDictionary.Add(AbilityType.Cha, chaProf);
            return abilityProfDictionary;
        }
    }
    public int XP { get { return xp; } }
    public int CurrentHP { get { return currentHP; } }
    public Dictionary<EquipType, IEquipable> EquippedItems
    {
        get
        {
            return equippedItems;
        }
        set
        {
            equippedItems = value;
        }
    }

    public int Lvl
    {
        get
        {
            if (XP < 300) return 1;
            else if (XP < 900) return 2;
            else if (XP < 2700) return 3;
            else if (XP < 6500) return 4;
            else if (XP < 14000) return 5;
            else if (XP < 23000) return 6;
            else if (XP < 34000) return 7;
            else if (XP < 48000) return 8;
            else if (XP < 64000) return 9;
            else if (XP < 85000) return 10;
            else if (XP < 100000) return 11;
            else if (XP < 120000) return 12;
            else if (XP < 140000) return 13;
            else if (XP < 165000) return 14;
            else if (XP < 195000) return 15;
            else if (XP < 225000) return 16;
            else if (XP < 265000) return 17;
            else if (XP < 305000) return 18;
            else if (XP < 355000) return 19;
            else return 20;
        }
    }

    public int ProfBonus
    {
        get
        {
            if (XP < 6500) return 2;
            else if (XP < 48000) return 3;
            else if (XP < 120000) return 4;
            else if (XP < 225000) return 5;
            else return 6;
        }
    }

    public int ArmorClass
    {
        get
        {
            int ac = 10;
            bool dexMods = true;
            bool dexModsMax2 = false;
            foreach (Armor armorItem in EquippedItems.Values)
            {
                ac += armorItem.ArmorClass;
                if (!armorItem.IsModifiedByDex)
                {
                    dexMods = false;
                }
                else if (armorItem.DexModifierIsCappedAt2)
                {
                    dexModsMax2 = true;
                }
            }
            if (dexMods)
            {
                if (dexModsMax2) ac += Mathf.Min(GetAbilityScoreModifier(AbilityType.Dex), 2);
                else ac += GetAbilityScoreModifier(AbilityType.Dex);
            }
            return ac;
        }
    }

    public void AddXp(int amount)
    {
        xp += amount;
    }

    public void AddEquipment(IEquipable equipment)
    {
        ICollection<EquipType> equippedTypes = equippedItems.Keys;
        if (equippedTypes.Contains(equipment.EquipType))
        {
            equippedItems[equipment.EquipType] = equipment;
        }
        else
        {
            equippedItems.Add(equipment.EquipType, equipment);
        }
    }

    public void RemoveEquipment(IEquipable equipment)
    {
        equippedItems.Remove(equipment.EquipType);
    }

    public void Heal(int points)
    {
        currentHP = Mathf.Min(baseHP, currentHP + points);
    }

    public void Hurt(int points)
    {
        currentHP -= points;
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
        if (AbilityProfDictionary[ability]) return GetAbilityScoreModifier(ability) + ProfBonus;
        else return GetAbilityScoreModifier(ability);
    }
}
