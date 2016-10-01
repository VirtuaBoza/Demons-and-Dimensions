using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActionsButton : MonoBehaviour {

	private Text buttonText;

	// Use this for initialization
	void Start () {
		buttonText = GetComponentInChildren<Text>();
		buttonText.text = "Actions (" + 1.ToString() + ")"; //TODO This will pull the acting player's action count
	}

}
