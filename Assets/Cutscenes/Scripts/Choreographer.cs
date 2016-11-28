using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Choreographer : MonoBehaviour {

	private List<Animator> animators;

	// Use this for initialization
	void Start () {
		GameObject[] characters = GameObject.FindGameObjectsWithTag ("Character");
		animators = new List<Animator> ();
		foreach (GameObject character in characters){
			animators.Add (character.GetComponent<Animator> ());
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)){
			foreach (Animator anim in animators) {
				anim.SetTrigger ("Start");
			}
		}
	}
}
