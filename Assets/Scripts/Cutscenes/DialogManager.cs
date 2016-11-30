using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogManager : MonoBehaviour {

	public TextAsset textFile;
	public string[] linesOfText;
	public Text speaker, dialog;

	private int speakerIndex = 0;
	private int dialogIndex = 1;

	// Use this for initialization
	void Start () {
		if (textFile != null) {
			linesOfText = (textFile.text.Split('\n'));
			UpdateText();
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.RightArrow) && dialogIndex <= linesOfText.Length){
			speakerIndex += 2;
			dialogIndex += 2;
			UpdateText();
		}

	}

	void UpdateText () {
		speaker.text = linesOfText[speakerIndex];
		dialog.text = linesOfText[dialogIndex];
		ColorSpeakerText(linesOfText[speakerIndex]);
	}

	void ColorSpeakerText (string speaker) {
		if (speaker.Contains("Damien")) {
			this.speaker.color = Color.red;
		} else if (speaker.Contains("Crystal")) {
			this.speaker.color = Color.blue;
		} else if (speaker.Contains("Teddy")) {
			this.speaker.color = Color.yellow;
		} else if (speaker.Contains("Hunter")) {
			this.speaker.color = Color.green;
		}
	}
}
