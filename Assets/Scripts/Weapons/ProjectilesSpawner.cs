using UnityEngine;
using UnityEngine.Serialization;

namespace IsoShooter.Weapons
{
    public class ProjectilesSpawner : MonoBehaviour
    {
        [SerializeField]
        private Projectile _projectilePrefab;

        
        public Projectile SpawnProjectile(Vector3 position, Quaternion rotation)
        {
            //in production code this would be be done with object pooling
            return Instantiate(_projectilePrefab, position, rotation, transform);
        }
    }
}


