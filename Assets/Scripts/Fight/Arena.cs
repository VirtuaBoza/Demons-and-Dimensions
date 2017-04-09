using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public enum ACTION {Attacking,Buffing,Casting}

public class Arena : MonoBehaviour {

	public GameObject targetSelectButton,crystalPrefab,damienPrefab,hunterPrefab,teddyPrefab;
	[Range(1,16)]public int[] numberOfEnemiesByType;
	public GameObject[] enemyPrefabs;
	public Sprite[] arenas = new Sprite[2];
	public int mapIndex = 0;
	public bool isCrystalPlaying, isDamienPlaying, isHunterPlaying, isTeddyPlaying;
	public FightMenuFrame fightMenuFrame;
	public FriendlyLayoutGroup friendlyLayoutGroup;
	public EnemyLayoutGroup enemyLayoutGroup;
	public List<Combatant> combatants = new List<Combatant>();

	private bool spellMode = false;
	private bool buffMode = false;
	private List<Vector3> positionList = new List<Vector3>();
	private List<int> initiativeList = new List<int>();

	void Start () {

		Image myImage = GetComponent<Image>();
		myImage.overrideSprite = arenas[mapIndex];

		InstantiateEnemies();

		InstantiateFriendlies ();

		FindObjectOfType<FightManager>().StartFight(combatants);
	}

	private void InstantiateEnemies() {
		for (int index = 0; index < numberOfEnemiesByType.Length; index++) {
			for (int number = 1; number <= numberOfEnemiesByType[index]; number++) {
				PlaceCharacter(false,enemyPrefabs[index]);
			}
		}
	}

	private void InstantiateFriendlies() {

		if (isCrystalPlaying) {
			PlaceCharacter(true,crystalPrefab);
		}

		if (isDamienPlaying) {
			PlaceCharacter(true,damienPrefab);
		}

		if (isHunterPlaying) {
			PlaceCharacter(true,hunterPrefab);
		}

		if (isTeddyPlaying) {
			PlaceCharacter(true,teddyPrefab);
		}

		List<Combatant> sortedList = combatants.OrderByDescending(c => c.initiative).ToList();
		combatants = sortedList;
		foreach (Combatant combatant in combatants) {
			if (combatant.isFriendly) {combatant.infoBox = friendlyLayoutGroup.PopulateInfoBox(combatant.myName,combatant.hp,combatant.maxHp);}
			else {combatant.infoBox = enemyLayoutGroup.PopulateInfoBox(combatant.myName,combatant.hp,combatant.maxHp);}
		}
	}

	private void PlaceCharacter(bool isFriendly, GameObject prefab) {
		Vector3 position = FindOpenPosition (isFriendly);
		GameObject character = Instantiate(prefab, position, Quaternion.identity) as GameObject;
		character.transform.SetParent(transform,false);
		character.GetComponent<SpriteRenderer>().sortingOrder = 10 - ((int)position.y);
		Combatant combatant = character.GetComponent<Combatant>();
		combatant.initiative = RollForInitiative(combatant);
		combatants.Add(combatant);
	}

	private Vector3 FindOpenPosition(bool isFriendly) {
		if (isFriendly) {
			Vector3 position = new Vector3(Random.Range(1, 5), Random.Range(1, 9), transform.position.z);
			if (positionList.Contains(position)) return FindOpenPosition(true);
			else {
				positionList.Add(position);
				return position;
			}
		} else {
			Vector3 position = new Vector3(Random.Range(5, 9), Random.Range(1, 9), transform.position.z);
			if (positionList.Contains(position)) return FindOpenPosition(false);
			else {
				positionList.Add(position);
				return position;
			}
		}
	}

	private int RollForInitiative(Combatant combatant) {
		int roll = Random.Range(1,21);
		if (initiativeList.Contains(roll)) return RollForInitiative(combatant);
		else {
			initiativeList.Add(roll);
			return roll;
		}
	}

	public void EnterTargetSelection (ACTION action){

		if(action == ACTION.Attacking || action == ACTION.Casting){
			foreach (Combatant combatant in combatants) {
				if (!combatant.isFriendly) Instantiate(targetSelectButton, combatant.transform, false);
			}
		} else if (action == ACTION.Buffing) {
			foreach (Combatant combatant in combatants) {
				if (combatant.isFriendly) Instantiate(targetSelectButton, combatant.transform, false);
			}
			buffMode = true;
		}

		if (action == ACTION.Casting) {
			spellMode = true;
		}

		Button[] buttons = GetComponentsInChildren<Button>();
		buttons[0].Select();

	}
		
	public void ExitTargetSelection () {
		Button[] buttons = GetComponentsInChildren<Button>();
		foreach(Button button in buttons){
			Destroy(button.gameObject);
		}

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

}
