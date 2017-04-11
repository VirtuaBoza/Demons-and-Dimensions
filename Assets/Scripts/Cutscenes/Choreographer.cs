using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choreographer : MonoBehaviour {

	public virtual void SetSpeaker(string speaker) {
		Debug.Log("You're calling the parent choreographer class somehow.");
	}

	public virtual void CueBlocking(int index){
		Debug.Log("You're calling the parent choreographer class somehow.");
	}
}
