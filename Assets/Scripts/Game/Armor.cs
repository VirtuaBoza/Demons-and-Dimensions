using System;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Item, IEquipable
{
    private string spriteSheetName;
    private string equipType;

    private int armorClass;
    private bool dexModifierIsCappedAt2;
    private bool disadvantagesStealth;
    private bool isModifiedByDex;
    private int strengthRequirement;

    public Armor(int id, string itemType, string title, string spriteName,
        string spriteSheetName, string equipType,
        int armorClass, bool dexModifierIsCappedAt2, bool disadvantagesStealth, bool isModifiedByDex, int strengthRequirement) : 
        base (id, itemType, title, spriteName)
    {
        this.spriteSheetName = spriteSheetName;
        this.equipType = equipType;

        this.armorClass = armorClass;
        this.dexModifierIsCappedAt2 = dexModifierIsCappedAt2;
        this.disadvantagesStealth = disadvantagesStealth;
        this.isModifiedByDex = isModifiedByDex;
        this.strengthRequirement = strengthRequirement;
    }

    public Dictionary<AnimationType, AnimationClip> AnimClipDictionary
    {
        get
        {
            return AnimationGenerator.CreateAnimationClips(spriteSheetName);
        }
    }

    public EquipType EquipType
    {
        get
        {
            if (!Enum.IsDefined(typeof(EquipType), equipType))
            {
                Debug.LogWarning("Armor constructor did not recognize equipType.");
                return EquipType.None;
            }
            else
            {
                return (EquipType)Enum.Parse(typeof(EquipType), equipType, true);
            }
        }
    }

    public int ArmorClass { get { return armorClass; } }
    public bool DexModifierIsCappedAt2 { get { return dexModifierIsCappedAt2; } }
    public bool DisadvantagesStealth { get { return disadvantagesStealth; } }
    public bool IsModifiedByDex { get { return isModifiedByDex; } }
    public int StrengthRequirement { get { return strengthRequirement; } }

}
