using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Assets.Scripts.Game;

public class InventoryTooltip : MonoBehaviour {

	private Item item;
	private string info;
	private GameObject tooltip;

	void Start () {
		tooltip = GameObject.Find("Tooltip");
		tooltip.SetActive(false);
	}

	void Update () {
		if (tooltip.activeSelf) {
			tooltip.transform.position = Input.mousePosition;
		}
	}

	public void Activate (Item item) {
		this.item = item;
		ContstructInfoString();
		tooltip.SetActive(true);
	}

	public void Deactivate () {
		tooltip.SetActive(false);
	}

	public void ContstructInfoString () {
		info = "<color=#0047ba><b>" + item.Title + "</b></color>\n\n" + item.Itemtype;
		tooltip.transform.GetComponentInChildren<Text>().text = info;
	}

}
