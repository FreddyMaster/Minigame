using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Camera mainCamera; // Reference to the main camera
    public float spawnOffset = 1.0f; // Distance from the screen edge to spawn enemies

    void Start()
    {
        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        Vector3 spawnPosition = GetRandomOffScreenPosition();
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    Vector3 GetRandomOffScreenPosition()
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
                spawnPosition = new Vector3(screenBottomLeft.x - spawnOffset, 0, Random.Range(screenBottomLeft.y, screenTopRight.y));
                break;
            case 1: // Right edge
                spawnPosition = new Vector3(screenTopRight.x + spawnOffset, 0, Random.Range(screenBottomLeft.y, screenTopRight.y));
                break;
            case 2: // Bottom edge
                spawnPosition = new Vector3(Random.Range(screenBottomLeft.x, screenTopRight.x), 0, screenBottomLeft.y - spawnOffset);
                break;
            case 3: // Top edge
                spawnPosition = new Vector3(Random.Range(screenBottomLeft.x, screenTopRight.x), 0, screenTopRight.y + spawnOffset);
                break;
        }

        return spawnPosition;
    }
}
