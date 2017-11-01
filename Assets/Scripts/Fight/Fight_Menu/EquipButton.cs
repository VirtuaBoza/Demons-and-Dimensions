using UnityEngine.UI;

public class EquipButton : FightMenuButton
{
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => OnMyClick());
    }

    void OnMyClick()
    {
        FindObjectOfType<FightManager>().EnterEquip();
    }
}
