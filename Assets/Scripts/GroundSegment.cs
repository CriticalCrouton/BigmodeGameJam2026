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

    void Start()
    {
        SpawnRandomBuildingsForeground(UnityEngine.Random.Range(2, 5));
        //SpawnRandomBuildingsBackground(UnityEngine.Random.Range(2, 5));
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
                Physics2D.SyncTransforms();

                int hits = Physics2D.OverlapBoxAll(
                                spawnPos,
                                instance.GetComponent<SpriteRenderer>().bounds.size,
                                0,
                                LayerMask.GetMask("DestructableBuilding")
                ).Length;

                // Debug.Log("Spawn attempt " + attempt + " for building " + buildingToSpawn.name + " at " + spawnPos + " resulted in " + (hit ? "a hit" : "no hits"));

                if (hits == 0)
                {
                    instance.transform.position = spawnPos;
                    break;
                }
                else if (attempt == maxAttempts)
                {
                    Destroy(instance);
                    // instance.GetComponent<SpriteRenderer>().color = Color.red;
                    Debug.Log("Failed to place building after max attempts.");
                }
            }
        }
    }
    void SpawnRandomBuildingsBackground(int count)
    {
        const int maxAttempts = 5;

        for (int i = 0; i < count; i++)
        {
            Building buildingToSpawn = getRandomBuilding(possibleBuildingsBackground);

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
                                    backgroundBuildingHeight.position.y,
                                    buildingToSpawn.transform.position.z
                                );
                Physics2D.SyncTransforms();

                int hits = Physics2D.OverlapBoxAll(
                                spawnPos,
                                instance.GetComponent<SpriteRenderer>().bounds.size,
                                0,
                                LayerMask.GetMask("CannonDestructableBuilding")
                ).Length;

                // Debug.Log("Spawn attempt " + attempt + " for building " + buildingToSpawn.name + " at " + spawnPos + " resulted in " + (hit ? "a hit" : "no hits"));

                if (hits == 0)
                {
                    instance.transform.position = spawnPos;
                    break;
                }
                else if (attempt == maxAttempts)
                {
                    Destroy(instance);
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
}
