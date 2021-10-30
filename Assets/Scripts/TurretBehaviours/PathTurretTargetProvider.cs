using UnityEngine;
using System.Linq;
namespace TowerDefense
{
    public class PathTurretTargetProvider : ITurretTargetProvider
    {
        private const int circleCastBufferLength = 7;
        private float range;
        private Transform transform;
        private LayerMask enemyLayer;
        private Collider2D[] possibleHits = new Collider2D[circleCastBufferLength];
        private EnemySpawner spawner;

        public PathTurretTargetProvider(float range, Transform transform, LayerMask enemyLayer, EnemySpawner spawner)
        {
            this.range = range;
            this.transform = transform;
            this.enemyLayer = enemyLayer;
            this.spawner = spawner;
        }

        public Transform GetTarget()
        {
            var hitCount = Physics2D.OverlapCircleNonAlloc(transform.position, range, possibleHits, enemyLayer);
            if (hitCount <= 0) return null;
            //return possibleHits.Take(hitCount).OrderBy(h => spawner.SpawnedEnemies.IndexOf(h.transform)).First().transform;
            var lowestIndex = int.MaxValue;
            Transform lowestItem = null;
            for(int i = 0; i < hitCount; i++)
            {
                int index = spawner.SpawnedEnemies.IndexOf(possibleHits[i].transform);
                if (index < lowestIndex && index >= 0)
                {
                    lowestIndex = index;
                    lowestItem = possibleHits[i].transform;
                }
            }
            return lowestItem;
        }
    }
}
