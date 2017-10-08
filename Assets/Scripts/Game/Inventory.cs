using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

	public GameObject inventoryPanel, inventorySlot, inventoryItem;

	public Dictionary<Slot,Item> assignedItems = new Dictionary<Slot, Item>();
	public Dictionary<PLAYERCHARACTER,List<Item>> characterEquippedItems = new Dictionary<PLAYERCHARACTER, List<Item>>();

	private ItemDatabase database;

	void Awake () {
		database = FindObjectOfType<ItemDatabase>();
		foreach (Slot slot in inventoryPanel.GetComponentsInChildren<Slot>()) {
			assignedItems.Add (slot, new Item ()); //Test
		}
	}

	void Start () {
		AddItem(0); //For testing puposes
		AddItem(3); //For testing puposes
		characterEquippedItems.Add(PLAYERCHARACTER.Crystal, new List<Item>());
		characterEquippedItems.Add(PLAYERCHARACTER.Damien, new List<Item>());
		characterEquippedItems.Add(PLAYERCHARACTER.Hunter, new List<Item>());
		characterEquippedItems.Add(PLAYERCHARACTER.Teddy, new List<Item>());
		UpdateInventory();
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.U)) UpdateInventory();
	}

	public void AddItem(int id) {
		Item itemToAdd = database.FetchItemByID(id);

		foreach (KeyValuePair<Slot,Item> entry in assignedItems){
			if (entry.Value.ID == -1){
				assignedItems[entry.Key] = itemToAdd;
				GameObject itemObj = Instantiate(inventoryItem);
				itemObj.GetComponent<ItemInfo>().item = itemToAdd;
				itemObj.GetComponent<ItemInfo>().amount = 1;
				itemObj.GetComponent<ItemInfo> ().slot = entry.Key;
				itemObj.transform.SetParent(entry.Key.transform,false);
				itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite;
				itemObj.name = itemToAdd.Title;
				break;
			}
		}
	}

	public void UpdateInventory() {
		PLAYERCHARACTER[] characters = characterEquippedItems.Keys.ToArray();
		foreach (PLAYERCHARACTER character in characters) {
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
