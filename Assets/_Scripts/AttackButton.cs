using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class AttackButton : MonoBehaviour, ISelectHandler {


	public void OnSelect(BaseEventData eventData) {
		if(GameObject.Find("AttackOptions")){
			GameObject.Find("AttackOptions").SetActive(false);
		}
		if(GameObject.Find("SpellAttackOptions")){
			GameObject.Find("SpellAttackOptions").SetActive(false);
		}
		if(GameObject.Find("SpellBuffOptions")){
			GameObject.Find("SpellBuffOptions").SetActive(false);
		}
		if(GetComponent<FightMenuButton>().subMenuInactive){
			GetComponent<Button>().image.overrideSprite = GetComponent<FightMenuButton>().subMenuInactive;
		}
	}
}
