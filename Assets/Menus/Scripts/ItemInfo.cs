using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class ItemInfo : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler {

	public Item item;
	public int amount = 1;
	public int itemSlotID;

	private Inventory inv;
	private Tooltip tooltip;

	void Start () {
		inv = GetComponentInParent<Inventory>();
		tooltip = GetComponentInParent<Tooltip>();
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
		this.transform.SetParent (inv.slots[itemSlotID].transform);
		this.transform.position = inv.slots[itemSlotID].transform.position;
		GetComponent<CanvasGroup>().blocksRaycasts = true;
	}

	public void OnPointerEnter (PointerEventData eventData) {
		tooltip.Activate(item);
	}

	public void OnPointerExit (PointerEventData eventData) {
		tooltip.Deactivate();
	}

}
