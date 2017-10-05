using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FightMenuFrame : MonoBehaviour {

	private GameObject fightMenu,targetPanel,waitPanel;

	// Use this for initialization
	void Awake () {
		fightMenu = GetComponentInChildren<FightMenu>().gameObject;
		ActivateFightMenu(true);
		targetPanel = GetComponentInChildren<TargetPanel>().gameObject;
		ActivateTargetPanel(false);
		waitPanel = GetComponentInChildren<WaitPanel>().gameObject;
		ActivateWaitPanel(false);
	}

	public void ActivateFightMenu(bool value) {
		if(value){
			fightMenu.SetActive(true);
		} else {
			fightMenu.SetActive(false);
		}
	}

	public void ActivateTargetPanel(bool value) {
		if(value){
			targetPanel.SetActive(true);
		} else {
			targetPanel.SetActive(false);
		}
	}

	public void ActivateWaitPanel(bool value) {
		if(value){
			waitPanel.SetActive(true);
		} else {
			waitPanel.SetActive(false);
		}
	}

}
