using System;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class EnemySpawner : MonoBehaviour
    {
        [Serializable]
        public struct EnemySpawningData
        {
            public GameObject prefab;
            public float spawnDelay;
        }

        public event Action EnemySpawned;

        [SerializeField] private EnemySpawningData spawningData;
        [SerializeField] private GroundEnemy.InitData enemyInitData;

        public List<Transform> SpawnedEnemies { get; private set; } = new List<Transform>();
        private float spawnTimer;

        private void Update()
        {
            var shouldSpawn = spawnTimer > spawningData.spawnDelay;
            if (shouldSpawn)
            {
                SpawnEnemy();
                spawnTimer = 0f;
            }

            spawnTimer += Time.deltaTime;
        }

        private void SpawnEnemy()
        {
            var instantiated = Instantiate(spawningData.prefab, transform.position, Quaternion.identity);

            var groundEnemy = instantiated.GetComponent<GroundEnemy>();
            groundEnemy.Init(enemyInitData);
            groundEnemy.PathBasedMovement.ReachedEnd += () => SpawnedEnemies.Remove(groundEnemy.transform);

            SpawnedEnemies.Add(instantiated.transform);
            EnemySpawned?.Invoke();
        }
    }
}
