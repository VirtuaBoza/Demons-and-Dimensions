using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoBox : MonoBehaviour {

	private Text nameTextBox,hpTextBox;
	private int maxHP = 0;

	// Use this for initialization
	void Awake () {
		nameTextBox = GetComponentsInChildren<Text>()[0];
		hpTextBox = GetComponentsInChildren<Text>()[1];
	}
	
	public void SetName(string name) {
		nameTextBox.text = name;
	}

	public void SetHP(int currentHp, int maximumHp) {
		maxHP = maximumHp;
		hpTextBox.text = "HP: " + currentHp.ToString () + "/" + maximumHp.ToString();
	}

	public void SetHP(int currentHp) {
		hpTextBox.text = "HP: " + currentHp.ToString () + "/" + maxHP.ToString();
	}
}
