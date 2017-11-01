using UnityEngine;

public class FightMenuFrame : MonoBehaviour
{
    private GameObject fightMenu, targetPanel, waitPanel;

    void Awake()
    {
        fightMenu = GameObject.Find("FightMenu");
        ActivateFightMenu(true);
        targetPanel = GameObject.Find("TargetPanel");
        ActivateTargetPanel(false);
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

    public void ActivateTargetPanel(bool value)
    {
        if (value)
        {
            targetPanel.SetActive(true);
        }
        else
        {
            targetPanel.SetActive(false);
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
