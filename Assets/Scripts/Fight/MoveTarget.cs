using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MoveTarget : MonoBehaviour, IPointerEnterHandler {

	private Button button;
	private FightManager fightManager;

	// Use this for initialization
	void Start () {
		button = GetComponent<Button>();
		fightManager = FindObjectOfType<FightManager>();
		button.onClick.AddListener(() => MyOnClick());
	}

	public void OnPointerEnter(PointerEventData eventData) {
		if (button.IsInteractable()) {
			button.Select();
		}
	}

	void MyOnClick() {
		fightManager.ExitMoveSelection(transform.localPosition);
	}

	void Destroy() {
		button.onClick.RemoveAllListeners();
	}
}
