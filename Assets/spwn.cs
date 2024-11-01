using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spwn : MonoBehaviour
{   
    public GameObject prefab;
    public int startNumberOfObjectsPerSpawn = 2; // Initial number of objects to spawn per spawn event
    public float startSpawnInterval = 3f; // Initial interval between spawn events

    private List<Vector3> spawnPositions = new List<Vector3>(); // List of spawn positions

    private int numberOfObjectsPerSpawn;
    private float spawnInterval;

    void Start()
    {
        // Define the spawn positions
        spawnPositions.Add(new Vector3(-11f, 3f, -0.074f));
        spawnPositions.Add(new Vector3(11f, 3f, -0.074f));
        spawnPositions.Add(new Vector3(-11f, 5f, -0.074f));
        spawnPositions.Add(new Vector3(11f, 5f, -0.074f));

        // Set initial values
        numberOfObjectsPerSpawn = startNumberOfObjectsPerSpawn;
        spawnInterval = startSpawnInterval;

        // Start spawning objects
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        int spawnEventsCount = 0; // Counter to track the number of spawn events
        
        while (true)
        {
            // Spawn the specified number of objects
            for (int i = 0; i < numberOfObjectsPerSpawn; i++)
            {
                // Randomly choose a spawn position
                Vector3 randomPosition = GetRandomPositionWithinArea();
                
                // Spawn the object at the chosen position
                Instantiate(prefab, randomPosition, Quaternion.identity);
            }

            // Decrease the spawn interval
            spawnInterval -= 0.1f;
            if (spawnInterval < 1.5f) // Limit the minimum spawn interval
                spawnInterval = 1.5f;

            // Increase the number of spawned items by 1 every 5 spawn events, limited to 5
            spawnEventsCount++;
            if (spawnEventsCount % 5 == 0 && numberOfObjectsPerSpawn < 5)
            {
                numberOfObjectsPerSpawn++;
            }

            // Wait for the specified interval before spawning again
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    Vector3 GetRandomPositionWithinArea()
    {
        // Randomly choose one of the four points
        Vector3 randomPoint = spawnPositions[Random.Range(0, spawnPositions.Count)];
        
        // Define range around the chosen point
        float rangeX = 2f;
        float rangeY = 2f;

        // Generate random position within range around the chosen point
        float randomX = Random.Range(randomPoint.x - rangeX, randomPoint.x + rangeX);
        float randomY = Random.Range(randomPoint.y - rangeY, randomPoint.y + rangeY);

        // Ensure z coordinate is consistent with the points
        float z = randomPoint.z;

        return new Vector3(randomX, randomY, z);
    }
}
