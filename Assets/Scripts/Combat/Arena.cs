using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class Arena : MonoBehaviour
{
    public GameObject crystalPrefab, damienPrefab, hunterPrefab, teddyPrefab;
    [Range(1, 16)] public int[] numberOfEnemiesByType;
    public GameObject[] enemyPrefabs;
    public Sprite[] arenas = new Sprite[2];
    public int mapIndex = 0;
    public bool isCrystalPlaying, isDamienPlaying, isHunterPlaying, isTeddyPlaying;
    public FightMenuFrame fightMenuFrame;
    public FriendlyLayoutGroup friendlyLayoutGroup;
    public EnemyLayoutGroup enemyLayoutGroup;
    public List<Combatant> combatants = new List<Combatant>();

    private List<Vector3> positionList = new List<Vector3>();   //To keep track of positions so that no two combatants have the same position
    private List<int> initiativeList = new List<int>();         //To keep track of initiative rolls so that no two combatants have the same initiative

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = arenas[mapIndex];
        InstantiateEnemies();
        InstantiateFriendlies();
        FindObjectOfType<FightManager>().StartFight(combatants);
        FindObjectOfType<StartOptions>().inMainMenu = false;
        FindObjectOfType<ShowPanels>().inFight = true;
    }

    private void InstantiateEnemies()
    {
        for (int index = 0; index < numberOfEnemiesByType.Length; index++)
        {
            for (int number = 1; number <= numberOfEnemiesByType[index]; number++)
            {
                PlaceCharacter(false, enemyPrefabs[index]);
            }
        }
    }

    private void InstantiateFriendlies()
    {
        if (isCrystalPlaying)
        {
            PlaceCharacter(true, crystalPrefab);
        }
        if (isDamienPlaying)
        {
            PlaceCharacter(true, damienPrefab);
        }
        if (isHunterPlaying)
        {
            PlaceCharacter(true, hunterPrefab);
        }
        if (isTeddyPlaying)
        {
            PlaceCharacter(true, teddyPrefab);
        }

        List<Combatant> sortedList = combatants.OrderByDescending(c => c.initiative).ToList();
        combatants = sortedList;
        foreach (Combatant combatant in combatants)
        {
            if (combatant.isFriendly) { combatant.infoBox = friendlyLayoutGroup.PopulateInfoBox(combatant.myName, combatant.currentHp, combatant.maxHp); }
            else { combatant.infoBox = enemyLayoutGroup.PopulateInfoBox(combatant.myName, combatant.currentHp, combatant.maxHp); }
        }
    }

    private void PlaceCharacter(bool isFriendly, GameObject prefab)
    {
        Vector3 position = FindOpenPosition(isFriendly);
        GameObject character = Instantiate(prefab, position, Quaternion.identity) as GameObject;
        character.transform.SetParent(transform, false);
        character.GetComponent<SpriteRenderer>().sortingOrder = 10 - ((int)position.y);
        if (isFriendly) { character.GetComponent<Animator>().SetTrigger("lookRight"); }

        Combatant combatant = character.GetComponent<Combatant>();
        combatant.initiative = RollForInitiative();
        combatants.Add(combatant);
    }

    private Vector3 FindOpenPosition(bool isFriendly)
    {
        Vector3 position;
        do
        {
            if (isFriendly) position = new Vector3(Random.Range(1, 5), Random.Range(1, 9), transform.position.z);
            else position = new Vector3(Random.Range(5, 9), Random.Range(1, 9), transform.position.z);
        } while (positionList.Contains(position));
        positionList.Add(position);
        return position;
    }

    private int RollForInitiative()
    {
        int roll = DieRoller.RollADie(DieType.d20);
        if (initiativeList.Contains(roll)) return RollForInitiative();
        else
        {
            initiativeList.Add(roll);
            return roll;
        }
    }
}
