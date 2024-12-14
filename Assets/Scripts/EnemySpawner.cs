using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour 
{
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private float spawnInterval = 2f;

    [SerializeField]
    private float spawnBuff = 0.5f; // How much the spawn time reduces each time

    [SerializeField]
    private int spawnBuffInterval = 10; // How often the spawn time reduces (every x spawn events)

    [SerializeField]
    private float spawnOffset = 10f; // Distance from the screen borden to spawn

    private Camera mainCamera;
    private int spawnEvents = 0;

    void Start() {
        mainCamera = Camera.main;

        // Start spawning enemies
        InvokeRepeating(nameof(SpawnEnemy), spawnInterval, spawnInterval);
    }

    void SpawnEnemy() {
        if(spawnEvents++ % spawnBuffInterval == 0) {
            spawnInterval -= spawnBuffInterval;
        }

        // Get screen boundaries in world space
        Vector2 screenMin = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector2 screenMax = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

        Vector2 spawnPosition = Vector2.zero;

        // Randomly pick a side of the screen
        int side = Random.Range(0, 4); // 0: Top, 1: Bottom, 2: Left, 3: Right

        switch(side) {
            case 0: // Top
                spawnPosition = new Vector2(Random.Range(screenMin.x, screenMax.x), screenMax.y + spawnOffset);
                break;
            case 1: // Bottom
                spawnPosition = new Vector2(Random.Range(screenMin.x, screenMax.x), screenMin.y - spawnOffset);
                break;
            case 2: // Left
                spawnPosition = new Vector2(screenMin.x - spawnOffset, Random.Range(screenMin.y, screenMax.y));
                break;
            case 3: // Right
                spawnPosition = new Vector2(screenMax.x + spawnOffset, Random.Range(screenMin.y, screenMax.y));
                break;
        }

        // Instantiate the enemy at the spawn position
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
