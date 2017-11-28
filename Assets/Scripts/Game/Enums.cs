public enum AbilityType
{
    Str = 0,
    Dex,
    Con,
    Int,
    Wis,
    Cha
}

public enum ActionType
{
    Attacking = 0,
    Buffing,
    Casting
}

public enum AnimationType
{
    IdleUp = 0, IdleLeft, IdleDown, IdleRight,
    WalkUp, WalkLeft, WalkDown, WalkRight,
    SlashUp, SlashLeft, SlashDown, SlashRight,
    ThrustUp, ThrustLeft, ThrustDown, ThrustRight,
    LooseUp, LooseLeft, LooseDown, LooseRight,
    SpellcastUp, SpellcastLeft, SpellcastDown, SpellcastRight,
    Fall
}

public enum CharacterClass
{
    DM = 0,
    Mage,
    Thief,
    Warrior
}

public enum CharacterPanelType
{
    None = 0,
    Stats,
    Inventory,
    Spells
}

public enum DamageType
{
    Acid = 0,
    Bludgeoning,
    Cold,
    Fire,
    Force,
    Lightning,
    Necrotic,
    Piercing,
    Poison,
    Psychic,
    Radiant,
    Slashing,
    Thunder
}

public enum DieType
{
    d20 = 20,
    d12 = 12,
    d10 = 10,
    d8 = 8,
    d6 = 6,
    d4 = 4,
    d00 = 100,
    d1 = 1
}

public enum EquipType
{
    None = 0,
    Footwear,
    Legwear,
    Handwear,
    Chestwear,
    Waistwear,
    Backwear,
    Neckwear,
    Headwear,
    RightHanded,
    LeftHanded
}

public enum ItemType
{
    Empty = 0,
    Armor,
    Boots,
    Helmet,
    Shield,
    Weapon
}
public enum PlayerCharacterName
{
    Crystal = 0,
    Teddy,
    Hunter,
    Damien
}

public enum SlotType //TODO Combine ItemType and SlotType?
{
    All = 0,
    Armor,
    Boots,
    Helmet,
    Weapon
}

public enum WeightCategory
{
    Normal = 0,
    Heavy,
    Light
}
