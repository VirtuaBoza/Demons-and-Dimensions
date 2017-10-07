using UnityEngine;
using UnityEngine.EventSystems;

public class ItemInfo : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler {

	public Item item;
	public int amount = 1;
	public Slot slot;

	private Inventory inv;
	private InventoryTooltip tooltip;

	void Start () {
		inv = FindObjectOfType<Inventory>();
		tooltip = GetComponentInParent<InventoryTooltip>();
	}

	public void OnBeginDrag (PointerEventData eventData) {
		if (item !=  null) {
			this.transform.SetParent(this.transform.parent.parent.parent);
			this.transform.position = eventData.position;
			GetComponent<CanvasGroup>().blocksRaycasts = false;
		}
	}

	public void OnDrag (PointerEventData eventData) {
		if (item !=  null) {
			this.transform.position = eventData.position;
		}
	}

	public void OnEndDrag (PointerEventData eventData) {
		foreach (Slot panelSlot in inv.assignedItems.Keys) {
			if (panelSlot == slot) {
				this.transform.SetParent (panelSlot.transform);
				this.transform.position = panelSlot.transform.position;
			}
		}
		GetComponent<CanvasGroup>().blocksRaycasts = true;
		inv.UpdateInventory();
	}

	public void OnPointerEnter (PointerEventData eventData) {
		tooltip.Activate(item);
	}

	public void OnPointerExit (PointerEventData eventData) {
		tooltip.Deactivate();
	}

}
