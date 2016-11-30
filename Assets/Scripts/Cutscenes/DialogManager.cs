using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogManager : MonoBehaviour {

	public TextAsset textFile;
	public string[] linesOfText;
	public Text speaker, dialog;
	public float letterPause = 0.05f;
	public float sentencePause = 0.5f;
	public AudioClip sound;
	public bool soundEnabled = true;
	public Image dialogArrow;

	private int speakerIndex = 0;
	private int dialogIndex = 1;
	private bool isTyping = false; 

	// Use this for initialization
	void Start () {
		if (textFile != null) {
			linesOfText = (textFile.text.Split('\n'));
			UpdateText();
		}
		dialogArrow.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.Space) && dialogIndex + 1 <= linesOfText.Length){
			UpdateText ();
		}
			
	}

	public void UpdateText () {
		if(!isTyping){
			isTyping = true;
			speakerIndex += 2;
			dialogIndex += 2;
			speaker.text = linesOfText[speakerIndex];
			ColorSpeakerText(linesOfText[speakerIndex]);
			dialog.text = "";
			StartCoroutine(TypeText());
			dialogArrow.gameObject.SetActive (false);
		} else {
			FinishTyping ();
		}
	}

	IEnumerator TypeText() {
		int charPosition = 0;
		foreach (char letter in linesOfText[dialogIndex].ToCharArray()){
			dialog.text += letter;
			if (sound && soundEnabled) {
				GetComponent<AudioSource> ().PlayOneShot (sound);
			}
			charPosition++;
			if (charPosition == linesOfText[dialogIndex].ToCharArray().Length){
				Debug.Log ("Reached last character");
				FinishTyping ();
			}
			if(letter == '.' || letter == '?') {
				yield return new WaitForSeconds (sentencePause);
			} else {
				yield return new WaitForSeconds (letterPause);
			}
		}
	}

	void FinishTyping() {
		StopAllCoroutines();
		isTyping = false;
		dialog.text = linesOfText [dialogIndex];
		dialogArrow.gameObject.SetActive (true);
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
