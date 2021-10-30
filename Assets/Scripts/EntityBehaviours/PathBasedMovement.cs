using System;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class PathBasedMovement : IUpdate, IEnemyMovement
    {
        public int CurrentTarget { get; private set; }
        public event Action ReachedEnd;
        private readonly float moveSpeed;
        private readonly Rigidbody2D rb;
        private readonly List<Transform> path;
        private readonly Transform transform;

        public PathBasedMovement(float moveSpeed, Rigidbody2D rb, List<Transform> path, Transform transform)
        {
            this.moveSpeed = moveSpeed;
            this.rb = rb;
            this.path = path;
            this.transform = transform;
        }

        public void Update(float dt)
        {
            var hasReachedEnd = CurrentTarget >= path.Count;
            if (hasReachedEnd)
            {
                OnReachedEnd();
                return;
            }

            ModifyVelocity(rb);
            TryChangeTarget();
        }

        private void OnReachedEnd()
        {
            ReachedEnd?.Invoke();
            GameObject.Destroy(transform.gameObject);
        }

        private void ModifyVelocity(Rigidbody2D rb)
        {
            var directionToCurrentTargetPoint = (path[CurrentTarget].position - transform.position).normalized;
            rb.velocity = directionToCurrentTargetPoint * moveSpeed;
        }

        private void TryChangeTarget()
        {
            var isApproximatelyOnTarget = (transform.position - path[CurrentTarget].position).sqrMagnitude < 0.002f;
            if (isApproximatelyOnTarget) CurrentTarget++;
        }
    }
}
