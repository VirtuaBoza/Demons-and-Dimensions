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
		Debug.Log("This is supposed to make the text bold");
		buttonText.fontStyle = FontStyle.Bold;
	}

	public void UnboldenText(){
		Debug.Log("This is supposed to make the text bold");
		buttonText.fontStyle = FontStyle.Normal;
	}
}
