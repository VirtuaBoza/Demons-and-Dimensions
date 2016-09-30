using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

public class FightMenuButton : MonoBehaviour, ISelectHandler, IDeselectHandler {

	public Sprite subMenuInactive;

	private GameObject eventSystem;

	void Awake () {
		eventSystem = GameObject.Find ("EventSystem");
	}

//	public void OnPointerEnter(PointerEventData eventData) {
	//		OnSelect(eventData);    IF YOU PUT THIS BACK, ADD IPointerEnterHandler to the top!!!!!!!!!!!!!!
//	}

	public void OnSelect(BaseEventData eventData) {

		EmboldenText();

		if (eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject != gameObject){
			eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem> ().SetSelectedGameObject (gameObject);

		}

		FightMenuButton[] fightMenuButtons = transform.parent.GetComponentsInChildren<FightMenuButton>();
		foreach(FightMenuButton fightMenuButton in fightMenuButtons){
			if(fightMenuButton.subMenuInactive){
				fightMenuButton.GetComponent<Button>().image.overrideSprite = GetComponent<FightMenuButton>().subMenuInactive;
			}
		}

	}
	
	public void EmboldenText(){
		GetComponentInChildren<Text>().fontStyle = FontStyle.Bold;
	}

	public void OnDeselect(BaseEventData eventData) {
		UnboldenText ();

	}

	public void UnboldenText(){
		GetComponentInChildren<Text>().fontStyle = FontStyle.Normal;
	}


}
