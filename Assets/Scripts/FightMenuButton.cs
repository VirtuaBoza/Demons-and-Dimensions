using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FightMenuButton : MonoBehaviour {

	private Text buttonText;

	// Use this for initialization
	void Start () {
		buttonText = GetComponent<Text>();
		Debug.Log(buttonText.text);
	}
	
	public void EmboldenText(){
		buttonText.fontStyle = FontStyle.Bold;
	}

	public void UnboldenText(){
		buttonText.fontStyle = FontStyle.Normal;
	}

}
