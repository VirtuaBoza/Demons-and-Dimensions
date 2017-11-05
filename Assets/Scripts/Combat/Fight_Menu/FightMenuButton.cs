using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FightMenuButton : MonoBehaviour, ISelectHandler, IDeselectHandler
{

    public void OnSelect(BaseEventData eventData)
    {
        EmboldenText();
    }

    public void EmboldenText()
    {
        foreach (Text text in GetComponentsInChildren<Text>())
        {
            text.fontStyle = FontStyle.Bold;
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        UnboldenText();
    }

    public void UnboldenText()
    {
        foreach (Text text in GetComponentsInChildren<Text>())
        {
            text.fontStyle = FontStyle.Normal;
        }
    }

    public void MyPointerEnter()
    {
        if (GetComponent<Selectable>().IsInteractable())
        {
            GetComponent<Selectable>().Select();
        }
    }

}
