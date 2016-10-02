using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class TargetSelectButton : MonoBehaviour, IPointerEnterHandler, ISelectHandler {

	private Button button;
	private GameObject eventSystem;

	// Use this for initialization
	void Start () {
		button = GetComponent<Button>();

		button.onClick.AddListener(() => MyOnClick());

		eventSystem = GameObject.Find ("EventSystem");
	}

	public void OnPointerEnter(PointerEventData eventData) {
		OnSelect(eventData);
	}

	public void OnSelect(BaseEventData eventData) {

		if (eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject != gameObject){
			eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem> ().SetSelectedGameObject (gameObject);

		}

	}

	void MyOnClick() {
		FightGround fightGround = GetComponentInParent<FightGround>();
		fightGround.ExitTargetSelection();
		GameObject.Find("SelectTargetPanel").SetActive(false);
		GameObject.FindObjectOfType<FightMenu>().EnableFightMenu();
	}

	void Destroy () {
		button.onClick.RemoveAllListeners();
	}
}
