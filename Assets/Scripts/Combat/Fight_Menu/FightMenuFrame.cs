using UnityEngine;

public class FightMenuFrame : MonoBehaviour
{
    private GameObject fightMenu, selectTargetPanel, waitPanel;

    void Awake()
    {
        fightMenu = GameObject.Find("FightMenu");
        ActivateFightMenu(true);
        selectTargetPanel = GameObject.Find("SelectTargetPanel");
        ActivateSelectTargetPanel(false);
        waitPanel = GameObject.Find("WaitPanel");
        ActivateWaitPanel(false);
    }

    public void ActivateFightMenu(bool value)
    {
        if (value)
        {
            fightMenu.SetActive(true);
        }
        else
        {
            fightMenu.SetActive(false);
        }
    }

    public void ActivateSelectTargetPanel(bool value)
    {
        if (value)
        {
            selectTargetPanel.SetActive(true);
        }
        else
        {
            selectTargetPanel.SetActive(false);
        }
    }

    public void ActivateWaitPanel(bool value)
    {
        if (value)
        {
            waitPanel.SetActive(true);
        }
        else
        {
            waitPanel.SetActive(false);
        }
    }
}
