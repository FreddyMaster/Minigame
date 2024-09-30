using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Camera mainCamera; // Reference to the main camera
    public float spawnOffset = 1f; // Distance from the screen edge to spawn enemies
    public float spawnInterval = 5f; // Time between enemy spawns in seconds
    public float groundLevel = 0f; // The y-coordinate of the ground level

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPosition = GetRandomSpawnPosition();
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float screenWidth = mainCamera.orthographicSize * mainCamera.aspect;
        float screenHeight = mainCamera.orthographicSize;

        // Randomly choose one of the four edges of the screen
        int edge = Random.Range(0, 4);

        switch (edge)
        {
            case 0: // Left edge
                return new Vector3(-screenWidth - spawnOffset, groundLevel, 0);
            case 1: // Right edge
                return new Vector3(screenWidth + spawnOffset, groundLevel, 0);
            case 2: // Top edge
                return new Vector3(Random.Range(-screenWidth, screenWidth), groundLevel + screenHeight + spawnOffset, 0);
            case 3: // Bottom edge
                return new Vector3(Random.Range(-screenWidth, screenWidth), groundLevel - screenHeight - spawnOffset, 0);
            default:
                return Vector3.zero;
        }
    }
}
