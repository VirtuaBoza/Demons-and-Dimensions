using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActionOptions : MonoBehaviour {

	public void OnEnable () {
		GetComponentsInChildren<Toggle>()[0].Select();
	}

	public void SetAllTogglesOff () {
		GetComponent<ToggleGroup>().SetAllTogglesOff();
	}

}
