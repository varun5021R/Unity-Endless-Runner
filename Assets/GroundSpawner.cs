using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public GameObject groundTile;
    public Transform player;
    public float spawnZ = 20f;
    public float tileLength = 20f;

    void Update()
    {
        if (player.position.z > spawnZ - 30)
        {
            SpawnTile();
        }
    }

    void SpawnTile()
    {
        Instantiate(groundTile, new Vector3(0, 0, spawnZ), Quaternion.identity);
        spawnZ += tileLength;
    }
}
