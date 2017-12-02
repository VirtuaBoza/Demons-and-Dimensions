using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject inventoryPanel;
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

    // TEST TEST TEST
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            AddItem(0);
        }
        if(Input.GetKeyDown(KeyCode.I))
        {
            AddItem(3);
        }
    }
    // TEST COMPLETE

    public void AddItem(int id)
    {
        foreach (KeyValuePair<Slot, Item> slotItemPair in assignedItemBySlot)
        {
            if (slotItemPair.Key.slotItemType == SlotType.All && slotItemPair.Value.ID == -1)
            {
                Item itemToAdd = itemDatabase.FetchItemByID(id);
                assignedItemBySlot[slotItemPair.Key] = itemToAdd;
                GameObject itemObj = Instantiate(inventoryItem);
                itemObj.GetComponent<ItemInfo>().item = itemToAdd;
                itemObj.GetComponent<ItemInfo>().amount = 1;
                itemObj.GetComponent<ItemInfo>().slot = slotItemPair.Key;
                itemObj.transform.SetParent(slotItemPair.Key.transform, false);
                itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite;
                itemObj.name = itemToAdd.Title;
                return;
            }
        }
        Debug.LogWarning("You need to handle what happens when the inventory is full and something attempts to add one more item");
    }

    public void UpdateInventory()
    {
        foreach (PlayerCharacterName character in Enum.GetValues(typeof(PlayerCharacterName)))
        {
            foreach (Slot slot in inventoryPanel.GetComponentsInChildren<Slot>())
            {
                if (slot.owner == character)
                {
                    if (slot.GetComponentInChildren<ItemInfo>())
                    {
                        IEquipable equipment = slot.GetComponentInChildren<ItemInfo>().item as IEquipable;
                        characterDatabase.CharacterDictionary[character].AddEquipment(equipment);
                    }
                }
            }
        }
        Player player = FindObjectOfType<Player>();
        if (player)
        {
            player.UpdatePlayerEquipment();
        }
    }
}
