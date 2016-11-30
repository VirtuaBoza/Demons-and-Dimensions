using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

public class FightMenuButton : MonoBehaviour, ISelectHandler, IDeselectHandler {

	public bool isSelected = false;

	//IF YOU PUT THIS BACK, ADD IPointerEnterHandler to the top!!!!!!!!!!!!!!
//	public void OnPointerEnter(PointerEventData eventData) {
//			OnSelect(eventData);    
//	}

	public void OnSelect(BaseEventData eventData) {
		isSelected = true;
		EmboldenText();

		if (EventSystem.current.GetComponent<EventSystem>().currentSelectedGameObject != gameObject){
			EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject (gameObject);

		}

	}
	
	public void EmboldenText(){
		GetComponentInChildren<Text>().fontStyle = FontStyle.Bold;
	}

	public void OnDeselect(BaseEventData eventData) {
		isSelected = false;
		UnboldenText ();
	}

	public void UnboldenText(){
		GetComponentInChildren<Text>().fontStyle = FontStyle.Normal;
	}

}
