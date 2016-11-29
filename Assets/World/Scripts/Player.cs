using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float playerSpeed;
	public bool recordMode = false;

	private Animator animator;
	private float moveX, moveY;
	private float timer = 0f;
	private int frame = 0;

	// Use this for initialization
	void Start () {
		animator = GetComponent <Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		if (((recordMode && moveX != 0f) || (recordMode && moveY != 0f)) && timer == 0f) {
			frame++;
			Debug.Log ("Frame: " + frame + "X: " + transform.position.x + " Y: " + transform.position.y);
			timer += Time.deltaTime;
			if (timer >= (1/15)){
				timer = 0f;
			}
		} else {
			frame = 0;
		}
	}

	void FixedUpdate(){
		moveX = Input.GetAxis("Horizontal");
		animator.SetFloat("speedX", moveX);
		moveY = Input.GetAxis("Vertical");
		animator.SetFloat("speedY", moveY);

		GetComponent<Rigidbody2D>().velocity = new Vector2 (moveX * playerSpeed, moveY * playerSpeed);
	}

}
