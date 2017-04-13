using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoveButton : MonoBehaviour {

	private Text buttonText;

	// Use this for initialization
	void Start () {
		buttonText = GetComponentInChildren<Text>();
	}

	public void UpdateText(int moves) {
		buttonText.text = "Move (" + moves.ToString() + ")";
	}

}
