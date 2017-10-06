using Assets.Scripts.Game;
using LitJson;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum ITEMTYPE { Armor, Boots, Helmet, Shield, Weapon }
public enum DAMAGETYPE { Acid, Bludgeoning, Cold, Fire, Force, Lightning, Necrotic, Piercing, Poison, Psychic, Radiant, Slashing, Thunder }

public class ItemDatabase : MonoBehaviour
{

    public List<Item> itemDatabase = new List<Item>(); // Prepares a list in which to keep every type of item
    private JsonData itemData;

    void Start()
    {
        itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json"));
        ConstructItemDatabase();
    }

    void ConstructItemDatabase()
    {
        for (int i = 0; i < itemData.Count; i++)
        {
            if (itemData[i]["itemtype"].ToString().Contains("Armor") || itemData[i]["itemtype"].ToString().Contains("Shield"))
            {
                itemDatabase.Add(new Item((int)itemData[i]["id"],
                    itemData[i]["title"].ToString(),
                    itemData[i]["itemtype"].ToString(),
                    (int)itemData[i]["ac"],
                    (bool)itemData[i]["dexmodifies"],
                    (bool)itemData[i]["modifiermax2"],
                    (int)itemData[i]["str"],
                    (bool)itemData[i]["stealthdisadv"],
                    itemData[i]["slug"].ToString()));
            }
            else if (itemData[i]["itemtype"].ToString().Contains("Weapon"))
            {
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
            }
            else
            {
                Debug.LogWarning("ConstructItemDatabase doesn't recognize the itemtype");
            }

        }
    }

    public Item FetchItemByID(int id)
    {
        for (int i = 0; i < itemDatabase.Count; i++)
        {
            if (itemDatabase[i].ID == id)
            {
                return itemDatabase[i];
            }
        }
        return null;
    }

}

