using UnityEngine;

public class AttackOptions : FightMenuButton
{
    public FightManager fightManager; //Assigned in inspector
    public GameObject attackOptionPrefab; //Assigned in inspector

    public void UpdateOptions()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        Inventory inventory = FindObjectOfType<Inventory>();
        bool isAtLeastOneWeapon = false;

        foreach (Item item in inventory.characterEquippedItems[fightManager.currentCombatant.character])
        {
            if (item.Itemtype == ItemType.Weapon)
            {
                GameObject prefab = Instantiate(attackOptionPrefab, transform) as GameObject;
                AttackOptionPrefab option = prefab.GetComponent<AttackOptionPrefab>();
                option.title = item.Title;
                option.damageRange = item.Damagerange;
                option.damageMulti = item.Damagemulti;
                option.damageType = item.Damagetype;
                option.finesse = item.Finesse;
                option.heavy = item.Heavy;
                option.isLight = item.Light;
                option.reach = item.Reach;
                option.twoHanded = item.Twohanded;
                option.range = item.Range;
                option.maxRange = item.Maxrange;
                option.UpdateFields();

                isAtLeastOneWeapon = true;
            }
        }

        if (!isAtLeastOneWeapon)
        {
            GameObject prefab = Instantiate(attackOptionPrefab, transform) as GameObject;
            AttackOptionPrefab option = prefab.GetComponent<AttackOptionPrefab>();
            option.title = "Unarmed Strike";
            option.damageRange = DieType.one;
            option.damageMulti = 1;
            option.damageType = DamageType.Bludgeoning;
            option.finesse = false;
            option.heavy = false;
            option.isLight = true;
            option.reach = false;
            option.twoHanded = false;
            option.range = 1;
            option.maxRange = 1;
            option.UpdateFields();
        }
    }
}
