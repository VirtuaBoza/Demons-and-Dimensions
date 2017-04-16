using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AttackOptions : MonoBehaviour {

	public FightManager fightManager; //Assigned in inspector
	public GameObject attackOptionPrefab; //Assigned in inspector

	public void UpdateOptions() {
		foreach (Transform child in transform) {
			GameObject.Destroy(child.gameObject);
		}
		Inventory inventory = FindObjectOfType<Inventory>();
		foreach (CHARACTER character in inventory.characterEquippedItems.Keys) {
			if (character == fightManager.currentPlayer.character) {
				foreach (Item item in inventory.characterEquippedItems[character]) {
					GameObject option = Instantiate(attackOptionPrefab,transform) as GameObject;
					option.GetComponent<Text>().text = item.Title;
				}
			}
		}
	}

}
