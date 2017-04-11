using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combatant : MonoBehaviour {

	public string myName;
	public int hp;
	public int maxHp;
	public int initiative;
	public bool isFriendly;
	[HideInInspector]public GameObject infoBox;

	private Button button;

	void Awake() {
		button = GetComponentInChildren<Button>();
	}

	public void StartTurn() {
		button.gameObject.SetActive(true);
		button.interactable = false;
		infoBox.GetComponent<Animator>().SetBool("isTurn", true);
	}

	public void EndTurn() {
		button.gameObject.SetActive(false);
		infoBox.GetComponent<Animator>().SetBool("isTurn", false);
	}

}
