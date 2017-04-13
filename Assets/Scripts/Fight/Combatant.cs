using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combatant : MonoBehaviour {

	public string myName;
	public int hp;
	public int maxHp;
	public int speed;
	public int remainingMoves;
	public int remainingActions = 1;
	public int initiative;
	public bool isFriendly;
	public bool isTurn = false;
	public float moveSpeed = 3f;
	[HideInInspector]public GameObject infoBox;

	private Button button;
	private Animator animator;
	private bool isMoving = false;
	private Vector3 targetPosition = new Vector3();

	void Awake() {
		button = GetComponentInChildren<Button>();
		animator = GetComponent<Animator>();
	}

	void Update() {
		if (isMoving && targetPosition != transform.localPosition) {
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, moveSpeed * Time.deltaTime);
			GetComponent<SpriteRenderer>().sortingOrder = 10 - ((int)transform.localPosition.y);
		} else if (isMoving) {
			isMoving = false;
			ResetAnimation();
		}
	}

	public void StartTurn() {
		button.gameObject.SetActive(true);
		button.interactable = false;
		infoBox.GetComponent<Animator>().SetBool("isTurn", true);
		isTurn = true;
		remainingMoves = speed;
		if (GetComponent<AI>()) GetComponent<AI>().StartTurn();
	}

	public void EndTurn() {
		button.gameObject.SetActive(false);
		infoBox.GetComponent<Animator>().SetBool("isTurn", false);
		isTurn = false;
	}

	public void MoveCombatant(Vector3 here) {
		targetPosition = here;
		isMoving = true;
		float xDiff = targetPosition.x - transform.localPosition.x;
		float yDiff = targetPosition.y - transform.localPosition.y;
		Animate(xDiff, yDiff);
		remainingMoves -= (int)Mathf.Abs(xDiff) + (int)Mathf.Abs(yDiff);
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
