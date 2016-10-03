using UnityEngine;
using System.Collections;

public class FightMenu : MonoBehaviour {

	public void ActivateMenu(bool value){
		if(value){
			gameObject.SetActive(true);
		} else {
			gameObject.SetActive(false);
		}
	}



}
