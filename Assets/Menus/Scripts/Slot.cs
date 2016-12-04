using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class Slot : MonoBehaviour, IDropHandler {

	public int slotID;

	private Inventory inv;

	void Start () {
		inv = GetComponentInParent<Inventory>();
	}

	public void OnDrop (PointerEventData eventData) {
		ItemInfo droppedItem = eventData.pointerDrag.GetComponent<ItemInfo>();
		if (inv.items[slotID].ID == -1) {
			inv.items[droppedItem.itemSlotID] = new Item();
			inv.items[slotID] = droppedItem.item;
			droppedItem.itemSlotID = slotID;
		} else if (droppedItem.itemSlotID != slotID){
			Transform presentItem = this.transform.GetChild(0);
			presentItem.GetComponent<ItemInfo>().itemSlotID = droppedItem.itemSlotID;
			presentItem.transform.SetParent(inv.slots[droppedItem.itemSlotID].transform);
			presentItem.position = inv.slots[droppedItem.itemSlotID].transform.position;

			droppedItem.itemSlotID = slotID;

			inv.items[droppedItem.itemSlotID] = presentItem.GetComponent<ItemInfo>().item;
			inv.items[slotID] = droppedItem.item;
		}
	}
}
