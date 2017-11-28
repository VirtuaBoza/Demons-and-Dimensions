using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class AttackOptionPrefab : MonoBehaviour, IPointerEnterHandler
{
    public Text weaponText; //Assigned in inspector.... why?
    public string title;
    public int damageMulti;
    public DieType damageRange;
    public DamageType damageType;
    public bool reach;
    public int maxRange;

    public void UpdateFields()
    {
        GetComponent<Button>().onClick.AddListener(() => MyOnClick());
        if (TargetsInRange())
        {
            weaponText.text = string.Format("{0} AtkBns:+{1} Dmg:{2}{3} {4}", 
                title, GetProfBonus(), damageMulti.ToString(), damageRange.ToString(), damageType.ToString());
            GetComponent<Button>().interactable = true; // This is probably not necessary
        }
        else
        {
            weaponText.text = title + "  No targets in range";
            GetComponent<Button>().interactable = false;
        }
    }

    bool TargetsInRange()
    {

        List<Vector3> enemyPositions = new List<Vector3>();
        FightManager fightManager = FindObjectOfType<FightManager>();
        foreach (Combatant combatant in fightManager.currentCombatants)
        {
            if (!combatant.isFriendly) enemyPositions.Add(combatant.transform.localPosition);
        }
        int trueRange = GetTrueRange();
        for (int x = -trueRange; x <= trueRange; x++)
        {
            for (int y = -trueRange; y <= trueRange; y++)
            {
                if (enemyPositions.Contains(new Vector3(fightManager.currentCombatant.transform.localPosition.x + x, fightManager.currentCombatant.transform.localPosition.y + y)))
                {
                    return true;
                }
            }
        }
        return false;
    }

    int GetTrueRange()
    {
        int trueRange = maxRange;
        if (reach) trueRange++;
        return trueRange;
    }

    int GetProfBonus()
    {
        CharacterDatabase characterDatabase = FindObjectOfType<CharacterDatabase>();
        foreach (PlayerCharacterName playerCharacter in characterDatabase.CharacterDictionary.Keys)
        {
            if (playerCharacter == FindObjectOfType<FightManager>().currentCombatant.character)
            {
                int bonus = characterDatabase.CharacterDictionary[playerCharacter].ProfBonus;
                if (maxRange > 1) bonus += characterDatabase.CharacterDictionary[playerCharacter].GetAbilityScoreModifier(AbilityType.Dex);
                else bonus += characterDatabase.CharacterDictionary[playerCharacter].GetAbilityScoreModifier(AbilityType.Str);
                return bonus;
            }
        }
        Debug.LogWarning("AttackOptionsPrefab doesn't recognize player to get ProfBonus");
        return 0;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GetComponent<Button>() && GetComponent<Button>().interactable)
        {
            GetComponent<Button>().Select();
            GameObject.Find("AttackButton").GetComponent<Animator>().SetTrigger("Normal");
        }
    }

    void MyOnClick()
    {
        FindObjectOfType<FightManager>().InitiateAttack(GetProfBonus(), damageRange, damageMulti, damageType, GetTrueRange());
    }

    void Destroy()
    {
        GetComponent<Button>().onClick.RemoveAllListeners();
    }
}
