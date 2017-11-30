using System;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item, IEquipable
{
    private string spriteSheetName;
    private string equipType;

    private int damageMulti;
    private int damageRange;
    private string damageType;
    private bool finesse;
    private int maxRange;
    private int range;
    private bool reach;
    private bool twoHanded;
    private string weightCategory;

    public Weapon(int id, string title, string spriteName,
        string spriteSheetName, string equipType,
        int damageMulti, int damageRange, string damageType, bool finesse, int maxRange, int range, bool reach, bool twoHanded, string weightCategory) : 
        base(id, "Weapon", title, spriteName)
    {
        this.spriteSheetName = spriteSheetName;
        this.equipType = equipType;

        this.damageMulti = damageMulti;
        this.damageRange = damageRange;
        this.damageType = damageType;
        this.finesse = finesse;
        this.maxRange = maxRange;
        this.range = range;
        this.reach = reach;
        this.twoHanded = twoHanded;
        this.weightCategory = weightCategory;
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
                Debug.LogWarning("Weapon constructor did not recognize equipType.");
                return EquipType.None;
            }
            else
            {
                return (EquipType)Enum.Parse(typeof(EquipType), equipType, true);
            }
        }
    }

    public DieType DamageRange
    {
        get
        {
            if (!Enum.IsDefined(typeof(DieType), damageRange))
            {
                Debug.LogWarning("Weapon constructor did not recognize damageRange.");
                return DieType.d1;
            }
            else
            {
                return (DieType)Enum.Parse(typeof(DieType), damageRange.ToString(), true);
            }
        }
    }

    public int DamageMulti { get { return damageMulti; } }
    public DamageType DamageType
    {
        get
        {
            if (!Enum.IsDefined(typeof(DamageType), damageType))
            {
                Debug.LogWarning("Weapon constructor did not recognize damageType.");
                return DamageType.Piercing;
            }
            else
            {
                return (DamageType)Enum.Parse(typeof(DamageType), damageType, true);
            }
        }
    }
    public bool Finesse { get { return finesse; } }
    public int MaxRange { get { return maxRange; } }
    public int Range { get { return range; } }
    public bool Reach { get { return reach; } }
    public bool TwoHanded { get { return twoHanded; } }
    public WeightCategory WeightCategory
    {
        get
        {
            if (!Enum.IsDefined(typeof(WeightCategory), weightCategory))
            {
                Debug.LogWarning("Weapon constructor did not recognize weightCategory.");
                return WeightCategory.Normal;
            }
            else
            {
                return (WeightCategory)Enum.Parse(typeof(WeightCategory), weightCategory, true);
            }
        }
    }
}
