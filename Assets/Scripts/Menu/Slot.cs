using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System;

public class Slot : MonoBehaviour, IDropHandler {

	public int slotID;
	public Image armorTypeIcon, bootsTypeIcon, helmetTypeIcon, weaponTypeIcon;
	public enum SlotType {All, Armor, Boots, Helmet, Weapon}
	public SlotType slotItemType = SlotType.All;
	public CHARACTER owner;

	private Inventory inv;

	void Start ()
	{
		inv = FindObjectOfType<Inventory>();

		switch (slotItemType) {
		case SlotType.All:
			// Do nothing
			break;
		case SlotType.Armor:
			Instantiate (armorTypeIcon, this.transform, false);
			break;
		case SlotType.Boots:
			Instantiate (bootsTypeIcon, this.transform, false);
			break;
		case SlotType.Helmet:
			Instantiate (helmetTypeIcon, this.transform, false);
			break;
		case SlotType.Weapon:
			Instantiate (weaponTypeIcon, this.transform, false);
			break;
		}
	}

	public void OnDrop (PointerEventData eventData) {

		// Get the item info of the dropped item
		ItemInfo droppedItem = eventData.pointerDrag.GetComponent<ItemInfo>();

		if (slotItemType == SlotType.All || 
			(droppedItem.item.Itemtype == ITEMTYPE.Armor && slotItemType == SlotType.Armor) || 
			((droppedItem.item.Itemtype == ITEMTYPE.Weapon || droppedItem.item.Itemtype == ITEMTYPE.Shield) && slotItemType == SlotType.Weapon) || 
			(droppedItem.item.Itemtype == ITEMTYPE.Helmet && slotItemType == SlotType.Helmet) ||
			(droppedItem.item.Itemtype == ITEMTYPE.Boots && slotItemType == SlotType.Boots)) {

			// If the item from the inventory's item list at the index that matches this slot's ID number is a non-item...
			if (inv.items[slotID].ID == -1) {
				// ... then put a non-item in the item list at the index that matches the slot ID of where the item came from...
				inv.items[droppedItem.itemSlotID] = new Item();
				// ... and put the dropped item in the item list at the index that matches this slot ID...
				inv.items[slotID] = droppedItem.item;
				// ... and update the item's slot ID with this slot ID.
				droppedItem.itemSlotID = slotID;

				// Otherwise, if the dropped item's slot ID is not this slot ID...
			} else if (droppedItem.itemSlotID != slotID){
				// ... cache the item in this slot...
				Transform presentItem = this.transform.GetChild(0);
				// ... reassign the item in this slot ID to the dropped item's slot ID...
				presentItem.GetComponent<ItemInfo>().itemSlotID = droppedItem.itemSlotID;
				// ... change the parent of the item that was in this slot to the dropped item's slot...
				presentItem.transform.SetParent(inv.slots[droppedItem.itemSlotID].transform);
				// ... change the position of the item that was in this slot to it's new parent's position...
				presentItem.position = inv.slots[droppedItem.itemSlotID].transform.position;
				// ... assign the dropped item to this slot...
				droppedItem.itemSlotID = slotID;
				// ... put the item that was in this slot into the item list at the dropped item's index...
				inv.items[droppedItem.itemSlotID] = presentItem.GetComponent<ItemInfo>().item;
				// ... and put the new item into the index of the item list that matches this slot ID.
				inv.items[slotID] = droppedItem.item;
			}
		}

	}
}
