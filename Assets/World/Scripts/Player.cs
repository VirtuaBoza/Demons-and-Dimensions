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
		
	}

	void FixedUpdate(){
		float moveX = Input.GetAxis("Horizontal");
		animator.SetFloat("speedX", moveX);
		float moveY = Input.GetAxis("Vertical");
		animator.SetFloat("speedY", moveY);

		GetComponent<Rigidbody2D>().velocity = new Vector2 (moveX * playerSpeed, moveY * playerSpeed);
	}

}
