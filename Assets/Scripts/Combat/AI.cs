using UnityEngine;

public class AI : MonoBehaviour
{

    public Combatant target;

    private Combatant myCombatant;
    private FightManager fightManager;
    private Enemy enemyType;

    void Awake()
    {
        myCombatant = GetComponent<Combatant>();
        fightManager = FindObjectOfType<FightManager>();
        enemyType = GetComponent<Enemy>();
    }

    public void StartTurn()
    {
        // TODO Decide what to do. For now, you're going to move as close as you can to a Friendly
        SelectNearestTarget();
        MoveForMelee();
    }

    public void FinishTurn()
    {
        target = null;
        Invoke("EndTurn", 1f);
    }

    void EndTurn()
    {
        fightManager.EndTurn();
    }

    void SelectNearestTarget()
    {
        Combatant[] combatants = FindObjectsOfType<Combatant>();
        foreach (Combatant combatant in combatants)
        {
            if (combatant.isFriendly)
            {
                if (target == null) target = combatant;
                else
                {
                    if (Vector3.Distance(combatant.transform.localPosition, transform.localPosition) < Vector3.Distance(target.transform.localPosition, transform.localPosition)) target = combatant;
                }
            }
        }
    }

    void MoveForMelee()
    {
        int spread = 1;
        while (spread < 7)
        {
            Vector3 bestPosition = new Vector3();
            for (int x = -spread; x <= spread; x++)
            {
                for (int y = -spread; y <= spread; y++)
                {
                    Vector3 position = new Vector3(target.transform.localPosition.x + x, target.transform.localPosition.y + y);
                    if (position.x >= 1 && position.x <= 8 && position.y >= 1 && position.y <= 8 && !fightManager.currentPositionList.Contains(position))
                    {
                        if (bestPosition.Equals(Vector3.zero)) bestPosition = position;
                        else if (Vector3.Distance(position, transform.localPosition) < Vector3.Distance(bestPosition, transform.localPosition)) bestPosition = position;
                    }
                }
            }

            if (Mathf.Abs(bestPosition.x - transform.localPosition.x) + Mathf.Abs(bestPosition.y - transform.localPosition.y) <= myCombatant.remainingMoves)
            {
                fightManager.UpdatePositionList(transform.localPosition, bestPosition);
                myCombatant.MoveCombatant(bestPosition);
                break;
            }
            else
            {
                spread++;
            }
        }
    }

    public void FinishedMoving()
    {
        if (myCombatant.remainingActions > 0) Attack();
    }

    void Attack()
    {
        Vector3 myPosition = new Vector3(transform.localPosition.x, transform.localPosition.y);
        bool withinMeleeRange = false;
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (myPosition.Equals(new Vector3(target.transform.localPosition.x + x, target.transform.localPosition.y + y)))
                {
                    withinMeleeRange = true;
                    break;
                }
            }
            if (withinMeleeRange) break;
        }
        if (withinMeleeRange) enemyType.Melee();
        else enemyType.Ranged();
        FinishTurn();
    }
}
