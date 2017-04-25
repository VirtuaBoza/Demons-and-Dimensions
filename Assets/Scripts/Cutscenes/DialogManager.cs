using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class DialogManager : MonoBehaviour {

	public TextAsset textFile;
	public string[] linesOfText;
	public Text speaker, dialog;
	public float letterPause = 0.05f;
	public float sentencePause = 0.5f;
	public AudioClip crystalTypeSound,damienTypeSound,hunterTypeSound,teddyTypeSound,otherTypeSound;
	public Image advanceDialogArrow;
	public int nextSceneIndex;
	public AnimationClip fadeColorAnimationClip;

	private int speakerIndex = 1;
	private int dialogIndex = 2;
	private bool isTyping = false;
	private IEnumerator coroutine;
	private AudioSource audioSource;

	private Choreographer choreographer;
	private int choreographyIndex = 0;
	public Animator animColorFade;
	private bool sceneIsEnded = false;

	// Use this for initialization
	void Start() {

		audioSource = GetComponent<AudioSource>();
		choreographer = FindObjectOfType<Choreographer>();

		if (GameObject.Find("FadeImage")) {
			animColorFade = GameObject.Find("FadeImage").GetComponent<Animator>();
		}

		if (textFile != null) {
			linesOfText = (textFile.text.Split('\n'));
			Invoke ("UpdateText", fadeColorAnimationClip.length * .5f);
		}
		advanceDialogArrow.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update() {
		if (Input.GetKeyDown(KeyCode.Space) && !sceneIsEnded){
			UpdateText ();
		}
	}

	private void UpdateText() {
		if (isTyping) {
			FinishTyping ();
		} else if (dialogIndex < linesOfText.Length) {
			isTyping = true;
			speaker.text = linesOfText[speakerIndex];
			choreographer.SetSpeaker(speaker.text);
			ColorSpeakerText(linesOfText[speakerIndex]);
			dialog.text = "";
			coroutine = TypeText (speaker.text);
			StartCoroutine(coroutine);
			advanceDialogArrow.gameObject.SetActive (false);
			choreographer.CueBlocking(choreographyIndex);
		} else {
			sceneIsEnded = true;
			choreographer.CueBlocking(choreographyIndex);
			EndScene();
		}
	}

	IEnumerator TypeText(string speaker) {
		int charPosition = 1;
		foreach (char letter in linesOfText[dialogIndex].ToCharArray()) {
			dialog.text += letter;
			charPosition++;
			if (charPosition == linesOfText[dialogIndex].ToCharArray().Length) {
				FinishTyping();
			}
			if (charPosition % 2 == 0 && charPosition != linesOfText[dialogIndex].ToCharArray().Length) {
				if (speaker.Contains("Damien")) {
					audioSource.PlayOneShot (damienTypeSound);
				} else if (speaker.Contains("Crystal")) {
					audioSource.PlayOneShot (crystalTypeSound);
				} else if (speaker.Contains("Teddy")) {
					audioSource.PlayOneShot (teddyTypeSound);
				} else if (speaker.Contains("Hunter")) {
					audioSource.PlayOneShot (hunterTypeSound);
				} else {
					audioSource.PlayOneShot (otherTypeSound);
				}
			}
			if (letter == '.' || letter == '?' || letter == '!') {
				yield return new WaitForSeconds (sentencePause);
			} else {
				yield return new WaitForSeconds (letterPause);
			}
		}
	}

	void FinishTyping() {
		StopAllCoroutines();
		isTyping = false;
		dialog.text = linesOfText[dialogIndex];
		dialogIndex += 3;
		speakerIndex += 3;
		choreographyIndex++;
		advanceDialogArrow.gameObject.SetActive (true);
	}

	void ColorSpeakerText(string speaker) {
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

	void EndScene() {
		//Use invoke to delay calling of LoadDelayed by half the length of fadeColorAnimationClip
		Invoke ("LoadDelayed", fadeColorAnimationClip.length * .5f);

		//Set the trigger of Animator animColorFade to start transition to the FadeToOpaque state.
		animColorFade.SetTrigger ("fade");
	}

	void LoadDelayed() {
		SceneManager.LoadScene (nextSceneIndex);
	}

}
