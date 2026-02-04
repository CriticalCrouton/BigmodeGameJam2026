using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class WeightedRandomItem<T>
{
    public T item;
    public float weight;
}

public static class WeightedRandom
{
    public static T getRandomItem<T>(List<WeightedRandomItem<T>> itemList)
    {
        if (itemList == null || itemList.Count == 0)
            return default(T);

        float totalWeight = 0f;
        foreach (WeightedRandomItem<T> item in itemList)
        {
            totalWeight += item.weight;
        }

        float randomValue = UnityEngine.Random.Range(0, totalWeight);
        float cumulativeWeight = 0f;

        foreach (WeightedRandomItem<T> item in itemList)
        {
            cumulativeWeight += item.weight;
            if (randomValue <= cumulativeWeight)
            {
                return item.item;
            }
        }

        return itemList[itemList.Count - 1].item; // Fallback
    }
}