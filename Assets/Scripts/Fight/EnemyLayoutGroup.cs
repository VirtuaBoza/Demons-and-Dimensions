using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyLayoutGroup : MonoBehaviour {

	public GameObject enemyInfoBox;

	public void PopulateInfoBox(string name, int hp, int maxHp){
		GameObject thisBox = Instantiate (enemyInfoBox, transform) as GameObject;
		thisBox.GetComponentsInChildren<Text> () [0].text = name;
		thisBox.GetComponentsInChildren<Text> () [1].text = "HP: " + hp.ToString () + "/" + maxHp.ToString();

	}

}
