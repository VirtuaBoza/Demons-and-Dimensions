using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FriendlyLayoutGroup : MonoBehaviour {

	public GameObject friendlyInfoBox;

	public InfoBox PopulateInfoBox(string name, int hp, int maxHp){
		GameObject thisBox = Instantiate (friendlyInfoBox, transform) as GameObject;
		InfoBox infoBox = thisBox.GetComponent<InfoBox>();
		infoBox.SetName(name);
		infoBox.SetHP(hp,maxHp);
		return infoBox;
	}
	
}
