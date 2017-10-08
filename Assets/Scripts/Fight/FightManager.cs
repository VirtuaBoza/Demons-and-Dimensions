using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightManager : MonoBehaviour
{

    public GameObject moveTarget;

    public List<Vector3> currentPositionList = new List<Vector3>();
    public Combatant currentCombatant;
    public List<Combatant> currentCombatants = new List<Combatant>();

    private FightMenuFrame fightMenuFrame;
    private Dictionary<Combatant, Button> combatantButtons = new Dictionary<Combatant, Button>();

    private ACTION actionType;

    private int profBonus;
    private DIE damageRange;
    private int damageMulti;

    // TODO implement damageType
    //private DAMAGETYPE damageType;

    private Dictionary<int, int> currentPlayerItemIDsAndQuantities = new Dictionary<int, int>();

    void Awake()
    {
        fightMenuFrame = FindObjectOfType<FightMenuFrame>();
    }

    public void StartFight(List<Combatant> combatants)
    {
        foreach (Combatant combatant in combatants)
        {
            combatantButtons.Add(combatant, combatant.GetComponentInChildren<Button>());
            combatant.GetComponentInChildren<Button>().gameObject.SetActive(false);
            currentPositionList.Add(combatant.transform.localPosition);
        }
        currentCombatants = combatants;
        RunGame();
    }

    public void RunGame()
    {
        currentCombatants[0].StartTurn();
        currentCombatant = currentCombatants[0];
        if (currentCombatant.character == PLAYERCHARACTER.Crystal || currentCombatant.character == PLAYERCHARACTER.Damien || currentCombatant.character == PLAYERCHARACTER.Hunter || currentCombatant.character == PLAYERCHARACTER.Teddy)
        {
            FindObjectOfType<GameManager>().currentCharacter = currentCombatant.character;
            Debug.Log("Set GameManager's currentCharacter to " + currentCombatant.character);
        }
        
        if (currentCombatants[0].isFriendly)
        {
            fightMenuFrame.ActivateWaitPanel(false);
            fightMenuFrame.ActivateFightMenu(true);
            ActionsButton actionsButton = FindObjectOfType<ActionsButton>();
            actionsButton.UpdateText(currentCombatants[0].remainingActions);
            actionsButton.GetComponent<Selectable>().Select();
            actionsButton.GetComponent<Animator>().SetTrigger("Highlighted");
            MoveButton moveButton = FindObjectOfType<MoveButton>();
            moveButton.UpdateText(currentCombatants[0].remainingMoves);
        }
        else
        {
            fightMenuFrame.ActivateWaitPanel(true);
            fightMenuFrame.ActivateFightMenu(false);
        }
    }

    public void EndTurn()
    {
        Combatant thisOne = currentCombatants[0];
        thisOne.EndTurn();
        currentCombatants.RemoveAt(0);
        currentCombatants.Add(thisOne);
        RunGame();
    }


    public void InitiateAttack(int prof, DIE dRange, int multi, DAMAGETYPE dType, int range)
    {
        profBonus = prof;
        damageRange = dRange;
        damageMulti = multi;
        // TODO implement damageType
        //damageType = dType;
        EnterTargetSelection(ACTION.Attacking, range);
        actionType = ACTION.Attacking;
    }

    void ResolveAttack(Combatant target)
    {
        int attackRoll = Die.RollADie(DIE.d20);
        attackRoll += profBonus;
        int totalDamage = 0;
        
        if (attackRoll == 20)
        {
            for (int i = 0; i < damageMulti; i++)
            {
                int roll1 = Die.RollADie(damageRange);
                int roll2 = Die.RollADie(damageRange);
                totalDamage += Mathf.Max(roll1, roll2);
            }
        }
        else if (attackRoll != 1 && attackRoll >= target.aC)
        {
            for (int i = 0; i < damageMulti; i++)
            {
                totalDamage += Die.RollADie(damageRange);
            }
        }

        target.TakeDamage(totalDamage);
    }

    public void EnterTargetSelection(ACTION action, int range)
    {
        fightMenuFrame.ActivateFightMenu(false);
        fightMenuFrame.ActivateTargetPanel(true);
        List<Vector3> combatantsInRange = new List<Vector3>();
        for (int x = -range; x <= range; x++)
        {
            for (int y = -range; y <= range; y++)
            {
                Vector3 spot = new Vector3(currentCombatant.transform.localPosition.x + x, currentCombatant.transform.localPosition.y + y);
                if (currentPositionList.Contains(spot) && spot != currentCombatant.transform.localPosition)
                {
                    combatantsInRange.Add(spot);
                }
            }
        }
        bool oneIsSelected = false;
        foreach (KeyValuePair<Combatant, Button> entry in combatantButtons)
        {
            if (combatantsInRange.Contains(entry.Key.transform.localPosition))
            {
                if ((action == ACTION.Attacking || action == ACTION.Casting) && !entry.Key.isFriendly)
                {
                    entry.Value.gameObject.SetActive(true);
                    entry.Value.interactable = true;
                    if (!oneIsSelected)
                    {
                        entry.Value.Select();
                        oneIsSelected = true;
                    }
                }
                else if (action == ACTION.Buffing && entry.Key.isFriendly)
                {
                    entry.Value.gameObject.SetActive(true);
                    entry.Value.interactable = true;
                    if (!oneIsSelected)
                    {
                        entry.Value.Select();
                        oneIsSelected = true;
                    }
                }
            }
        }
        actionType = action;
    }

    public void ExitTargetSelection(Combatant target)
    {

        if (actionType == ACTION.Attacking) ResolveAttack(target);

        foreach (KeyValuePair<Combatant, Button> entry in combatantButtons)
        {
            if (entry.Key != currentCombatant) entry.Value.gameObject.SetActive(false);
        }

        fightMenuFrame.ActivateFightMenu(true);
        fightMenuFrame.ActivateTargetPanel(false);

        currentCombatant.remainingActions -= 1;
        FindObjectOfType<ActionsButton>().UpdateText(currentCombatant.remainingActions);

        SelectAppropriateOption();

    }

    public void EnterMoveSelection()
    {

        fightMenuFrame.ActivateFightMenu(false);
        fightMenuFrame.ActivateTargetPanel(true);

        bool oneIsSelected = false;
        foreach (Combatant combatant in currentCombatants)
        {
            if (combatant.isTurn)
            {
                for (int x = 1; x <= 8; x++)
                {
                    for (int y = 1; y <= 8; y++)
                    {
                        Vector3 possibleOption = new Vector3(x, y);
                        if (Mathf.Abs(x - combatant.transform.localPosition.x) + Mathf.Abs(y - combatant.transform.localPosition.y) <= combatant.remainingMoves && !currentPositionList.Contains(possibleOption))
                        {
                            GameObject target = Instantiate(moveTarget, FindObjectOfType<Arena>().transform, false) as GameObject;
                            target.transform.localPosition = possibleOption;
                            if (!oneIsSelected)
                            {
                                target.GetComponent<Button>().Select();
                                oneIsSelected = true;
                            }
                        }
                    }
                }
            }
        }
    }

    public void ExitMoveSelection(Vector3 target)
    {
        foreach (MoveTarget targetButton in FindObjectsOfType<MoveTarget>())
        {
            Destroy(targetButton.gameObject);
        }

        UpdatePositionList(currentCombatant.transform.localPosition, target);
        currentCombatant.MoveCombatant(target);
        fightMenuFrame.ActivateFightMenu(true);
        fightMenuFrame.ActivateTargetPanel(false);
        FindObjectOfType<MoveButton>().UpdateText(currentCombatant.remainingMoves);
        SelectAppropriateOption();
    }

    public void EnterEquip()
    {
        FindObjectOfType<ShowPanels>().EnterEquipMode();
        currentPlayerItemIDsAndQuantities = GetCurrentPlayerItems();
    }

    public void ExitEquip()
    {
        if (!CompareDicts(currentPlayerItemIDsAndQuantities, GetCurrentPlayerItems()))
        {
            currentCombatant.remainingActions -= 1;
            FindObjectOfType<ActionsButton>().UpdateText(currentCombatant.remainingActions);
        }
        FindObjectOfType<EquipButton>().GetComponent<Selectable>().Select();
        SelectAppropriateOption();
    }

    Dictionary<int, int> GetCurrentPlayerItems()
    {
        Dictionary<int, int> tempDict = new Dictionary<int, int>();
        foreach (Item item in FindObjectOfType<Inventory>().characterEquippedItems[currentCombatant.character])
        {
            if (tempDict.ContainsKey(item.ID))
            {
                tempDict[item.ID]++;
            }
            else
            {
                tempDict.Add(item.ID, 1);
            }
        }
        return tempDict;
    }

    bool CompareDicts(Dictionary<int, int> dict1, Dictionary<int, int> dict2)
    {
        if (dict1.Count != dict2.Count) return false;
        foreach (KeyValuePair<int, int> entry in dict1)
        {
            if (!dict2.ContainsKey(entry.Key) || dict2[entry.Key] != entry.Value) return false;
        }
        return true;
    }

    void SelectAppropriateOption()
    {
        foreach (Selectable selectable in FindObjectOfType<FightMenu>().GetComponentsInChildren<Selectable>())
        {
            if (selectable.IsInteractable())
            {
                selectable.Select();
                break;
            }
        }
    }

    public void UpdatePositionList(Vector3 oldPosition, Vector3 newPosition)
    {
        currentPositionList.Remove(oldPosition);
        currentPositionList.Add(newPosition);
    }

    public void EliminateCombatant(Combatant combatant)
    {
        currentPositionList.Remove(combatant.transform.localPosition);
        combatantButtons.Remove(combatant);
        currentCombatants.Remove(combatant);
    }

}
