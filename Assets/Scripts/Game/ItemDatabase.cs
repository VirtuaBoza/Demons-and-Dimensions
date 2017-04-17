using UnityEngine;
using System.Collections;
using LitJson;
using System.Collections.Generic;
using System.IO;

public enum ITEMTYPE {Armor, Boots, Helmet, Shield, Weapon}
public enum DAMAGETYPE {Acid, Bludgeoning, Cold, Fire, Force, Lightning, Necrotic, Piercing, Poison, Psychic, Radiant, Slashing, Thunder}

public class ItemDatabase : MonoBehaviour {

	public List<Item> itemDatabase = new List<Item>();
	private JsonData itemData;

	void Start(){
		itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json"));
		ConstructItemDatabase();
	}

	void ConstructItemDatabase () {
		for (int i = 0; i < itemData.Count; i++){
			if (itemData[i]["itemtype"].ToString().Contains("Armor") || itemData[i]["itemtype"].ToString().Contains("Shield")) {
				itemDatabase.Add(new Item((int)itemData[i]["id"], 
					itemData[i]["title"].ToString(), 
					itemData[i]["itemtype"].ToString(), 
					(int)itemData[i]["ac"], 
					(bool)itemData[i]["dexmodifies"], 
					(bool)itemData[i]["modifiermax2"], 
					(int)itemData[i]["str"], 
					(bool)itemData[i]["stealthdisadv"], 
					itemData[i]["slug"].ToString()));
			} else if (itemData[i]["itemtype"].ToString().Contains("Weapon")) {
				itemDatabase.Add(new Item((int)itemData[i]["id"], 
					itemData[i]["title"].ToString(), 
					itemData[i]["itemtype"].ToString(),
					(int)itemData[i]["damagerange"],
					(int)itemData[i]["damagemulti"],
					itemData[i]["damagetype"].ToString(),
					(bool)itemData[i]["properties"]["finesse"],
					(bool)itemData[i]["properties"]["heavy"],
					(bool)itemData[i]["properties"]["light"],
					(bool)itemData[i]["properties"]["reach"],
					(bool)itemData[i]["properties"]["twohanded"],
					(int)itemData[i]["range"],
					(int)itemData[i]["maxrange"],
					itemData[i]["slug"].ToString()));
			} else {
				Debug.LogWarning ("ConstructItemDatabase doesn't recognize the itemtype");	
			} 

		}
	}

	public Item FetchItemByID (int id){
		for (int i = 0; i < itemDatabase.Count; i++) {
			if (itemDatabase[i].ID == id) {
				return itemDatabase[i];
			}
		}
		return null;
	}

}

public class Item {
	
	// Properties of all items
	public int ID { get; set; }
	public string Title { get; set; }
	public ITEMTYPE Itemtype { get; set; }

	// Properties of armor
	public int Ac { get; set; }
	public bool Dexmodifies { get; set; }
	public bool Modifiermax2 { get; set; }
	public int Str { get; set; }
	public bool Stealthdisadv { get; set; }

	// Properties of weapons
	public DIE Damagerange { get; set; }
	public int Damagemulti { get; set; }
	public DAMAGETYPE Damagetype { get; set; }
	public bool Finesse { get; set; }
	public bool Heavy { get; set; }
	public bool Light { get; set; }
	public bool Reach { get; set; }
	public bool Twohanded { get; set; }
	public int Range { get; set; }
	public int Maxrange { get; set; }
	public string Slug { get; set; }
	public Sprite Sprite { get; set; }

	// Constructor for an armor item
	public Item (int id, string title, string itemtype, int ac, bool dexmodifies, bool modifiermax2, int str, bool stealthdisadv, string slug){
		this.ID = id;
		this.Title = title;
		if (itemtype.Contains("Armor")) this.Itemtype = ITEMTYPE.Armor;
		else if (itemtype.Contains("Boots")) this.Itemtype = ITEMTYPE.Boots;
		else if (itemtype.Contains("Helmet")) this.Itemtype = ITEMTYPE.Helmet;
		else if (itemtype.Contains("Shield")) this.Itemtype = ITEMTYPE.Weapon;
		else if (itemtype.Contains("Weapon")) this.Itemtype = ITEMTYPE.Weapon;
		else Debug.LogWarning ("Item constructor doesn't recognize the itemtype");	
		this.Ac = ac;
		this.Dexmodifies = dexmodifies;
		this.Modifiermax2 = modifiermax2;
		this.Str = str;
		this.Stealthdisadv = stealthdisadv;
		this.Slug = slug;
		this.Sprite = Resources.Load<Sprite>("Sprites/Items/" + slug);
	}

	// Constructor for a weapon item
	public Item (int id, string title, string itemtype, int damagerange, int damagemulti, string damagetype, bool finesse, bool heavy,
		bool light, bool reach, bool twohanded, int range, int maxrange, string slug) {
		this.ID = id;
		this.Title = title;
		if (itemtype.Contains("armor")) this.Itemtype = ITEMTYPE.Armor;
		else if (itemtype.ToLower().Contains("boots")) this.Itemtype = ITEMTYPE.Boots;
		else if (itemtype.ToLower().Contains("helmet")) this.Itemtype = ITEMTYPE.Helmet;
		else if (itemtype.ToLower().Contains("shield")) this.Itemtype = ITEMTYPE.Weapon;
		else if (itemtype.ToLower().Contains("weapon")) this.Itemtype = ITEMTYPE.Weapon;
		else Debug.LogWarning ("Item constructor doesn't recognize the itemtype");
		switch (damagerange) {
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
			Debug.LogWarning ("Item constructor doesn't recognize the Damagerange");
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
		else Debug.LogWarning ("Item constructor doesn't recognize the Damagetype");
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

	public Item () {
		this.ID = -1;
	}

}
