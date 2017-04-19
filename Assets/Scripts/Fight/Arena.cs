using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public enum ACTION {Attacking,Buffing,Casting}

public class Arena : MonoBehaviour {

	public GameObject crystalPrefab,damienPrefab,hunterPrefab,teddyPrefab;
	[Range(1,16)]public int[] numberOfEnemiesByType;
	public GameObject[] enemyPrefabs;
	public Sprite[] arenas = new Sprite[2];
	public int mapIndex = 0;
	public bool isCrystalPlaying, isDamienPlaying, isHunterPlaying, isTeddyPlaying;
	public FightMenuFrame fightMenuFrame; //Assigned in inspector
	public FriendlyLayoutGroup friendlyLayoutGroup; //Assigned in inspector
	public EnemyLayoutGroup enemyLayoutGroup; //Assigned in inspector
	public FightManager fightManager; //Assigned in inspector
	public List<Combatant> combatants = new List<Combatant>();

	private List<Vector3> positionList = new List<Vector3>(); //To keep track of positions so that no two combatants have the same position
	private List<int> initiativeList = new List<int>(); //To keep track of initiative rolls so that no two combatants have the same initiative

	void Start () {
		GetComponent<SpriteRenderer>().sprite = arenas[mapIndex];
		InstantiateEnemies();
		InstantiateFriendlies ();
		FindObjectOfType<FightManager>().StartFight(combatants);
		FindObjectOfType<StartOptions>().inMainMenu = false;
		FindObjectOfType<ShowPanels>().inFight = true;
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
			if (combatant.isFriendly) {combatant.infoBox = friendlyLayoutGroup.PopulateInfoBox(combatant.myName,combatant.currentHp,combatant.maxHp);}
			else {combatant.infoBox = enemyLayoutGroup.PopulateInfoBox(combatant.myName,combatant.currentHp,combatant.maxHp);}
		}
	}

	private void PlaceCharacter(bool isFriendly, GameObject prefab) {
		Vector3 position = FindOpenPosition (isFriendly);
		GameObject character = Instantiate(prefab, position, Quaternion.identity) as GameObject;
		character.transform.SetParent(transform,false);
		character.GetComponent<SpriteRenderer>().sortingOrder = 10 - ((int)position.y);
		Combatant combatant = character.GetComponent<Combatant>();
		combatant.initiative = RollForInitiative();
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

	private int RollForInitiative() {
		int roll = fightManager.RollADie(DIE.d20);
		if (initiativeList.Contains(roll)) return RollForInitiative();
		else {
			initiativeList.Add(roll);
			return roll;
		}
	}

}
