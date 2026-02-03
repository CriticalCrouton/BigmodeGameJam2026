using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public static MapGenerator Instance { get; private set; }

    public Transform player;
    public List<GroundSegment> segmentPrefabs;
    public int segmentsAhead = 3;
    private float nextX = 23f;
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
        for (int i = 0; i < segmentsAhead; i++)
            SpawnSegment();
    }

    void Update()
    {
        if (GameManagement.Instance.GameState != GameState.Run) return;

        if (player.position.x + 50 >= nextX)
        {
            SpawnSegment();
            RemoveOldSegment();
        }
    }

    void SpawnSegment()
    {
        int index = Random.Range(0, segmentPrefabs.Count);
        GroundSegment segmentToSpawn = segmentPrefabs[index];

        GroundSegment newSegment = Instantiate(
            segmentToSpawn,
            new Vector3(nextX, -4, 0.1f),
            Quaternion.identity
        );

        activeSegments.Enqueue(newSegment);
        Debug.Log(newSegment.length);

        nextX += newSegment.length * 2;
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
    }
}
