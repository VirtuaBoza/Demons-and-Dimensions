using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EquipPanel : MonoBehaviour {

	public Text nameFrameText;
	public GameObject crystalPanel, teddyPanel, hunterPanel, damienPanel;

	public void ShowEquipPanel(int index){
		if (index == 0){
			crystalPanel.transform.SetAsLastSibling();
			nameFrameText.text = "Crystal";
		} else if (index == 1) {
			teddyPanel.transform.SetAsLastSibling();
			nameFrameText.text = "Teddy";
		} else if (index == 2) {
			hunterPanel.transform.SetAsLastSibling();
			nameFrameText.text = "Hunter";
		} else if (index == 3) {
			damienPanel.transform.SetAsLastSibling();
			nameFrameText.text = "Damien";
		} else {
			Debug.LogWarning("Invalid equip panel index.");
		}
	}
}
