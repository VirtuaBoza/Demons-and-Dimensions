using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EnemyLayoutGroup : MonoBehaviour {

	public GameObject enemyInfoBox;

	public List<EnemyInfoBox> enemiesInBattle = new List<EnemyInfoBox>();

	public void PopulateInfoBox(string name, int hp, int maxHp){
		GameObject thisBox = Instantiate (enemyInfoBox, transform) as GameObject;
		EnemyInfoBox infoBox =	thisBox.GetComponent<EnemyInfoBox>();
		infoBox.enemyName = name;
		infoBox.currentHp = hp;
		infoBox.maxHp = maxHp;
		infoBox.UpdateText();

		enemiesInBattle.Add(infoBox);
	}

}
