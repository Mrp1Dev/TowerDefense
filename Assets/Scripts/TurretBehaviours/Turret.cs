using System.Collections.Generic;
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
            public List<Transform> path;
        }
        [System.Serializable]
        public struct RotatorData
        {
            public float turnSpeed;
            public Transform gunPivot;
        }

        [SerializeField] private TurretTargetProviderData targetProviderData;
        [SerializeField] private RotatorData rotatorData;
        /*[SerializeField] private float rotSpeed;
        [SerializeField] private float rpm;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform gunPivot;
        [SerializeField] private Transform gunBarrel;*/

        private ITurretTargetProvider targetProvider;
        private TurretRotator rotator;
        public void Init(List<Transform> path)
        {
            targetProvider = new PathTurretTargetProvider(
               targetProviderData.range,
               transform,
               targetProviderData.enemyLayer,
               path
            );
            rotator = new TurretRotator(
                rotatorData.gunPivot,
                rotatorData.turnSpeed
            );
        }

        private void Update()
        {
            rotator?.Rotate(Time.deltaTime, targetProvider);
        }
    }
}
