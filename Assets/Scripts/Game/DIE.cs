using UnityEngine;

public static class Die
{
    public static int RollADie(DieType die)
    {
        switch (die)
        {
            case DieType.d20:
                return Random.Range(1, 21);
            case DieType.d12:
                return Random.Range(1, 13);
            case DieType.d10:
                return Random.Range(1, 11);
            case DieType.d8:
                return Random.Range(1, 9);
            case DieType.d6:
                return Random.Range(1, 7);
            case DieType.d4:
                return Random.Range(1, 5);
            case DieType.d00:
                int roll = Random.Range(1, 11);
                return roll * 10;
            case DieType.d1:
                return 1;
            default:
                return 0;
        }
    }
}
