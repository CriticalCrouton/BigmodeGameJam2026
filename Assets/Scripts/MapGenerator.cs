using System.Collections.Generic;
using UnityEngine;



public class MapGenerator : MonoBehaviour
{
    public static MapGenerator Instance { get; private set; }

    public Transform player;
    public List<WeightedRandomItem<GroundSegment>> segmentPrefabs;

    public int segmentsAhead = 3;
    private float nextX = 41f;
    private Queue<GroundSegment> activeSegments = new Queue<GroundSegment>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        player = PirateShipTest.Instance.transform;
        SpawnSegments(10);
    }

    void Update()
    {
        if (GameManagement.Instance.GameState != GameState.Run) return;

        if (player.position.x + 50 >= nextX)
        {
            SpawnSegments(10);
            RemoveOldSegment();
        }
    }

    void SpawnSegments(int count)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnSegment(WeightedRandom.getRandomItem(segmentPrefabs));
        }
    }

    public void SpawnSegment(GroundSegment segmentToSpawn)
    {

        GroundSegment newSegment = Instantiate(
            segmentToSpawn,
            new Vector3(nextX, -6, 0.1f),
            Quaternion.identity
        );

        activeSegments.Enqueue(newSegment);
        Debug.Log(newSegment.length);

        nextX += newSegment.length;
    }


    void RemoveOldSegment()
    {
        if (activeSegments.Count > segmentsAhead + 1)
        {
            Destroy(activeSegments.Dequeue().gameObject);
        }
    }

    public void Reset()
    {
        foreach (GroundSegment segment in activeSegments)
        {
            Destroy(segment.gameObject);
        }
        activeSegments.Clear();
        nextX = 23f;

        SpawnSegments(10);
    }
}
