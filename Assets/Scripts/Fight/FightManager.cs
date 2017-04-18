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
	public List<Combatant> currentCombatants = new List<Combatant>();

	private FightMenuFrame fightMenuFrame;
	private Dictionary<Combatant, Button> combatantButtons = new Dictionary<Combatant, Button>();

	private bool spellMode = false;
	private bool buffMode = false;
	private ACTION actionType;

	private int profBonus;
	private DIE damageRange;
	private int damageMulti;
	private DAMAGETYPE damageType;

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


	public void InitiateAttack(int prof, DIE dRange, int multi, DAMAGETYPE dType, int range) {
		profBonus = prof;
		damageRange = dRange;
		damageMulti = multi;
		damageType = dType;
		EnterTargetSelection(ACTION.Attacking,range);
		actionType = ACTION.Attacking;
	}

	void ResolveAttack(Combatant target) {
		Debug.Log ("Pick up here later!");
	}

	public void EnterTargetSelection(ACTION action, int range){
		fightMenuFrame.ActivateFightMenu(false);
		fightMenuFrame.ActivateTargetPanel(true);
		List<Vector3> combatantsInRange = new List<Vector3>();
		for (int x = -range; x <= range; x++) {
			for (int y = -range; y <= range; y++) {
				Vector3 spot = new Vector3(currentPlayer.transform.localPosition.x + x,currentPlayer.transform.localPosition.y + y);
				if (currentPositionList.Contains(spot) && spot != currentPlayer.transform.localPosition) {
					combatantsInRange.Add(spot);
				} 
			}
		}
		bool oneIsSelected = false;
		foreach (KeyValuePair<Combatant, Button> entry in combatantButtons) {
			if (combatantsInRange.Contains(entry.Key.transform.localPosition)) {
				if ((action == ACTION.Attacking || action == ACTION.Casting) && !entry.Key.isFriendly) {
					entry.Value.gameObject.SetActive(true);
					entry.Value.interactable = true;
					if (!oneIsSelected) {
						entry.Value.Select();
						oneIsSelected = true;
					}
				} else if (action == ACTION.Buffing && entry.Key.isFriendly) {
					entry.Value.gameObject.SetActive(true);
					entry.Value.interactable = true;
					if (!oneIsSelected) {
						entry.Value.Select();
						oneIsSelected = true;
					}
				}
			}
		}
		actionType = action;
	}

	public void ExitTargetSelection(Combatant target) {
		
		Debug.Log("Need to resolve the selection");
		if (actionType == ACTION.Attacking) ResolveAttack(target);

		foreach (KeyValuePair<Combatant, Button> entry in combatantButtons) {
			if (entry.Key != currentPlayer) entry.Value.gameObject.SetActive(false);
		}

		fightMenuFrame.ActivateFightMenu(true);
		fightMenuFrame.ActivateTargetPanel(false);

		switch (actionType) {
		case ACTION.Attacking:
			FindObjectOfType<AttackOptions>().GetComponentsInChildren<Button>()[0].Select();
			break;
		case ACTION.Buffing:
			FindObjectOfType<BuffSpellOptions>().GetComponentsInChildren<Button>()[0].Select();
			break;
		case ACTION.Casting:
			FindObjectOfType<AttackSpellOptions>().GetComponentsInChildren<Button>()[0].Select();
			break;
		default:
			break;
		}

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
