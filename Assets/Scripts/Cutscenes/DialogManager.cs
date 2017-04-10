using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogManager : MonoBehaviour {

	public TextAsset textFile;
	public string[] linesOfText;
	public Text speaker, dialog;
	public float letterPause = 0.05f;
	public float sentencePause = 0.5f;
	public AudioClip crystalTypeSound,damienTypeSound,hunterTypeSound,teddyTypeSound,otherTypeSound;
	public bool soundEnabled = true;
	public Image dialogArrow;

	private int speakerIndex = 0;
	private int dialogIndex = 1;
	private bool isTyping = false;
	private IEnumerator coroutine;

	private Choreographer choreographer;
	private int choreographyIndex = 0;

	// Use this for initialization
	void Start () {

		choreographer = FindObjectOfType<Choreographer>();

		if (textFile != null) {
			linesOfText = (textFile.text.Split('\n'));
			UpdateText();
		}
		dialogArrow.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)){
			UpdateText ();
		}
	}

	public void UpdateText () {
		if (isTyping) {
			FinishTyping ();
		} else if (dialogIndex + 1 < linesOfText.Length) {
			isTyping = true;
			speakerIndex += 2;
			dialogIndex += 2;
			speaker.text = linesOfText[speakerIndex];
			ColorSpeakerText(linesOfText[speakerIndex]);
			dialog.text = "";
			coroutine = TypeText (speaker.text);
			StartCoroutine(coroutine);
			dialogArrow.gameObject.SetActive (false);

			choreographer.CueBlocking(choreographyIndex);
			choreographyIndex++;
		} else {
			EndScene();
		}
	}

	IEnumerator TypeText(string speaker) {
		int charPosition = 0;
		foreach (char letter in linesOfText[dialogIndex].ToCharArray()){
			dialog.text += letter;
			charPosition++;
			if (charPosition == linesOfText[dialogIndex].ToCharArray().Length){
				FinishTyping ();
			}
			if (soundEnabled && charPosition % 2 == 0 && charPosition != linesOfText[dialogIndex].ToCharArray().Length) {
				if (speaker.Contains("Damien")) {
					GetComponent<AudioSource> ().PlayOneShot (damienTypeSound);
				} else if (speaker.Contains("Crystal")) {
					GetComponent<AudioSource> ().PlayOneShot (crystalTypeSound);
				} else if (speaker.Contains("Teddy")) {
					GetComponent<AudioSource> ().PlayOneShot (teddyTypeSound);
				} else if (speaker.Contains("Hunter")) {
					GetComponent<AudioSource> ().PlayOneShot (hunterTypeSound);
				} else {
					GetComponent<AudioSource> ().PlayOneShot (otherTypeSound);
				}
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

	void EndScene () {
		Debug.Log("This log indicates that the scene should end here when i can manage to get around to adding that feature");
	}

}
