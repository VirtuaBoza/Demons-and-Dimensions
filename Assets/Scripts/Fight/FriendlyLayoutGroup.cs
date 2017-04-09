using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FriendlyLayoutGroup : MonoBehaviour {

	public GameObject friendlyInfoBox;

	public GameObject PopulateInfoBox(string name, int hp, int maxHp){
		GameObject thisBox = Instantiate (friendlyInfoBox, transform) as GameObject;
		thisBox.GetComponentsInChildren<Text> () [0].text = name;
		thisBox.GetComponentsInChildren<Text> () [1].text = "HP: " + hp.ToString () + "/" + maxHp.ToString();
		return thisBox;
	}
	
}
