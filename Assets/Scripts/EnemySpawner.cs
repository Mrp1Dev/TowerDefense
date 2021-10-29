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
        private List<Transform> spawnedEnemies = new List<Transform>();
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
            instantiated.GetComponent<GroundEnemy>().Init(enemyInitData);
            spawnedEnemies.Add(instantiated.transform);
            EnemySpawned?.Invoke();
        }
    }
}
