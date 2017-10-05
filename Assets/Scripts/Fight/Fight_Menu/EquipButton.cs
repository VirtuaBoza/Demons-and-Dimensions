using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipButton : MonoBehaviour {

	private Button button;

	void Start () {
		button = GetComponent<Button> ();
		button.onClick.AddListener(() => OnMyClick());
	}

	void OnMyClick() {
		FindObjectOfType<FightManager>().EnterEquip();
	}

}
