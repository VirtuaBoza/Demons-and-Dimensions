using UnityEngine;

public class EnemyBat : Enemy
{
    public override void Melee()
    {
        GetComponent<AI>().target.TakeDamage(1);
        GetComponent<Combatant>().remainingActions -= 1;
        GetComponent<Combatant>().PopUpText(Color.black, "Bite");
    }

    public override void Ranged()
    {
        //Do nothing. Bat does not have ranged attack
    }
}
