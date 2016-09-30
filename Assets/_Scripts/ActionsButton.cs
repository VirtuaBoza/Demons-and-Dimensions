using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ActionsButton : MonoBehaviour, ISelectHandler {

	private Text buttonText;
	private GameObject actionButtons;
	private FightMenuButton fightMenuButton;

	// Use this for initialization
	void Start () {
		buttonText = GetComponentInChildren<Text>();
		buttonText.text = "Actions (" + 1.ToString() + ")"; //TODO This will pull the acting player's action count
		actionButtons = GameObject.Find("ActionButtons");
		fightMenuButton = GetComponent<FightMenuButton>();
	}
	
	public void OnSelect(BaseEventData eventData) {
		if(actionButtons){
			actionButtons.SetActive(false);
		}
		if(fightMenuButton.subMenuInactive){
			GetComponent<Button>().image.overrideSprite = fightMenuButton.subMenuInactive;
		}
	}
}
