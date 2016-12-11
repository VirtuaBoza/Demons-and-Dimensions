using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

	public GameObject inventoryPanel, inventorySlot, inventoryItem;

	public List<Item> items = new List<Item>();
	public List<GameObject> slots = new List<GameObject>();

	ItemDatabase database;

	void Awake () {
		database = GetComponent<ItemDatabase>();

		foreach (Slot slot in inventoryPanel.GetComponentsInChildren<Slot>()) {
			slots.Add (slot.gameObject);
			items.Add (new Item());
		}
	}

	void Start () {
		AddItem(0);
		AddItem(3);
	}

	public void AddItem(int id) {
		Item itemToAdd = database.FetchItemByID(id);

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
