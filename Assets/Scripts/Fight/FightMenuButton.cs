using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

public class FightMenuButton : MonoBehaviour, ISelectHandler, IDeselectHandler {

	public bool isSelected = false;

	private GameObject eventSystem;

	void Awake () {
		eventSystem = GameObject.Find ("EventSystem");
	}

//	public void OnPointerEnter(PointerEventData eventData) {
	//		OnSelect(eventData);    IF YOU PUT THIS BACK, ADD IPointerEnterHandler to the top!!!!!!!!!!!!!!
//	}

	public void OnSelect(BaseEventData eventData) {
		isSelected = true;
		EmboldenText();

		if (eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject != gameObject){
			eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem> ().SetSelectedGameObject (gameObject);

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
