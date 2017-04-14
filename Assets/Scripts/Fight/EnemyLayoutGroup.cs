using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyLayoutGroup : MonoBehaviour {

	public GameObject enemyInfoBox;

	public InfoBox PopulateInfoBox(string name, int hp, int maxHp){
		GameObject thisBox = Instantiate (enemyInfoBox, transform) as GameObject;
		InfoBox infoBox = thisBox.GetComponent<InfoBox>();
		infoBox.SetName(name);
		infoBox.SetHP(hp,maxHp);
		return infoBox;
	}

}
