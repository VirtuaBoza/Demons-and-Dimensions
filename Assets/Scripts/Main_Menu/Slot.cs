using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IDropHandler
{
    public Image armorTypeIcon, bootsTypeIcon, helmetTypeIcon, weaponTypeIcon;
    public SlotType slotItemType = SlotType.All;
    public PlayerCharacterName owner;

    private Inventory inv;

    void Start()
    {
        inv = FindObjectOfType<Inventory>();

        switch (slotItemType)
        {
            case SlotType.Armor:
                Instantiate(armorTypeIcon, this.transform, false);
                break;
            case SlotType.Boots:
                Instantiate(bootsTypeIcon, this.transform, false);
                break;
            case SlotType.Helmet:
                Instantiate(helmetTypeIcon, this.transform, false);
                break;
            case SlotType.Weapon:
                Instantiate(weaponTypeIcon, this.transform, false);
                break;
            default:
                break;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        // Get the item info of the dropped item
        ItemInfo droppedItem = eventData.pointerDrag.GetComponent<ItemInfo>();

        if (slotItemType == SlotType.All ||
            (droppedItem.item.ItemType == ItemType.Armor && slotItemType == SlotType.Armor) ||
            ((droppedItem.item.ItemType == ItemType.Weapon || droppedItem.item.ItemType == ItemType.Shield) && slotItemType == SlotType.Weapon) ||
            (droppedItem.item.ItemType == ItemType.Helmet && slotItemType == SlotType.Helmet) ||
            (droppedItem.item.ItemType == ItemType.Boots && slotItemType == SlotType.Boots))
        {

            if (inv.assignedItemBySlot[this].ID == -1)
            {
                inv.assignedItemBySlot[droppedItem.slot] = new Item();
                inv.assignedItemBySlot[this] = droppedItem.item;
                droppedItem.slot = this;
            }
            else if (droppedItem.slot != this)
            {
                ItemInfo presentItem = this.transform.GetComponentInChildren<ItemInfo>();
                presentItem.slot = droppedItem.slot;
                presentItem.transform.SetParent(droppedItem.slot.transform);
                presentItem.transform.position = droppedItem.slot.transform.position;
                droppedItem.slot = this;
                inv.assignedItemBySlot[droppedItem.slot] = presentItem.item;
                inv.assignedItemBySlot[this] = droppedItem.item;
            }
        }
    }
}
