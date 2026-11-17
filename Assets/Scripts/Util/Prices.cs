using UnityEngine;

public static class Prices
{
    public static int Level(int level)
    {
        int[] basePrices = { 5, 10, 25 };
        return basePrices[level % 3] * Mathf.RoundToInt(Mathf.Pow(10, level / 3));      
    }

    public static int SteakSlot(int level)
    {
        return 100 * Mathf.RoundToInt(Mathf.Pow(level, 2f));
    }
}
