using UnityEngine;

namespace Assets.Scripts.Game
{
    public class Item
    {

        // Properties of all items
        public readonly int ID;
        public readonly string Title;
        public readonly ITEMTYPE Itemtype;

        // Properties of armor
        public readonly int Ac;
        public readonly bool Dexmodifies;
        public readonly bool Modifiermax2;
        public readonly int Str;
        public readonly bool Stealthdisadv;

        // Properties of weapons
        public readonly DIE Damagerange;
        public readonly int Damagemulti;
        public readonly DAMAGETYPE Damagetype;
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
            if (itemtype.Contains("Armor")) this.Itemtype = ITEMTYPE.Armor;
            else if (itemtype.Contains("Boots")) this.Itemtype = ITEMTYPE.Boots;
            else if (itemtype.Contains("Helmet")) this.Itemtype = ITEMTYPE.Helmet;
            else if (itemtype.Contains("Shield")) this.Itemtype = ITEMTYPE.Weapon;
            else if (itemtype.Contains("Weapon")) this.Itemtype = ITEMTYPE.Weapon;
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
            if (itemtype.Contains("armor")) this.Itemtype = ITEMTYPE.Armor;
            else if (itemtype.ToLower().Contains("boots")) this.Itemtype = ITEMTYPE.Boots;
            else if (itemtype.ToLower().Contains("helmet")) this.Itemtype = ITEMTYPE.Helmet;
            else if (itemtype.ToLower().Contains("shield")) this.Itemtype = ITEMTYPE.Weapon;
            else if (itemtype.ToLower().Contains("weapon")) this.Itemtype = ITEMTYPE.Weapon;
            else Debug.LogWarning("Item constructor doesn't recognize the itemtype");
            switch (damagerange)
            {
                case 20:
                    this.Damagerange = DIE.d20;
                    break;
                case 12:
                    this.Damagerange = DIE.d12;
                    break;
                case 10:
                    this.Damagerange = DIE.d10;
                    break;
                case 8:
                    this.Damagerange = DIE.d8;
                    break;
                case 6:
                    this.Damagerange = DIE.d6;
                    break;
                case 4:
                    this.Damagerange = DIE.d4;
                    break;
                default:
                    Debug.LogWarning("Item constructor doesn't recognize the Damagerange");
                    break;
            }
            this.Damagemulti = damagemulti;
            if (damagetype.ToLower().Contains("acid")) this.Damagetype = DAMAGETYPE.Acid;
            else if (damagetype.ToLower().Contains("bludgeoning")) this.Damagetype = DAMAGETYPE.Bludgeoning;
            else if (damagetype.ToLower().Contains("cold")) this.Damagetype = DAMAGETYPE.Cold;
            else if (damagetype.ToLower().Contains("fire")) this.Damagetype = DAMAGETYPE.Fire;
            else if (damagetype.ToLower().Contains("force")) this.Damagetype = DAMAGETYPE.Force;
            else if (damagetype.ToLower().Contains("lightning")) this.Damagetype = DAMAGETYPE.Lightning;
            else if (damagetype.ToLower().Contains("necrotic")) this.Damagetype = DAMAGETYPE.Necrotic;
            else if (damagetype.ToLower().Contains("piercing")) this.Damagetype = DAMAGETYPE.Piercing;
            else if (damagetype.ToLower().Contains("poison")) this.Damagetype = DAMAGETYPE.Poison;
            else if (damagetype.ToLower().Contains("psychic")) this.Damagetype = DAMAGETYPE.Psychic;
            else if (damagetype.ToLower().Contains("radiant")) this.Damagetype = DAMAGETYPE.Radiant;
            else if (damagetype.ToLower().Contains("slashing")) this.Damagetype = DAMAGETYPE.Slashing;
            else if (damagetype.ToLower().Contains("thunder")) this.Damagetype = DAMAGETYPE.Thunder;
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

}
