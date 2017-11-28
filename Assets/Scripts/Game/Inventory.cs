using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject inventorySlot;
    public GameObject inventoryItem;
    public Dictionary<Slot, Item> assignedItemBySlot = new Dictionary<Slot, Item>();

    private ItemDatabase itemDatabase;
    private CharacterDatabase characterDatabase;

    void Awake()
    {
        itemDatabase = FindObjectOfType<ItemDatabase>();
        characterDatabase = FindObjectOfType<CharacterDatabase>();
        foreach (Slot slot in inventoryPanel.GetComponentsInChildren<Slot>())
        {
            assignedItemBySlot.Add(slot, new Item());
        }
    }

    void Start()
    {
        //UpdateInventory();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U)) UpdateInventory();
    }

    public void AddItem(int id)
    {
        Item itemToAdd = itemDatabase.FetchItemByID(id);

        foreach (KeyValuePair<Slot, Item> entry in assignedItemBySlot)
        {
            if (entry.Value.ID == -1)
            {
                assignedItemBySlot[entry.Key] = itemToAdd;
                GameObject itemObj = Instantiate(inventoryItem);
                itemObj.GetComponent<ItemInfo>().item = itemToAdd;
                itemObj.GetComponent<ItemInfo>().amount = 1;
                itemObj.GetComponent<ItemInfo>().slot = entry.Key;
                itemObj.transform.SetParent(entry.Key.transform, false);
                itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite;
                itemObj.name = itemToAdd.Title;
                break;
            }
        }
    }

    public void UpdateInventory()
    {
        foreach (PlayerCharacterName character in Enum.GetValues(typeof(PlayerCharacterName)))
        {
            List<Item> tempList = new List<Item>();
            foreach (Slot slot in inventoryPanel.GetComponentsInChildren<Slot>())
            {
                if (slot.owner == character)
                {
                    if (slot.GetComponentInChildren<ItemInfo>())
                    {
                        tempList.Add(slot.GetComponentInChildren<ItemInfo>().item);
                    }
                }
            }
            characterDatabase.CharacterDictionary[character].EquippedItems = tempList;
        }
    }
}
