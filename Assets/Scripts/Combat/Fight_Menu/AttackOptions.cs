using UnityEngine;

public class AttackOptions : FightMenuButton
{
    public FightManager fightManager; //Assigned in inspector
    public GameObject attackOptionPrefab; //Assigned in inspector

    private CharacterDatabase characterDatabase;

    void Start()
    {
        characterDatabase = FindObjectOfType<CharacterDatabase>();
    }

    public void UpdateOptions()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        bool isAtLeastOneWeapon = false;

        foreach (Weapon weapon in characterDatabase.CharacterDictionary[fightManager.currentCombatant.character].EquippedItems)
        {
            isAtLeastOneWeapon = true;
            GameObject prefab = Instantiate(attackOptionPrefab, transform) as GameObject;
            AttackOptionPrefab option = prefab.GetComponent<AttackOptionPrefab>();
            option.title = weapon.Title;
            option.damageRange = weapon.DamageRange;
            option.damageMulti = weapon.DamageMulti;
            option.damageType = weapon.DamageType;
            option.reach = weapon.Reach;
            option.maxRange = weapon.MaxRange;
            option.UpdateFields();
        }

        if (!isAtLeastOneWeapon)
        {
            GameObject prefab = Instantiate(attackOptionPrefab, transform) as GameObject;
            AttackOptionPrefab option = prefab.GetComponent<AttackOptionPrefab>();
            option.title = "Unarmed Strike";
            option.damageRange = DieType.d1;
            option.damageMulti = 1;
            option.damageType = DamageType.Bludgeoning;
            option.reach = false;
            option.maxRange = 1;
            option.UpdateFields();
        }
    }
}
