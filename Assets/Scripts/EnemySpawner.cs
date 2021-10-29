using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnDelay;
    [field: SerializeField] public List<Transform> Path { get; private set; } = new List<Transform>();
    public List<Transform> enemiesSpawned = new List<Transform>();
    private float lastSpawnTime;

    private void Awake()
    {
        lastSpawnTime = Time.time;
    }

    private void Update()
    {
        var x = 0f;
        if (Time.time - lastSpawnTime > spawnDelay)
        {
            var instantiated = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            instantiated.GetComponent<EnemyMovement>().Init(Path);
            enemiesSpawned.Add(instantiated.transform);
            lastSpawnTime = Time.time;
        }
    }
}
