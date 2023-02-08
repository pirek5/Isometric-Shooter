using UnityEngine;

namespace IsoShooter.Weapons
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField]
        private AudioSource audioSource;
        
        private ProjectilesWeapon.ProjectileSettings _settings;
        private Transform _transform;

        private Vector3 _lastPosition;
    
        
        public void SetProjectileProperties(ProjectilesWeapon.ProjectileSettings projectileSettings)
        {
            _transform = GetComponent<Transform>();
            _settings = projectileSettings;
        }

        private void Update()
        {
            HandleMovement();
            HandleCollisions();
        }

        private void HandleMovement()
        {
            _lastPosition = _transform.position;
            _transform.Translate(Vector3.forward * Time.deltaTime * _settings.Speed);
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
                damageable.HandleDamage(_settings.Damage);
            }

            if (_settings.HitSfx != null)
            {
                audioSource.PlayOneShot(_settings.HitSfx);
            }
            
            //in production code this should be be done with object pooling
            if (_settings.HitFx != null)
            {
                Instantiate(_settings.HitFx, hitInfo.point, Quaternion.identity);
            }
            
            Destroy(gameObject);
        }
    }
}
