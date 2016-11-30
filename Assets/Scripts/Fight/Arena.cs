using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Arena : MonoBehaviour {

	public GameObject targetSelectButton,crystalPrefab,damienPrefab,hunterPrefab,teddyPrefab;
	public int[] numberOfEnemies;
	public GameObject[] enemyPrefabs;
	public Sprite[] arenas = new Sprite[2];
	public int mapIndex = 0;
	public bool isCrystalPlaying, isDamienPlaying, isHunterPlaying, isTeddyPlaying;
	public FightMenu fightMenu;
	public FightMenuFrame fightMenuFrame;
	public FriendlyLayoutGroup friendlyLayoutGroup;
	public EnemyLayoutGroup enemyLayoutGroup;

	private bool spellMode = false;
	private bool buffMode = false;
	private List<Vector3> positionList = new List<Vector3>();

	void Start () {
		
		InstantiateEnemies();

		InstantiateFriendlies ();

		Image myImage = GetComponent<Image>();
		myImage.overrideSprite = arenas[mapIndex];
	}

	private void InstantiateFriendlies() {

		if (isCrystalPlaying) {
			Vector3 position = FindOpenPosition (true);
			GameObject character = Instantiate(crystalPrefab, position, Quaternion.identity) as GameObject;
			character.transform.SetParent(transform,false);
			character.GetComponent<SpriteRenderer> ().sortingOrder = 10 - ((int)position.y);
			Friendly friendly = character.GetComponent<Friendly> ();
			friendlyLayoutGroup.PopulateInfoBox (friendly.myName, friendly.hp, friendly.maxHp);
			isCrystalPlaying = false;
			InstantiateFriendlies();
		}

		if (isDamienPlaying) {
			Vector3 position = FindOpenPosition (true);
			GameObject character = Instantiate(damienPrefab, position, Quaternion.identity) as GameObject;
			character.transform.SetParent(transform,false);
			character.GetComponent<SpriteRenderer> ().sortingOrder = 10 - ((int)position.y);
			Friendly friendly = character.GetComponent<Friendly> ();
			friendlyLayoutGroup.PopulateInfoBox (friendly.myName, friendly.hp, friendly.maxHp);
			isDamienPlaying = false;
			InstantiateFriendlies();
		}

		if (isHunterPlaying) {
			Vector3 position = FindOpenPosition (true);
			GameObject character = Instantiate(hunterPrefab, position, Quaternion.identity) as GameObject;
			character.transform.SetParent(transform,false);
			character.GetComponent<SpriteRenderer> ().sortingOrder = 10 - ((int)position.y);
			Friendly friendly = character.GetComponent<Friendly> ();
			friendlyLayoutGroup.PopulateInfoBox (friendly.myName, friendly.hp, friendly.maxHp);
			isHunterPlaying = false;
			InstantiateFriendlies();
		}

		if (isTeddyPlaying) {
			Vector3 position = FindOpenPosition (true);
			GameObject character = Instantiate(teddyPrefab, position, Quaternion.identity) as GameObject;
			character.transform.SetParent(transform,false);
			character.GetComponent<SpriteRenderer> ().sortingOrder = 10 - ((int)position.y);
			Friendly friendly = character.GetComponent<Friendly> ();
			friendlyLayoutGroup.PopulateInfoBox (friendly.myName, friendly.hp, friendly.maxHp);
			isTeddyPlaying = false;
			InstantiateFriendlies();
		}
	}

	private void InstantiateEnemies() {
		
		for (int index = 0; index < numberOfEnemies.Length; index++) {
			for (int number = 1; number <= numberOfEnemies[index]; number++) {
				Vector3 position = FindOpenPosition (false);
				GameObject character = Instantiate(enemyPrefabs[index], position, Quaternion.identity) as GameObject;
				character.transform.SetParent(transform,false);
				character.GetComponent<SpriteRenderer> ().sortingOrder = 10 - ((int)position.y);
				Enemy enemy = character.GetComponent<Enemy>();
				enemyLayoutGroup.PopulateInfoBox(enemy.myName, enemy.hp, enemy.maxHp);
			}
		}
	}

	private Vector3 FindOpenPosition (bool isFriendly) {
		if (isFriendly) {
			Vector3 position = new Vector3(Random.Range(1, 5), Random.Range(1, 9), transform.position.z);
			if (positionList.Contains(position)){
				FindOpenPosition(true);
			} else {
				positionList.Add(position);
				return position;
			}
		} else {
			Vector3 position = new Vector3(Random.Range(5, 9), Random.Range(1, 9), transform.position.z);
			if (positionList.Contains(position)){
				FindOpenPosition(false);
			} else {
				positionList.Add(position);
				return position;
			}
		}
		return Vector3.zero;
	}

	public void EnterTargetSelection (string aOrBOrC){
		if(aOrBOrC == "A" || aOrBOrC == "C"){
			Enemy[] enemies = GetComponentsInChildren<Enemy>();
			foreach (Enemy enemy in enemies) {
				GameObject button = Instantiate(targetSelectButton, enemy.transform.position, Quaternion.identity) as GameObject;
				button.transform.SetParent(enemy.transform);
			}
		} else if (aOrBOrC == "B") {
			Transform[] characters = GetComponentsInChildren<Transform>();
			foreach (Transform character in characters) {
				if (character.tag == "Friendly") {
					GameObject button = Instantiate(targetSelectButton, character.position, Quaternion.identity) as GameObject;
					button.transform.SetParent(character);
				}
			}
			buffMode = true;
		} 

		if (aOrBOrC == "C") {
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

		fightMenu.ActivateMenu(true);
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
