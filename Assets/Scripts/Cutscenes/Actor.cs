using UnityEngine;
using System.Collections;

public class Actor : MonoBehaviour {

	public float playerSpeed = 3f;

	private Animator animator;
	private Vector3 target, lastTarget;

	// Use this for initialization
	void Start () {
		animator = GetComponent <Animator>();
		target = transform.position;
		lastTarget = target;
	}
	
	// Update is called once per frame
	void Update () {
		
		GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		transform.position = Vector3.MoveTowards(transform.position, target, playerSpeed * Time.deltaTime);

		// Resets animation
		if (transform.position == target || target != lastTarget){
			ResetAnimation();
		} else {
			
			// Animates movement

			float xDiff = target.x - transform.position.x;
			float yDiff = target.y - transform.position.y;
			Animate(xDiff, yDiff);

		}

		lastTarget = target;

	}

	// Called by Ground.cs when the ground is clicked
	public void MovePlayer(Vector3 here){
		target = here;

	}

	void Animate (float x, float y) {
		if (Mathf.Abs(x) > Mathf.Abs(y)){
			animator.SetFloat("speedX", x);
			animator.SetFloat("speedY", 0f);
		} else {
			animator.SetFloat("speedY", y);
			animator.SetFloat("speedX", 0f);
		}
	}

	void ResetAnimation(){
		animator.SetFloat("speedX", 0f);
		animator.SetFloat("speedY", 0f);
	}
}
