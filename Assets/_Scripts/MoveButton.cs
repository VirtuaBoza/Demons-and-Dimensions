using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoveButton : MonoBehaviour {

	private Text buttonText;

	// Use this for initialization
	void Start () {
		buttonText = GetComponentInChildren<Text>();
		buttonText.text = "Move (" + 3.ToString() + ")"; //TODO This will pull the acting player's move count
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
