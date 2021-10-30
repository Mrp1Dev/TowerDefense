using UnityEngine;

namespace TowerDefense
{

    public class Turret : MonoBehaviour
    {
        [System.Serializable]
        public struct TurretTargetProviderData
        {
            public float range;
            public LayerMask enemyLayer;
        }
        [System.Serializable]
        public struct RotatorData
        {
            public float turnSpeed;
            public Transform gunPivot;
        }
        [SerializeField] private EnemySpawner spawner;
        [SerializeField] private TurretTargetProviderData targetProviderData;
        [SerializeField] private RotatorData rotatorData;
        /*[SerializeField] private float rotSpeed;
        [SerializeField] private float rpm;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform gunPivot;
        [SerializeField] private Transform gunBarrel;*/

        private ITurretTargetProvider targetProvider;
        private TurretRotator rotator;
        private void Awake()
        {
            targetProvider = new PathTurretTargetProvider(
               targetProviderData.range,
               transform,
               targetProviderData.enemyLayer,
               spawner
            );
            rotator = new TurretRotator(
                rotatorData.gunPivot,
                rotatorData.turnSpeed
            );
        }

        private void Update()
        {
            /*var hits = Physics2D.OverlapCircleAll(transform.position, range);
            if (hits.Length <= 0) return;
            var enemies = hits.Where(h => spawner.enemiesSpawned.Contains(h.transform)).Select(h => h.GetComponent<PathBasedMovement>());
            var highestIndex = enemies.Select(enemy => enemy.CurrentTarget).Max();
            var furthestPoint = spawner.Path[highestIndex];
            Transform furthestEnemy = null;
            foreach (var enemy in enemies.Where(enemy => enemy.CurrentTarget == highestIndex))
            {
                if (furthestEnemy == null) furthestEnemy = enemy.transform;
                else
                {
                    var isFarther = (furthestPoint.position - enemy.transform.position).sqrMagnitude <
                                    (furthestPoint.position - furthestEnemy.position).sqrMagnitude;
                    if (isFarther) furthestEnemy = enemy.transform;
                }
            }*/

            /*
            );*/
            rotator.Rotate(Time.deltaTime, targetProvider);
        }
    }
}
