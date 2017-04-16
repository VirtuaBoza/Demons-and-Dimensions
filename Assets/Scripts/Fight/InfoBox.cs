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
		hpTextBox.text = "HP: <color=green>" + currentHp.ToString () + "</color>/" + maximumHp.ToString();
	}

	public void SetHP(int currentHp) {
		string color;
		if (currentHp <= 0.25f * maxHP) color = "red";
		else if (currentHp <= 0.5f * maxHP) color = "orange";
		else if (currentHp < maxHP) color = "black";
		else color = "green";
		hpTextBox.text = "HP: <color=" + color + ">" + currentHp.ToString () + "</color>/" + maxHP.ToString();
	}
}
