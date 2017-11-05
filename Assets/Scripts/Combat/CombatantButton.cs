using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CombatantButton : MonoBehaviour, ISelectHandler, IDeselectHandler
{

    private Button button;
    private FightManager fightManager;

    void Start()
    {
        button = GetComponent<Button>();
        fightManager = FindObjectOfType<FightManager>();
        button.onClick.AddListener(() => OnMouseDown());
    }

    public void OnMouseEnter()
    {
        if (button.IsInteractable())
        {
            button.Select();
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        foreach (Text text in GetComponentInParent<Combatant>().infoBox.GetComponentsInChildren<Text>()) text.color = Color.red;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        foreach (Text text in GetComponentInParent<Combatant>().infoBox.GetComponentsInChildren<Text>()) text.color = Color.black;
    }

    public void OnMouseDown()
    {
        if (button.IsInteractable())
        {
            fightManager.ExitTargetSelection(GetComponentInParent<Combatant>());
            foreach (Text text in GetComponentInParent<Combatant>().infoBox.GetComponentsInChildren<Text>()) text.color = Color.black;
        }
    }

    void Destroy()
    {
        button.onClick.RemoveAllListeners();
    }
}
