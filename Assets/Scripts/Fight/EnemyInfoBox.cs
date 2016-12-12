using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyInfoBox : MonoBehaviour {

	public Text nameText, hpText;
	public string enemyName;
	public int currentHp, maxHp;

	public void UpdateText() {
		nameText.text = enemyName;
		hpText.text = "HP: " + currentHp.ToString() + "/" + maxHp.ToString();
	}
}
