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
	public GameObject infoBox;

	public void StartTurn() {
		GetComponentInChildren<Image>().GetComponent<Animator>().SetBool("isTurn",true);
		infoBox.GetComponent<Animator>().SetBool("isTurn",true);
	}

	public void EndTurn() {
		GetComponentInChildren<Image>().GetComponent<Animator>().SetBool("isTurn",false);
		infoBox.GetComponent<Animator>().SetBool("isTurn",false);
	}

}
