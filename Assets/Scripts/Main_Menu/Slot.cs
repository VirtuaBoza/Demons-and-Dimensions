using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IDropHandler {

	public Image armorTypeIcon, bootsTypeIcon, helmetTypeIcon, weaponTypeIcon;
	public enum SlotType {All, Armor, Boots, Helmet, Weapon}
	public SlotType slotItemType = SlotType.All;
	public CHARACTER owner;

	private Inventory inv;

	void Start ()
	{
		inv = FindObjectOfType<Inventory>();

		switch (slotItemType) {
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
		default:
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

			if (inv.assignedItems[this].ID == -1) { //Test
				inv.assignedItems[droppedItem.slot] = new Item();
				inv.assignedItems [this] = droppedItem.item;
				droppedItem.slot = this;
			} else if (droppedItem.slot != this) {
				ItemInfo presentItem = this.transform.GetComponentInChildren<ItemInfo>();
				presentItem.slot = droppedItem.slot;
				presentItem.transform.SetParent (droppedItem.slot.transform);
				presentItem.transform.position = droppedItem.slot.transform.position;
				droppedItem.slot = this;
				inv.assignedItems [droppedItem.slot] = presentItem.item;
				inv.assignedItems [this] = droppedItem.item;
			}
		}

	}
}
