using UnityEngine.UI;

public class ActionsButton : FightMenuButton
{
    private Text buttonText;
    private Toggle toggle;

    void Awake()
    {
        buttonText = GetComponentInChildren<Text>();
        toggle = GetComponent<Toggle>();
    }

    public void UpdateText(int actions)
    {
        buttonText.text = "Actions (" + actions.ToString() + ")";
        if (actions > 0) toggle.interactable = true;
        else toggle.interactable = false;
    }
}
