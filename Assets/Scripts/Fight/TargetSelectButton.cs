using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class TargetSelectButton : MonoBehaviour, ISelectHandler, IPointerEnterHandler {

	private Button button;
	private Arena arena;

	// Use this for initialization
	void Start () {

		arena = GetComponentInParent<Arena>();

		button = GetComponent<Button>();

		button.onClick.AddListener(() => MyOnClick());

	}

	public void OnPointerEnter(PointerEventData eventData) {
		OnSelect(eventData);
	}

	public void OnSelect(BaseEventData eventData) {

		if (EventSystem.current.GetComponent<EventSystem>().currentSelectedGameObject != gameObject){
			EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject (gameObject);
		}

	}

	void MyOnClick() {
		arena.ExitTargetSelection();
	}

	void Destroy () {
		button.onClick.RemoveAllListeners();
	}
}
