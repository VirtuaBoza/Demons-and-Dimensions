using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

	public GameObject inventorySlot, inventoryItem;

	public List<Item> items = new List<Item>();
	public List<GameObject> slots = new List<GameObject>();

	ItemDatabase database;
	int numberOfSlots;

	void Start () {
		database = GetComponent<ItemDatabase>();

		foreach (Slot slot in GetComponentsInChildren<Slot>()) {
			slots.Add(slot.gameObject);
			items.Add (new Item());
		}

		AddItem(0);
		AddItem(1);
		AddItem(1);
		AddItem(1);
	}

	public void AddItem(int id) {
		Item itemToAdd = database.FetchItemByID(id);

		if (itemToAdd.Stackable && CheckIfItemIsInInventory(itemToAdd) != null){
			ItemInfo info = CheckIfItemIsInInventory(itemToAdd).transform.GetComponentInChildren<ItemInfo>();
			info.amount++;
			info.transform.GetComponentInChildren<Text>().text = info.amount.ToString();
		} else {
			for (int i = 0; i < items.Count; i++){
				if (items[i].ID == -1){
					items[i] = itemToAdd;
					GameObject itemObj = Instantiate(inventoryItem);
					itemObj.GetComponent<ItemInfo>().item = itemToAdd;
					itemObj.GetComponent<ItemInfo>().amount = 1;
					itemObj.GetComponent<ItemInfo>().itemSlotID = i;
					itemObj.transform.SetParent(slots[i].transform,false);
					itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite;
					itemObj.name = itemToAdd.Title;
					break;
				}
			}
		}
	}

	GameObject CheckIfItemIsInInventory (Item item) {
		for (int i = 0; i < items.Count; i++){
			if (items[i].ID == item.ID){
				return slots[i];
			}
		}
		return null;
	}

}
