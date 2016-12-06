using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EquipToggle : MonoBehaviour {

	public Sprite brightButton, dullButton;

	public void IlluminateToggle () {
		if (GetComponent<Toggle>().isOn){
			GetComponent<Image>().sprite = brightButton;
		} else {
			GetComponent<Image>().sprite = dullButton;
		}
	}
}
