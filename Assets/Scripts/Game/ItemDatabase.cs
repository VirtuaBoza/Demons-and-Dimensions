using UnityEngine;
using System.Collections;
using LitJson;
using System.Collections.Generic;
using System.IO;

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
					(bool)itemData[i]["properties"]["ammunition"],
					(bool)itemData[i]["properties"]["finesse"],
					(bool)itemData[i]["properties"]["heavy"],
					(bool)itemData[i]["properties"]["light"],
					(bool)itemData[i]["properties"]["loading"],
					(bool)itemData[i]["properties"]["reach"],
					(bool)itemData[i]["properties"]["thrown"],
					(bool)itemData[i]["properties"]["twohanded"],
					(bool)itemData[i]["properties"]["versatile"],
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
	public string Itemtype { get; set; }

	// Properties of armor
	public int Ac { get; set; }
	public bool Dexmodifies { get; set; }
	public bool Modifiermax2 { get; set; }
	public int Str { get; set; }
	public bool Stealthdisadv { get; set; }

	// Properties of weapons
	public int Damagerange { get; set; }
	public int Damagemulti { get; set; }
	public string Damagetype { get; set; }
	public bool Ammunition { get; set; }
	public bool Finesse { get; set; }
	public bool Heavy { get; set; }
	public bool Light { get; set; }
	public bool Loading { get; set; }
	public bool Reach { get; set; }
	public bool Thrown { get; set; }
	public bool Twohanded { get; set; }
	public bool Versatile { get; set; }
	public int Range { get; set; }
	public int Maxrange { get; set; }
	public string Slug { get; set; }
	public Sprite Sprite { get; set; }

	// Constructor for an armor item
	public Item (int id, string title, string itemtype, int ac, bool dexmodifies, bool modifiermax2, int str, bool stealthdisadv, string slug){
		this.ID = id;
		this.Title = title;
		this.Itemtype = itemtype;
		this.Ac = ac;
		this.Dexmodifies = dexmodifies;
		this.Modifiermax2 = modifiermax2;
		this.Str = str;
		this.Stealthdisadv = stealthdisadv;
		this.Slug = slug;
		this.Sprite = Resources.Load<Sprite>("Sprites/Items/" + slug);
	}

	// Constructor for a weapon item
	public Item (int id, string title, string itemtype, int damagerange, int damagemulti, string damagetype, bool ammunition, bool finesse, bool heavy,
		bool light, bool loading, bool reach, bool thrown, bool twohanded, bool versatile, int range, int maxrange, string slug) {
		this.ID = id;
		this.Title = title;
		this.Itemtype = itemtype;
		this.Damagerange = damagerange;
		this.Damagemulti = damagemulti;
		this.Damagetype = damagetype;
		this.Ammunition = ammunition;
		this.Finesse = finesse;
		this.Heavy = heavy;
		this.Light = light;
		this.Loading = loading;
		this.Reach = reach;
		this.Thrown = thrown;
		this.Twohanded = twohanded;
		this.Versatile = versatile;
		this.Range = range;
		this.Maxrange = maxrange;
		this.Slug = slug;
		this.Sprite = Resources.Load<Sprite>("Sprites/Items/" + slug);
	}

	public Item () {
		this.ID = -1;
	}

}
