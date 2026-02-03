using System.Collections.Generic;
using UnityEngine;

public class GroundSegment : MonoBehaviour
{
    public float length { get { return GetComponent<SpriteRenderer>().bounds.extents.x; } }
    public List<Building> possibleBuildings;

    void Start()
    {
        SpawnRandomBuilding();
    }

    void SpawnRandomBuilding()
    {
        if (possibleBuildings == null || possibleBuildings.Count == 0)
            return;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Bounds bounds = sr.bounds;

        Building prefab = possibleBuildings[Random.Range(0, possibleBuildings.Count)];

        float randomX = Random.Range(bounds.min.x + bounds.extents.x /4, bounds.max.x - bounds.extents.x /4);

        Vector3 spawnPosition = new Vector3(
            randomX,
            -3.2f,
            0f
        );

        Instantiate(prefab, spawnPosition, Quaternion.identity);//, transform); This prevents the buildings from being heinously wide
    }
}
