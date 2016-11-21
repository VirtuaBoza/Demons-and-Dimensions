using UnityEngine;
using System.Collections;

public class Choreographer : MonoBehaviour {

	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = FindObjectOfType<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)){
			animator.SetTrigger ("Start");
		}
	}
}
