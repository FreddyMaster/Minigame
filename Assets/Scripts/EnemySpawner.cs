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
        // Start the coroutine to spawn enemies
        StartCoroutine(SpawnEnemyRoutine());
    }

    private IEnumerator SpawnEnemyRoutine()
    {
        while (true) // This will run indefinitely
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPosition = GetRandomOffScreenPosition();
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    private Vector3 GetRandomOffScreenPosition()
    {
        // Get the screen boundaries in world coordinates
        Vector3 screenBottomLeft = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 screenTopRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.nearClipPlane));

        // Randomly choose one of the four edges of the screen
        int edge = Random.Range(0, 4);

        Vector3 spawnPosition = Vector3.zero;

        switch (edge)
        {
            case 0: // Left edge
                spawnPosition = new Vector3(screenBottomLeft.x - spawnOffset, groundLevel, Random.Range(screenBottomLeft.z, screenTopRight.z));
                break;
            case 1: // Right edge
                spawnPosition = new Vector3(screenTopRight.x + spawnOffset, groundLevel, Random.Range(screenBottomLeft.z, screenTopRight.z));
                break;
            case 2: // Bottom edge
                spawnPosition = new Vector3(Random.Range(screenBottomLeft.x, screenTopRight.x), groundLevel, screenBottomLeft.z - spawnOffset);
                break;
            case 3: // Top edge
                spawnPosition = new Vector3(Random.Range(screenBottomLeft.x, screenTopRight.x), groundLevel, screenTopRight.z + spawnOffset);
                break;
        }

        return spawnPosition;
    }
}
