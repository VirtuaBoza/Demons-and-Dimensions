using UnityEngine;

public class Item
{
    // Properties of all items
    public readonly int ID;
    public readonly string Title;
    public readonly ItemType Itemtype;

    // Properties of armor
    public readonly int Ac;
    public readonly bool Dexmodifies;
    public readonly bool Modifiermax2;
    public readonly int Str;
    public readonly bool Stealthdisadv;

    // Properties of weapons
    public readonly DieType Damagerange;
    public readonly int Damagemulti;
    public readonly DamageType Damagetype;
    public readonly bool Finesse;
    public readonly bool Heavy;
    public readonly bool Light;
    public readonly bool Reach;
    public readonly bool Twohanded;
    public readonly int Range;
    public readonly int Maxrange;
    public readonly string Slug;
    public readonly Sprite Sprite;

    // Constructor for an armor item
    public Item(int id, string title, string itemtype, int ac, bool dexmodifies, bool modifiermax2, int str, bool stealthdisadv, string slug)
    {
        this.ID = id;
        this.Title = title;
        if (itemtype.Contains("Armor")) this.Itemtype = ItemType.Armor;
        else if (itemtype.Contains("Boots")) this.Itemtype = ItemType.Boots;
        else if (itemtype.Contains("Helmet")) this.Itemtype = ItemType.Helmet;
        else if (itemtype.Contains("Shield")) this.Itemtype = ItemType.Weapon;
        else if (itemtype.Contains("Weapon")) this.Itemtype = ItemType.Weapon;
        else Debug.LogWarning("Item constructor doesn't recognize the itemtype");
        this.Ac = ac;
        this.Dexmodifies = dexmodifies;
        this.Modifiermax2 = modifiermax2;
        this.Str = str;
        this.Stealthdisadv = stealthdisadv;
        this.Slug = slug;
        this.Sprite = Resources.Load<Sprite>("Sprites/Items/" + slug);
    }

    // Constructor for a weapon item
    public Item(int id, string title, string itemtype, int damagerange, int damagemulti, string damagetype, bool finesse, bool heavy,
        bool light, bool reach, bool twohanded, int range, int maxrange, string slug)
    {
        this.ID = id;
        this.Title = title;
        if (itemtype.Contains("armor")) this.Itemtype = ItemType.Armor;
        else if (itemtype.ToLower().Contains("boots")) this.Itemtype = ItemType.Boots;
        else if (itemtype.ToLower().Contains("helmet")) this.Itemtype = ItemType.Helmet;
        else if (itemtype.ToLower().Contains("shield")) this.Itemtype = ItemType.Weapon;
        else if (itemtype.ToLower().Contains("weapon")) this.Itemtype = ItemType.Weapon;
        else Debug.LogWarning("Item constructor doesn't recognize the itemtype");
        switch (damagerange)
        {
            case 20:
                this.Damagerange = DieType.d20;
                break;
            case 12:
                this.Damagerange = DieType.d12;
                break;
            case 10:
                this.Damagerange = DieType.d10;
                break;
            case 8:
                this.Damagerange = DieType.d8;
                break;
            case 6:
                this.Damagerange = DieType.d6;
                break;
            case 4:
                this.Damagerange = DieType.d4;
                break;
            default:
                Debug.LogWarning("Item constructor doesn't recognize the Damagerange");
                break;
        }
        this.Damagemulti = damagemulti;
        if (damagetype.ToLower().Contains("acid")) this.Damagetype = DamageType.Acid;
        else if (damagetype.ToLower().Contains("bludgeoning")) this.Damagetype = DamageType.Bludgeoning;
        else if (damagetype.ToLower().Contains("cold")) this.Damagetype = DamageType.Cold;
        else if (damagetype.ToLower().Contains("fire")) this.Damagetype = DamageType.Fire;
        else if (damagetype.ToLower().Contains("force")) this.Damagetype = DamageType.Force;
        else if (damagetype.ToLower().Contains("lightning")) this.Damagetype = DamageType.Lightning;
        else if (damagetype.ToLower().Contains("necrotic")) this.Damagetype = DamageType.Necrotic;
        else if (damagetype.ToLower().Contains("piercing")) this.Damagetype = DamageType.Piercing;
        else if (damagetype.ToLower().Contains("poison")) this.Damagetype = DamageType.Poison;
        else if (damagetype.ToLower().Contains("psychic")) this.Damagetype = DamageType.Psychic;
        else if (damagetype.ToLower().Contains("radiant")) this.Damagetype = DamageType.Radiant;
        else if (damagetype.ToLower().Contains("slashing")) this.Damagetype = DamageType.Slashing;
        else if (damagetype.ToLower().Contains("thunder")) this.Damagetype = DamageType.Thunder;
        else Debug.LogWarning("Item constructor doesn't recognize the Damagetype");
        this.Finesse = finesse;
        this.Heavy = heavy;
        this.Light = light;
        this.Reach = reach;
        this.Twohanded = twohanded;
        this.Range = range;
        this.Maxrange = maxrange;
        this.Slug = slug;
        this.Sprite = Resources.Load<Sprite>("Sprites/Items/" + slug);
    }

    public Item()
    {
        this.ID = -1;
    }
}