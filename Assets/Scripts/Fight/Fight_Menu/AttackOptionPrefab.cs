using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AttackOptionPrefab : MonoBehaviour, IPointerEnterHandler
{
    public Text weaponText; //Assigned in inspector.... why?
    public string title;
    public DieType damageRange;
    public int damageMulti;
    public DamageType damageType;
    public bool finesse;
    public bool heavy;
    public bool isLight;
    public bool reach;
    public bool twoHanded;
    public int range;
    public int maxRange;

    public void UpdateFields()
    {
        GetComponent<Button>().onClick.AddListener(() => MyOnClick());
        if (TargetsInRange())
        {
            weaponText.text = title + "  AtkBns:+" + GetProfBonus() + "  Dmg:" + damageMulti.ToString() + GetDieTypeInString(damageRange) + " " + GetDamageTypeInString(damageType);
            GetComponent<Button>().interactable = true; // This is probably not necessary
        }
        else
        {
            weaponText.text = title + "  No targets in range";
            GetComponent<Button>().interactable = false;
        }
    }

    string GetDieTypeInString(DieType die)
    {
        switch (die)
        {
            case DieType.d00:
                return "d00";
            case DieType.d10:
                return "d10";
            case DieType.d12:
                return "d12";
            case DieType.d20:
                return "d20";
            case DieType.d4:
                return "d4";
            case DieType.d6:
                return "d6";
            case DieType.d8:
                return "d8";
            case DieType.one:
                return "";
            default:
                return "AttackOptionPrefab doesn't recognize Die type";
        }
    }

    string GetDamageTypeInString(DamageType damType)
    {
        switch (damType)
        {
            case DamageType.Acid:
                return "Acid";
            case DamageType.Bludgeoning:
                return "Bludgeoning";
            case DamageType.Cold:
                return "Cold";
            case DamageType.Fire:
                return "Fire";
            case DamageType.Force:
                return "Force";
            case DamageType.Lightning:
                return "Lightning";
            case DamageType.Necrotic:
                return "Necrotic";
            case DamageType.Piercing:
                return "Piercing";
            case DamageType.Poison:
                return "Poison";
            case DamageType.Psychic:
                return "Psychic";
            case DamageType.Radiant:
                return "Radiant";
            case DamageType.Slashing:
                return "Slashing";
            case DamageType.Thunder:
                return "Thunder";
            default:
                return "AttackOptionPrefab doesn't recognize DamageType";
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
        foreach (PlayerCharacter playerCharacter in characterDatabase.CharacterDictionary.Keys)
        {
            if (playerCharacter == FindObjectOfType<FightManager>().currentCombatant.character)
            {
                int bonus = characterDatabase.CharacterDictionary[playerCharacter].GetProfBonus();
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
