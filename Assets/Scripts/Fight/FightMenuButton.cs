using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

public class FightMenuButton : MonoBehaviour, ISelectHandler, IDeselectHandler {

	//public bool isSelected = false;

	//IF YOU PUT THIS BACK, ADD IPointerEnterHandler to the top!!!!!!!!!!!!!!
//	public void OnPointerEnter(PointerEventData eventData) {
//			OnSelect(eventData);    
//	}

	public void OnSelect(BaseEventData eventData) {
		EmboldenText();
	}
	
	public void EmboldenText() {
		foreach (Text text in GetComponentsInChildren<Text>()) {
			text.fontStyle = FontStyle.Bold;
		}
	}

	public void OnDeselect(BaseEventData eventData) {
		UnboldenText ();
	}

	public void UnboldenText() {
		foreach (Text text in GetComponentsInChildren<Text>()) {
			text.fontStyle = FontStyle.Normal;
		}
	}

	public void MyPointerEnter() {
		if (GetComponent<Selectable>().IsInteractable()) {
			GetComponent<Selectable>().Select();
		}
	}

}
