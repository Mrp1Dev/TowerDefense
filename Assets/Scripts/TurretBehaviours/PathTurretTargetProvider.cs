using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace TowerDefense
{
    public class PathTurretTargetProvider : ITurretTargetProvider
    {
        private const int circleCastBufferLength = 7;
        private float range;
        private Transform transform;
        private LayerMask enemyLayer;
        private Collider2D[] possibleHits = new Collider2D[circleCastBufferLength];
        private List<Transform> path;
        public PathTurretTargetProvider(float range, Transform transform, LayerMask enemyLayer, List<Transform> path)
        {
            this.range = range;
            this.transform = transform;
            this.enemyLayer = enemyLayer;
            this.path = path;
        }

        public Transform GetTarget()
        {
            /*var hitCount = Physics2D.OverlapCircleNonAlloc(transform.position, range, possibleHits, enemyLayer);
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
            return lowestItem;*/

            /*var hitCount = Physics2D.OverlapCircleNonAlloc(transform.position, range, possibleHits, enemyLayer);
            if (hitCount <= 0) return null;
            var enemies = possibleHits.Take(hitCount).Select(h => h.transform.GetComponent<GroundEnemy>());
            var highestIndex = enemies.Select(enemy => enemy.PathBasedMovement.CurrentTarget).Max();
            var furthestPoint = path[highestIndex];
            Transform furthestEnemy = null;
            foreach (var enemy in enemies.Where(enemy => enemy.PathBasedMovement.CurrentTarget == highestIndex))
            {
                if (furthestEnemy == null) furthestEnemy = enemy.transform;
                else
                {
                    var isFarther = (furthestPoint.position - enemy.transform.position).sqrMagnitude <
                                    (furthestPoint.position - furthestEnemy.position).sqrMagnitude;
                    if (isFarther) furthestEnemy = enemy.transform;
                }
            }
            return furthestEnemy;*/
            var hitCount = Physics2D.OverlapCircleNonAlloc(transform.position, range, possibleHits, enemyLayer);
            if (hitCount <= 0) return null;
            Transform currentFurthestEnemy = null;
            var furthestDistance = Mathf.Infinity;
            for (int i = 0; i < hitCount; i++)
            {
                Collider2D enemy = possibleHits[i];
                float distance = SqrDistanceToEnd(enemy.GetComponent<GroundEnemy>(), path);
                var isCloserToEnd = distance < furthestDistance;
                if (isCloserToEnd)
                {
                    furthestDistance = distance;
                    currentFurthestEnemy = enemy.transform;
                }
            }
            return currentFurthestEnemy;
        }

        public float SqrDistanceToEnd(GroundEnemy enemy, List<Transform> path)
        {
            var target = enemy.PathBasedMovement.CurrentTarget;
            var enemyToTarget = (path[target].position - enemy.transform.position).sqrMagnitude;
            var leftDistance = 0f;
            for(var i = target; i < path.Count - 1; i++)
            {
                leftDistance += (path[i + 1].position - path[i].position).sqrMagnitude;
            }
            return enemyToTarget + leftDistance;
        }
    }
}
