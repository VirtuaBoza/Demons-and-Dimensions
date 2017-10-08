using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AttackOptionPrefab : MonoBehaviour, IPointerEnterHandler
{

    public Text weaponText; //Assigned in inspector

    public string title;
    public DIE damageRange;
    public int damageMulti;
    public DAMAGETYPE damageType;
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

    string GetDieTypeInString(DIE die)
    {
        switch (die)
        {
            case DIE.d00:
                return "d00";
            case DIE.d10:
                return "d10";
            case DIE.d12:
                return "d12";
            case DIE.d20:
                return "d20";
            case DIE.d4:
                return "d4";
            case DIE.d6:
                return "d6";
            case DIE.d8:
                return "d8";
            case DIE.one:
                return "";
            default:
                return "AttackOptionPrefab doesn't recognize Die type";
        }
    }

    string GetDamageTypeInString(DAMAGETYPE damType)
    {
        switch (damType)
        {
            case DAMAGETYPE.Acid:
                return "Acid";
            case DAMAGETYPE.Bludgeoning:
                return "Bludgeoning";
            case DAMAGETYPE.Cold:
                return "Cold";
            case DAMAGETYPE.Fire:
                return "Fire";
            case DAMAGETYPE.Force:
                return "Force";
            case DAMAGETYPE.Lightning:
                return "Lightning";
            case DAMAGETYPE.Necrotic:
                return "Necrotic";
            case DAMAGETYPE.Piercing:
                return "Piercing";
            case DAMAGETYPE.Poison:
                return "Poison";
            case DAMAGETYPE.Psychic:
                return "Psychic";
            case DAMAGETYPE.Radiant:
                return "Radiant";
            case DAMAGETYPE.Slashing:
                return "Slashing";
            case DAMAGETYPE.Thunder:
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
        CharacterKeeper characterKeeper = FindObjectOfType<CharacterKeeper>();
        foreach (PLAYERCHARACTER character in characterKeeper.characters.Keys)
        {
            if (character == FindObjectOfType<FightManager>().currentCombatant.character)
            {
                int bonus = characterKeeper.characters[character].GetProfBonus();
                if (maxRange > 1) bonus += characterKeeper.characters[character].GetAbilityScoreModifier(ABILITY.Dex);
                else bonus += characterKeeper.characters[character].GetAbilityScoreModifier(ABILITY.Str);
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
            FindObjectOfType<AttackButton>().GetComponent<Animator>().SetTrigger("Normal");
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
