using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float playerSpeed;

	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent <Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey("right")) {
			animator.SetBool ("isWalkingRight", true);
			transform.Translate (Vector3.right * Time.deltaTime * playerSpeed, Space.World);
		} else {
			animator.SetBool ("isWalkingRight", false);
		}

		if (Input.GetKey("up")) {
			animator.SetBool ("isWalkingUp", true);
			transform.Translate (Vector3.up * Time.deltaTime * playerSpeed, Space.World);
		} else {
			animator.SetBool ("isWalkingUp", false);
		}

		if (Input.GetKey("left")) {
			animator.SetBool ("isWalkingLeft", true);
			transform.Translate (Vector3.left * Time.deltaTime * playerSpeed, Space.World);
		} else {
			animator.SetBool ("isWalkingLeft", false);
		}

		if (Input.GetKey("down")) {
			animator.SetBool ("isWalkingDown", true);
			transform.Translate (Vector3.down * Time.deltaTime * playerSpeed, Space.World);
		} else {
			animator.SetBool ("isWalkingDown", false);
		}
	}

}
