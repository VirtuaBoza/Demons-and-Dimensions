using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class FightGround : MonoBehaviour {

	public GameObject targetSelectButton,prefab,crystalPrefab,damienPrefab,hunterPrefab,teddyPrefab;
	[Range(1,50)]public int numberOfEnemies = 1;
	public Sprite[] arenas = new Sprite[2];
	public int mapIndex = 0;
	public bool isCrystalPlaying, isDamienPlaying, isHunterPlaying, isTeddyPlaying;

	private bool spellMode = false;
	private bool buffMode = false;
	private List<Vector3> positionList = new List<Vector3>();

	private FightMenu fightMenu;
	private FightMenuFrame fightMenuFrame;

	void Start () {
		fightMenu = FindObjectOfType<FightMenu>();
		fightMenuFrame = FindObjectOfType<FightMenuFrame>();

		for (int i = 1; i <= numberOfEnemies; i++){
			InstantiateEnemy();
		}

		InstantiateFriendlies ();

		Image myImage = GetComponent<Image>();
		myImage.overrideSprite = arenas[mapIndex];
	}

	private void InstantiateFriendlies() {
		if (isCrystalPlaying) {
			
		}

		if (isDamienPlaying) {
			
		}

		if (isHunterPlaying) {
			
		}

		if (isTeddyPlaying) {
			
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
		character.GetComponent<SpriteRenderer> ().sortingOrder = 9 - ((int)position.y);
		GameObject button = Instantiate(targetSelectButton, Vector3.zero, Quaternion.identity) as GameObject;
		button.transform.SetParent(character.transform, false);
	}

	public void EnterTargetSelection (string AorSorB){
		if(AorSorB == "A"){
			//something
		} else if (AorSorB == "S") {
			spellMode = true;
		} else if (AorSorB == "B") {
			buffMode = true;
		}
		Button[] buttons = GetComponentsInChildren<Button>();
		foreach(Button button in buttons){
			button.interactable = true;
		}
		buttons[0].Select();
	}
		
	public void ExitTargetSelection () {
		Button[] buttons = GetComponentsInChildren<Button>();
		foreach(Button button in buttons){
			button.interactable = false;
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
