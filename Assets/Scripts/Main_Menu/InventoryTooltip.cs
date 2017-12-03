using UnityEngine;
using UnityEngine.UI;

public class InventoryTooltip : MonoBehaviour
{
    private Item item;
    private string info;
    private GameObject tooltip;

    void Start()
    {
        tooltip = GameObject.Find("Tooltip");
        tooltip.SetActive(false);
    }

    void Update()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        if (tooltip.activeSelf)
        {
            tooltip.transform.position = Input.mousePosition;
        }
    }

    public void Activate(Item item)
    {
        this.item = item;
        ContstructInfoString();
        tooltip.SetActive(true);
    }

    public void Deactivate()
    {
        tooltip.SetActive(false);
    }

    public void ContstructInfoString()
    {
        info = "<color=#0047ba><b>" + item.Title + "</b></color>\n\n" + item.ItemType;
        tooltip.transform.GetComponentInChildren<Text>().text = info;
    }
}
