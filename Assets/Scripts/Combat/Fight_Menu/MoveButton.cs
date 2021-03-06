﻿using UnityEngine.UI;

public class MoveButton : FightMenuButton
{
    private Text buttonText;

    void Awake()
    {
        buttonText = GetComponentInChildren<Text>();
    }

    public void UpdateText(int moves)
    {
        buttonText.text = "Move (" + moves.ToString() + ")";
        if (moves > 0) GetComponent<Button>().interactable = true;
        else GetComponent<Button>().interactable = false;
    }
}
