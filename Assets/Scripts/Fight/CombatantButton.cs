using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CombatantButton : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler {

	private Button button;
	private FightManager fightManager;

	// Use this for initialization
	void Start () {
		button = GetComponent<Button>();
		button.onClick.AddListener(() => MyOnClick());
		fightManager = FindObjectOfType<FightManager>();
	}
	
	public void OnPointerEnter(PointerEventData eventData) {
		if (button.IsInteractable()) {
			button.Select();
		}
	}

	public void OnSelect(BaseEventData eventData) {
		foreach (Text text in GetComponentInParent<Combatant>().infoBox.GetComponentsInChildren<Text>()) text.color = Color.red;
	}

	public void OnDeselect(BaseEventData eventData) {
		foreach (Text text in GetComponentInParent<Combatant>().infoBox.GetComponentsInChildren<Text>()) text.color = Color.black;
	}

	void MyOnClick() {
		fightManager.ExitTargetSelection();
	}

	void Destroy () {
		button.onClick.RemoveAllListeners();
	}
}
