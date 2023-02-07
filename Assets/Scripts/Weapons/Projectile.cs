using UnityEngine;

namespace IsoShooter.Weapons
{
    public class Projectile : MonoBehaviour
    {
        private float _projectileSpeed;
        private int _damage;
        private Transform _transform;

        private Vector3 _lastPosition;
    
        public void SetProjectileProperties(ProjectilesWeapon.ProjectileSettings projectileSettings)
        {
            _transform = GetComponent<Transform>();
            _projectileSpeed = projectileSettings.Speed;
            _damage = projectileSettings.Damage;
        }

        private void Update()
        {
            HandleMovement();
            HandleCollisions();
        }

        private void HandleMovement()
        {
            _lastPosition = _transform.position;
            _transform.Translate(Vector3.forward * Time.deltaTime * _projectileSpeed);
        }

        private void HandleCollisions()
        {
            Vector3 newPosition = _transform.position;
            float distance = Vector3.Distance(_lastPosition, newPosition);
            Ray ray = new Ray(_lastPosition, newPosition - _lastPosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, distance))
            {
                PerformCollision(hitInfo);
            }
        }
    
        private void PerformCollision (RaycastHit hitInfo)
        {
            if(hitInfo.collider.TryGetComponent(out IDamageable damageable))
            {
                damageable.HandleDamage(_damage);
            }

            //in production code this would be be done with object pooling
            Destroy(gameObject);
        }
    }
}
