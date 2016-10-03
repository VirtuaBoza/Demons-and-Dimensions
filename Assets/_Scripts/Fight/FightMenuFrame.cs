using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FightMenuFrame : MonoBehaviour {

	private GameObject targetPanel;

	// Use this for initialization
	void Start () {
		targetPanel = GetComponentInChildren<TargetPanel>().gameObject;
		ActivateTargetPanel(false);
	}

	public void ActivateTargetPanel(bool value) {
		if(value){
			targetPanel.SetActive(true);
		} else {
			targetPanel.SetActive(false);
		}
	}

}
