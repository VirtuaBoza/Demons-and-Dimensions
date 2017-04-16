using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveTarget : MonoBehaviour {

	private Button button;
	private FightManager fightManager;

	void Start () {
		button = GetComponent<Button>();
		fightManager = FindObjectOfType<FightManager>();
	}

	void OnMouseEnter() {
		if (button.IsInteractable()) {
			button.Select();
		}
	}

	public void OnMouseDown() {
		fightManager.ExitMoveSelection(transform.localPosition);
	}

}
