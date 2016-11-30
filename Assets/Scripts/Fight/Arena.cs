using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Arena : MonoBehaviour {

	public GameObject targetSelectButton,prefab,crystalPrefab,damienPrefab,hunterPrefab,teddyPrefab;
	[Range(1,50)]public int numberOfEnemies = 1;
	public Sprite[] arenas = new Sprite[2];
	public int mapIndex = 0;
	public bool isCrystalPlaying, isDamienPlaying, isHunterPlaying, isTeddyPlaying;
	public FightMenu fightMenu;
	public FightMenuFrame fightMenuFrame;

	private bool spellMode = false;
	private bool buffMode = false;
	private List<Vector3> positionList = new List<Vector3>();

	void Start () {

		for (int i = 1; i <= numberOfEnemies; i++){
			InstantiateEnemy();
		}

		InstantiateFriendlies ();

		Image myImage = GetComponent<Image>();
		myImage.overrideSprite = arenas[mapIndex];
	}

	private void InstantiateFriendlies() {

		Vector3 position = new Vector3(Random.Range(1, 5), Random.Range(1, 9), transform.position.z);
		if (positionList.Contains(position)){
			InstantiateFriendlies();
		} else {
			positionList.Add(position);
		}

		if (isCrystalPlaying) {
			GameObject character = Instantiate(crystalPrefab, position, Quaternion.identity) as GameObject;
			character.transform.SetParent(transform,false);
			character.GetComponent<SpriteRenderer> ().sortingOrder = 10 - ((int)position.y);
			isCrystalPlaying = false;
			InstantiateFriendlies();
		}

		if (isDamienPlaying) {
			GameObject character = Instantiate(damienPrefab, position, Quaternion.identity) as GameObject;
			character.transform.SetParent(transform,false);
			character.GetComponent<SpriteRenderer> ().sortingOrder = 10 - ((int)position.y);
			isDamienPlaying = false;
			InstantiateFriendlies();
		}

		if (isHunterPlaying) {
			GameObject character = Instantiate(hunterPrefab, position, Quaternion.identity) as GameObject;
			character.transform.SetParent(transform,false);
			character.GetComponent<SpriteRenderer> ().sortingOrder = 10 - ((int)position.y);
			isHunterPlaying = false;
			InstantiateFriendlies();
		}

		if (isTeddyPlaying) {
			GameObject character = Instantiate(teddyPrefab, position, Quaternion.identity) as GameObject;
			character.transform.SetParent(transform,false);
			character.GetComponent<SpriteRenderer> ().sortingOrder = 10 - ((int)position.y);
			isTeddyPlaying = false;
			InstantiateFriendlies();
		}
	}

	private void InstantiateEnemy() {
		Vector3 position = new Vector3(Random.Range(5, 9), Random.Range(1, 9), transform.position.z);
		if (positionList.Contains(position)){
			InstantiateEnemy();
		} else {
			positionList.Add(position);
		}
		GameObject character = Instantiate(prefab, position, Quaternion.identity) as GameObject;
		character.transform.SetParent(transform,false);
		character.GetComponent<SpriteRenderer> ().sortingOrder = 10 - ((int)position.y);
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
