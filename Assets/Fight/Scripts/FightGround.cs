using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class FightGround : MonoBehaviour {

	public GameObject targetSelectButton,prefab;
	[Range(1,50)]public int numberOfEnemies = 1;
	public Sprite[] arenas = new Sprite[2];
	public int mapIndex = 0;

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

		Image myImage = GetComponent<Image>();
		myImage.overrideSprite = arenas[mapIndex];
	}

	private void InstantiateEnemy() {
		Vector3 position = new Vector3(Random.Range(0, 5) + 0.5f, Random.Range(-5, 5) + 0.5f + transform.position.y, 0);
		if (positionList.Contains(position)){
			InstantiateEnemy();
		} else {
			positionList.Add(position);
		}
		GameObject character = Instantiate(prefab, position, Quaternion.identity) as GameObject;
		character.transform.SetParent(transform,true);
		GameObject button = Instantiate(targetSelectButton, position, Quaternion.identity) as GameObject;
		button.transform.SetParent(transform,true);
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
