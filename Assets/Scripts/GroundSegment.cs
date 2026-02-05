using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;



public class GroundSegment : MonoBehaviour
{
    public float length { get { return GetComponent<SpriteRenderer>().bounds.extents.x; } }
    public List<WeightedRandomItem<Building>> possibleBuildingsForeground;
    public List<WeightedRandomItem<Building>> possibleBuildingsBackground;

    [SerializeField] Transform foregroundBuildingHeight;
    [SerializeField] Transform backgroundBuildingHeight;
    [SerializeField] MinMax<int> foregroundBuildingAmount;
    [SerializeField] MinMax<int> backgroundBuildingsAmount;
    

    List<Building> buildings = new List<Building>();

    void Start()
    {
        //foreground and then background
        SpawnRandomBuildings(foregroundBuildingHeight, possibleBuildingsForeground, UnityEngine.Random.Range(foregroundBuildingAmount.min, foregroundBuildingAmount.max));
        SpawnRandomBuildings(backgroundBuildingHeight, possibleBuildingsBackground, UnityEngine.Random.Range(backgroundBuildingsAmount.min, backgroundBuildingsAmount.max));

    }

    void SpawnRandomBuildings(Transform height, List<WeightedRandomItem<Building>> buildingsList, int count)
    {
        const int maxAttempts = 50;

        for (int i = 0; i < count; i++)
        {
            Building buildingToSpawn = getRandomBuilding(buildingsList);

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
                                    height.position.y,
                                    buildingToSpawn.transform.position.z
                                );
                Physics2D.SyncTransforms();

                bool canSpawn = IsPositionEmpty(instance.GetComponent<Collider2D>(), spawnPos, instance.gameObject.layer);

                // Debug.Log("Spawn attempt " + attempt + " for building " + buildingToSpawn.name + " at " + spawnPos + " resulted in " + (hit ? "a hit" : "no hits"));

                if (canSpawn)
                {
                    instance.transform.position = spawnPos;
                    buildings.Add(instance);
                    break;
                }
                else if (attempt == maxAttempts)
                {
                    Destroy(instance);
                    // instance.transform.position = spawnPos;
                    // instance.GetComponent<SpriteRenderer>().color = Color.red;
                    Debug.Log("Failed to place building after max attempts.");
                }
            }
        }
    }

    Building getRandomBuilding(List<WeightedRandomItem<Building>> buildingList)
    {
        return WeightedRandom.getRandomItem(buildingList);
    }

    // Check if position is empty from existing objects
    private bool IsPositionEmpty(Collider2D collider2D, Vector3 position, int layer)
    {
        foreach (var item in buildings)
        {
            if (!item.TryGetComponent(out Collider2D col))
                continue;

                Debug.Log("collider bounds: " + col.bounds.extents.x + ", " + collider2D.bounds.extents.x);
                
            float range =
                col.bounds.extents.x +
                collider2D.bounds.extents.x;

            if (
                Mathf.Abs(item.transform.position.x - position.x) < range &&
                col.gameObject.layer == layer
            )
            {
                
                return false;
            }
        }

        return true;
    }

}
