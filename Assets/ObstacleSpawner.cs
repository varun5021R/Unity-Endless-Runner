using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("References")]
    public GameObject obstaclePrefab;
    public GameObject coinPrefab;
    public Transform player;

    [Header("Spawn Settings")]
    public float spawnDistance = 30f;
    public float spawnInterval = 2f;

    private float timer;

    void Update()
    {
        if (Time.timeScale == 0f) return;

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnObstacle();
        }
    }

    void SpawnObstacle()
    {
        // Random lane (-2.5, 0, 2.5)
        float laneX = Random.Range(0, 3);

        if (laneX == 0)
            laneX = -2.5f;
        else if (laneX == 1)
            laneX = 0f;
        else
            laneX = 2.5f;

        Vector3 spawnPos = new Vector3(
            laneX,
            0.5f,
            player.position.z + spawnDistance
        );

        // Spawn obstacle
        Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);

        // Spawn coin above obstacle
        if (coinPrefab != null)
        {
            Vector3 coinPos = new Vector3(
                laneX,
                2f,
                spawnPos.z
            );

            Instantiate(coinPrefab, coinPos, Quaternion.identity);
        }
    }
}
