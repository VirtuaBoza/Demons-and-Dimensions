using UnityEngine;

public enum DIE { d20, d12, d10, d8, d6, d4, d00, one }

public static class Die
{
    public static int RollADie(DIE die)
    {
        switch (die)
        {
            case DIE.d20:
                return Random.Range(1, 21);
            case DIE.d12:
                return Random.Range(1, 13);
            case DIE.d10:
                return Random.Range(1, 11);
            case DIE.d8:
                return Random.Range(1, 9);
            case DIE.d6:
                return Random.Range(1, 7);
            case DIE.d4:
                return Random.Range(1, 5);
            case DIE.d00:
                int roll = Random.Range(1, 11);
                return roll * 10;
            case DIE.one:
                return 1;
            default:
                return 0;
        }
    }
}
