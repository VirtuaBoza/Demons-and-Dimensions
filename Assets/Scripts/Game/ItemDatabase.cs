using LitJson;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;

public class ItemDatabase : MonoBehaviour
{
    public List<Item> allItems = new List<Item>();

    private List<int> itemIDs = new List<int>();

    void Start()
    {
        AddArmorToDatabase();
        AddWeaponsToDatabase();
    }

    private void AddArmorToDatabase()
    {
        JsonData itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Armor.json"));
        for (int i = 0; i < itemData.Count; i++)
        {
            if (itemIDs.Contains((int)itemData[i]["id"]))
            {
                Debug.LogWarning("You have a duplicate itemID!");
            }
            else
            {
                itemIDs.Add((int)itemData[i]["id"]);
                allItems.Add(new Armor(
                        (int)itemData[i]["id"],
                        itemData[i]["itemtype"].ToString(),
                        itemData[i]["title"].ToString(),
                        itemData[i]["spritename"].ToString(),
                        itemData[i]["spritesheetname"].ToString(),
                        itemData[i]["animationcategories"].ToString(),
                        itemData[i]["equiptype"].ToString(),
                        (int)itemData[i]["ac"],
                        (bool)itemData[i]["dexmodifieriscappedat2"],
                        (bool)itemData[i]["disadvantagesstealth"],
                        (bool)itemData[i]["ismodifiedbydex"],
                        (int)itemData[i]["strengthrequirement"]));
            }
        }
    }

    private void AddWeaponsToDatabase()
    {
        JsonData itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Weapons.json"));
        for (int i = 0; i < itemData.Count; i++)
        {
            if (itemIDs.Contains((int)itemData[i]["id"]))
            {
                Debug.LogWarning("You have a duplicate itemID!");
            }
            else
            {
                itemIDs.Add((int)itemData[i]["id"]);
                allItems.Add(new Weapon(
                        (int)itemData[i]["id"],
                        itemData[i]["title"].ToString(),
                        itemData[i]["spritename"].ToString(),
                        itemData[i]["spritesheetname"].ToString(),
                        itemData[i]["animationcategories"].ToString(),
                        itemData[i]["equiptype"].ToString(),
                        (int)itemData[i]["damagemulti"],
                        (int)itemData[i]["damagerange"],
                        itemData[i]["damagetype"].ToString(),
                        (bool)itemData[i]["finesse"],
                        (int)itemData[i]["maxrange"],
                        (int)itemData[i]["range"],
                        (bool)itemData[i]["reach"],
                        (bool)itemData[i]["twohanded"],
                        itemData[i]["weightcategory"].ToString()));
            }
        }
    }

    public Item FetchItemByID(int id)
    {
        for (int i = 0; i < allItems.Count; i++)
        {
            if (allItems[i].ID == id)
            {
                return allItems[i];
            }
        }
        Debug.LogWarning("No item exists with ID " + id);
        return new Item();
    }

    public IEquipable FetchIEquipableByID(int id)
    {
        for (int i = 0; i < allItems.Count; i++)
        {
            if (allItems[i].ID == id)
            {
                if (allItems[i] as IEquipable != null)
                {
                    return allItems[i] as IEquipable;
                }
                else
                {
                    Debug.LogWarning("Item with ID " + id + " is not IEquipable");
                    return null;
                }
            }
        }
        Debug.LogWarning("No item exists with ID " + id);
        return null;
    }
}

