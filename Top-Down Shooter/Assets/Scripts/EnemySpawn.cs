using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] GameObject EnemyPrefab1;
    [SerializeField] GameObject EnemyPrefab2;
    [SerializeField] float minSpawnTime = 1f;
    [SerializeField] float maxSpawnTime = 3f;

    float spawnDistance = 10f;
    Vector2 screenBounds;
    Vector2 spawnPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy()
    {
        float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        int side = Random.Range(0, 4);
        switch (side)
        {
            case 0: // top
                spawnPos = new Vector2(Random.Range(-screenBounds.x - spawnDistance, screenBounds.x + spawnDistance), screenBounds.y + spawnDistance);
                break;
            case 1: // bottom
                spawnPos = new Vector2(Random.Range(-screenBounds.x - spawnDistance, screenBounds.x + spawnDistance), -screenBounds.y - spawnDistance);
                break;
            case 2: // left
                spawnPos = new Vector2(-screenBounds.x - spawnDistance, Random.Range(-screenBounds.y - spawnDistance, screenBounds.y + spawnDistance));
                break;
            case 3: // right
                spawnPos = new Vector2(screenBounds.x + spawnDistance, Random.Range(-screenBounds.y - spawnDistance, screenBounds.y + spawnDistance));
                break;
        }
        float enemyType = Random.Range(0f, 10f);
        if (enemyType <= 3f)
        {
            Instantiate(EnemyPrefab2, spawnPos, transform.rotation);
        }
        else
        {
            Instantiate(EnemyPrefab1, spawnPos, transform.rotation);
        }

        Invoke("SpawnEnemy", spawnTime);
    }
}
