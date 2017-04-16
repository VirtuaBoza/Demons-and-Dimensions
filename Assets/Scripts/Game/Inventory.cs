using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Inventory : MonoBehaviour {

	public GameObject inventoryPanel, inventorySlot, inventoryItem;

	public List<Item> items = new List<Item>();
	public List<GameObject> slots = new List<GameObject>();
	public Dictionary<CHARACTER,List<Item>> characterEquippedItems = new Dictionary<CHARACTER, List<Item>>();

	private ItemDatabase database;

	void Awake () {
		database = FindObjectOfType<ItemDatabase>();
		foreach (Slot slot in inventoryPanel.GetComponentsInChildren<Slot>()) {
			slots.Add (slot.gameObject);
			items.Add (new Item());
		}
	}

	void Start () {
		AddItem(0); //For testing puposes
		AddItem(3); //For testing puposes
		characterEquippedItems.Add(CHARACTER.Crystal, new List<Item>());
		characterEquippedItems.Add(CHARACTER.Damien, new List<Item>());
		characterEquippedItems.Add(CHARACTER.Hunter, new List<Item>());
		characterEquippedItems.Add(CHARACTER.Teddy, new List<Item>());
		UpdateInventory();
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.U)) UpdateInventory();
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

	public void UpdateInventory() {
		CHARACTER[] characters = characterEquippedItems.Keys.ToArray();
		foreach (CHARACTER character in characters) {
			List<Item> tempList = new List<Item>();
			foreach (Slot slot in inventoryPanel.GetComponentsInChildren<Slot>()) {
				if (slot.owner == character) {
					if (slot.GetComponentInChildren<ItemInfo>()) {
						tempList.Add(slot.GetComponentInChildren<ItemInfo>().item);
					}
				}
			}
			characterEquippedItems[character] = tempList;
		}
	}

}
