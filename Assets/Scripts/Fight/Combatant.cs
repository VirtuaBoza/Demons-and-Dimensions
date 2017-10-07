using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combatant : MonoBehaviour {

	public string myName;
	public CHARACTER character;
	public int aC;
	public int maxHp;
	[HideInInspector]public int currentHp;
	public int speed;
	public int remainingMoves;
	public int actions = 1;
	public int remainingActions;
	public int initiative;
	public bool isFriendly;
	[HideInInspector]public bool isTurn = false;
	public float moveSpeed = 3f;
	[HideInInspector]public InfoBox infoBox;
	public List<Item> equippedItems = new List<Item>();

	private Button button;
	private Animator animator;
	private bool isMoving = false;
	private Vector3 targetPosition = new Vector3();
	private Text popUpText;

	void Awake() {
		button = GetComponentInChildren<Button>();
		animator = GetComponent<Animator>();
		popUpText = GetComponentInChildren<Text>();
		currentHp = maxHp;
	}

	void Update() {
		if (isMoving && targetPosition != transform.localPosition) {
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, moveSpeed * Time.deltaTime);
			GetComponent<Renderer>().sortingOrder = 10 - ((int)transform.localPosition.y);
		} else if (isMoving) {
			isMoving = false;
			ResetAnimation();
			if (GetComponent<AI>()) GetComponent<AI>().FinishedMoving();
		}
	}

	public void StartTurn() {
		button.gameObject.SetActive(true);
		button.interactable = false;
		infoBox.GetComponent<Animator>().SetBool("isTurn", true);
		isTurn = true;
		remainingMoves = speed;
		remainingActions = actions;
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

	public void TakeDamage(int amount) {
		if (amount == 0) {
			PopUpText (Color.black, "Miss");
		} else {
			currentHp -= amount;
			PopUpText(Color.red,"-"+amount.ToString());
			infoBox.SetHP(Mathf.Max(0,currentHp));
			if (currentHp <= 0) {
				animator.SetTrigger("die");
				Invoke("Die",0.9f);
			}
		} 
	}

	void Die() {
		FindObjectOfType<FightManager>().EliminateCombatant(this);
		Destroy(this.gameObject);
	}

	public void PopUpText(Color color, string text) {
		popUpText.color = color;
		popUpText.text = text;
		popUpText.GetComponent<Animator>().SetTrigger("popUp");
	}

}
