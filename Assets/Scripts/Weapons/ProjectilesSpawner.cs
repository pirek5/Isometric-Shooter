using UnityEngine;

namespace IsoShooter.Weapons
{
    public class ProjectilesSpawner : MonoBehaviour
    {
        //in production code this would be be done with object pooling
    
        [SerializeField]
        private Projectile projectilePrefab;

        public Projectile SpawnProjectile(Vector3 position, Quaternion rotation)
        {
            return Instantiate(projectilePrefab, position, rotation, transform);
        }
    }
}


