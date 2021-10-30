using UnityEngine;

namespace TowerDefense
{
    public class TurretRotator
    {
        Transform gunPivot;
        float turnSpeed;
        
        public TurretRotator(Transform gunPivot, float turnSpeed)
        {
            this.gunPivot = gunPivot;
            this.turnSpeed = turnSpeed;
        }

        public void Rotate(float dt, ITurretTargetProvider targetProvider)
        {
            Transform target = targetProvider.GetTarget();
            if (!target) return;
            Vector3 directionToTarget = (target.position - gunPivot.position).normalized;
            gunPivot.up = Vector3.RotateTowards(
                gunPivot.up, directionToTarget, turnSpeed * Mathf.Deg2Rad * dt, 0.0f
            );
        }
    }
}
