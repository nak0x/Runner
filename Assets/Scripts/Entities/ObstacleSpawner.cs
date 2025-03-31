using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] Transform obstacle;
    [SerializeField] float timeBetweenWaves;
    
    float timeToSpawn = 2f;

    void Update()
    {
        if (Time.time >= timeToSpawn) {
            SpawnObstacles();
            timeToSpawn = Time.time + timeBetweenWaves;
        }
    }

    void SpawnObstacles()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (i != randomIndex) {
                Instantiate(obstacle, spawnPoints[i].position, Quaternion.identity);
            }
        }
    }
}
