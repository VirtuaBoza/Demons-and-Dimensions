using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActionOptions : MonoBehaviour {

	public void SetAllTogglesOff () {
		GetComponent<ToggleGroup>().SetAllTogglesOff();
	}

}
