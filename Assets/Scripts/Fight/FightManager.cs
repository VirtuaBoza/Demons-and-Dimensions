using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public enum DIE {d20,d12,d10,d8,d6,d4,d00}

public class FightManager : MonoBehaviour {
	
	public GameObject moveTarget;

	public List<Vector3> currentPositionList = new List<Vector3>();
	public Combatant currentPlayer;

	private FightMenuFrame fightMenuFrame;
	private List<Combatant> currentCombatants = new List<Combatant>();
	private Dictionary<Combatant, Button> combatantButtons = new Dictionary<Combatant, Button>();

	private bool spellMode = false;
	private bool buffMode = false;

	void Awake() {
		fightMenuFrame = FindObjectOfType<FightMenuFrame>();
	}

	public void StartFight(List<Combatant> combatants) {
		foreach (Combatant combatant in combatants) {
			combatantButtons.Add(combatant,combatant.GetComponentInChildren<Button>());
			combatant.GetComponentInChildren<Button>().gameObject.SetActive(false);
			currentPositionList.Add(combatant.transform.localPosition);
		}
		currentCombatants = combatants;
		RunGame();
	}

	public void RunGame() {
		currentCombatants[0].StartTurn();
		currentPlayer = currentCombatants[0];
		if (currentCombatants[0].isFriendly) {
			fightMenuFrame.ActivateWaitPanel(false);
			fightMenuFrame.ActivateFightMenu(true);
			ActionsButton actionsButton = FindObjectOfType<ActionsButton>();
			actionsButton.UpdateText(currentCombatants[0].remainingActions);
			actionsButton.GetComponent<Selectable>().Select();
			actionsButton.GetComponent<Animator>().SetTrigger("Highlighted");
			MoveButton moveButton = FindObjectOfType<MoveButton>();
			moveButton.UpdateText(currentCombatants[0].remainingMoves);
		} else {
			fightMenuFrame.ActivateWaitPanel(true);
			fightMenuFrame.ActivateFightMenu(false);
		}
	}

	public void EndTurn() {
		Combatant thisOne = currentCombatants[0];
		thisOne.EndTurn();
		currentCombatants.RemoveAt(0);
		currentCombatants.Add(thisOne);
		RunGame();
	}

	public void EnterTargetSelection(ACTION action){
		
		fightMenuFrame.ActivateFightMenu(false);
		fightMenuFrame.ActivateTargetPanel(true);

		bool oneIsSelected = false;
		if(action == ACTION.Attacking || action == ACTION.Casting){
			foreach (KeyValuePair<Combatant, Button> entry in combatantButtons) {
				if (!entry.Key.isFriendly) {
					entry.Value.gameObject.SetActive(true);
					entry.Value.interactable = true;
					if (!oneIsSelected) {
						entry.Value.Select();
						oneIsSelected = true;
					}
				}
			}
		} else if (action == ACTION.Buffing) {
			foreach (KeyValuePair<Combatant, Button> entry in combatantButtons) {
				if (entry.Key.isFriendly) {
					entry.Value.gameObject.SetActive(true);
					entry.Value.interactable = true;
					if (!oneIsSelected) {
						entry.Value.Select();
						oneIsSelected = true;
					}
				}
			}
			buffMode = true;
		}

		if (action == ACTION.Casting) {
			spellMode = true;
		}

	}

	public void ExitTargetSelection() {
		
		Debug.Log("Resolve the selection and make the buttons go away.");

		fightMenuFrame.ActivateFightMenu(true);
		fightMenuFrame.ActivateTargetPanel(false);

		if(spellMode){
			FindObjectOfType<AttackSpellOptions>().GetComponentsInChildren<Button>()[0].Select();
		} else if(buffMode){
			FindObjectOfType<BuffSpellOptions>().GetComponentsInChildren<Button>()[0].Select();
		} else {
			FindObjectOfType<AttackOptions>().GetComponentsInChildren<Button>()[0].Select();
		}

		spellMode = false;
		buffMode = false;

	}

	public void EnterMoveSelection() {

		fightMenuFrame.ActivateFightMenu(false);
		fightMenuFrame.ActivateTargetPanel(true);

		bool oneIsSelected = false;
		foreach (Combatant combatant in currentCombatants) {
			if (combatant.isTurn) {
				for (int x = 1; x <= 8; x++) {
					for (int y = 1; y <= 8; y++) {
						Vector3 possibleOption = new Vector3(x,y);
						if (Mathf.Abs(x - combatant.transform.localPosition.x) + Mathf.Abs(y - combatant.transform.localPosition.y) <= combatant.remainingMoves && !currentPositionList.Contains(possibleOption)) {
							GameObject target = Instantiate(moveTarget,FindObjectOfType<Arena>().transform,false) as GameObject;
							target.transform.localPosition = possibleOption;
							if (!oneIsSelected) {
								target.GetComponent<Button>().Select();
								oneIsSelected = true;
							}
						}
					}
				}
			}
		}
	}

	public void ExitMoveSelection(Vector3 target) {
		foreach (MoveTarget targetButton in FindObjectsOfType<MoveTarget>()) {
			Destroy(targetButton.gameObject);
		}
		foreach (Combatant combatant in currentCombatants) {
			if (combatant.isTurn) {
				UpdatePositionList(combatant.transform.localPosition,target);
				combatant.MoveCombatant(target);
				fightMenuFrame.ActivateFightMenu(true);
				fightMenuFrame.ActivateTargetPanel(false);
				FindObjectOfType<MoveButton>().UpdateText(combatant.remainingMoves);
				if (combatant.remainingMoves > 0) FindObjectOfType<MoveButton>().GetComponent<Button>().Select();
				else {
					if (combatant.remainingActions > 0) FindObjectOfType<ActionsButton>().GetComponent<Toggle>().Select();
					else GameObject.Find("EndTurn").GetComponent<Button>().Select();
				}
			}
		}
	}

	public void UpdatePositionList(Vector3 oldPosition, Vector3 newPosition) {
		currentPositionList.Remove(oldPosition);
		currentPositionList.Add(newPosition);
	}

	public void EliminateCombatant(Combatant combatant) {
		currentPositionList.Remove(combatant.transform.localPosition);
		combatantButtons.Remove(combatant);
		currentCombatants.Remove(combatant);
	}

	public int RollADie(DIE die) {
		int roll;
		switch (die) {
		case DIE.d20:
			roll = Random.Range(1,21);
			break;
		case DIE.d12:
			roll = Random.Range(1,13);
			break;
		case DIE.d10:
			roll = Random.Range(1,11);
			break;
		case DIE.d8:
			roll = Random.Range(1,9);
			break;
		case DIE.d6:
			roll = Random.Range(1,7);
			break;
		case DIE.d4:
			roll = Random.Range(1,5);
			break;
		case DIE.d00:
			roll = Random.Range(1,11);
			roll = roll * 10;
			break;
		default:
			roll = 0;
			break;
		}
		return roll;
	}
}
