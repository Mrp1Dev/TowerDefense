using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class GroundEnemy : MonoBehaviour
    {
        [System.Serializable]
        public struct InitData
        {
            public List<Transform> path;
        }
        [System.Serializable]
        public struct MovementData
        {
            public float moveSpeed;
        }

        [SerializeField] private MovementData movementData;
        public PathBasedMovement PathBasedMovement { get; private set; }

        private readonly List<IUpdate> updates = new List<IUpdate>();

        public void Init(InitData data)
        {
            PathBasedMovement = new PathBasedMovement(
                movementData.moveSpeed,
                GetComponent<Rigidbody2D>(),
                data.path,
                transform
            );
            updates.Add(PathBasedMovement);
        }

        private void Update()
        {
            PathBasedMovement.Update(Time.deltaTime);
        }
    }
}
