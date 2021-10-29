using System.Linq;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private float rotSpeed;
    [SerializeField] private float rpm;
    [SerializeField] private float range;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform gunPivot;
    [SerializeField] private Transform gunBarrel;
    [SerializeField] private EnemySpawner spawner;

    private void Update()
    {
        var hits = Physics2D.OverlapCircleAll(transform.position, range);
        if (hits.Length <= 0) return;
        var enemies = hits.Where(h => spawner.enemiesSpawned.Contains(h.transform)).Select(h => h.GetComponent<EnemyMovement>());
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
        }

        gunPivot.up = Vector3.RotateTowards(
            gunPivot.up, (furthestEnemy.transform.position - transform.position).normalized, rotSpeed * Mathf.Deg2Rad * Time.deltaTime, 0.0f
        );
    }
}
