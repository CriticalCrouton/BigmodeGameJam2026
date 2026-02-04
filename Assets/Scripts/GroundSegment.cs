using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[Serializable]
public class WeightedRandomItem<T>
{
    public T item;
    public float weight;
}

public class GroundSegment : MonoBehaviour
{
    public float length { get { return GetComponent<SpriteRenderer>().bounds.extents.x; } }
    public List<WeightedRandomItem<Building>> possibleBuildingsForeground;
    public List<WeightedRandomItem<Building>> possibleBuildingsBackground;

    [SerializeField] Transform foregroundBuildingHeight;
    [SerializeField] Transform backgroundBuildingHeight;

    void Start()
    {
        SpawnRandomBuildingsForeground(UnityEngine.Random.Range(2, 5));
        // SpawnRandomBuildingsBackground(UnityEngine.Random.Range(2, 5));
    }

    void SpawnRandomBuildingsForeground(int count)
    {
        const int maxAttempts = 5;

        for (int i = 0; i < count; i++)
        {
            Building buildingToSpawn = getRandomBuilding(possibleBuildingsForeground);

            Building instance = Instantiate(
                                buildingToSpawn,
                                Vector3.up * 1000,
                                Quaternion.identity,
                                null
                            );
            instance.transform.SetParent(transform, true);

            for (int attempt = 1; attempt <= maxAttempts; attempt++)
            {
                float xPos = UnityEngine.Random.Range(length / 4f, length - (length / 4f));
                Vector3 spawnPos = new Vector3(
                                    transform.position.x + xPos,
                                    foregroundBuildingHeight.position.y,
                                    buildingToSpawn.transform.position.z
                                );

                instance.transform.position = spawnPos;
                Physics2D.SyncTransforms();

                Collider2D col = instance.GetComponent<Collider2D>();

                if (col == null)
                {
                    Destroy(instance);
                    break;
                }

                ContactFilter2D filter = new ContactFilter2D
                {
                    useLayerMask = true,
                    layerMask = LayerMask.GetMask("DestructableBuilding"),
                    useTriggers = true
                };

                Collider2D[] results = new Collider2D[1];
                int hits = col.Overlap(filter, results);

                if (hits == 0)
                {
                    break;
                }
                else if (attempt == maxAttempts)
                {
                    // Destroy(instance);
                    instance.GetComponent<SpriteRenderer>().color = Color.red;
                    Debug.Log("Failed to place building after max attempts.");
                }
            }
        }
    }


    Building getRandomBuilding(List<WeightedRandomItem<Building>> buildingList)
    {
        float totalWeight = 0f;
        foreach (WeightedRandomItem<Building> item in buildingList)
        {
            totalWeight += item.weight;
        }

        float randomValue = UnityEngine.Random.Range(0, totalWeight);
        float cumulativeWeight = 0f;

        foreach (WeightedRandomItem<Building> item in buildingList)
        {
            cumulativeWeight += item.weight;
            if (randomValue <= cumulativeWeight)
            {
                return item.item;
            }
        }

        return buildingList[buildingList.Count - 1].item; // Fallback
    }

}
