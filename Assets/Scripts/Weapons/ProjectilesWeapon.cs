using System;
using System.Collections.Generic;
using MEC;
using UnityEngine;

namespace IsoShooter.Weapons
{
    public class ProjectilesWeapon : Weapon, IReloadableWeapon
    {
        public event Action OnMagazineStatusChanged;

        [SerializeField]
        private ProjectilesSpawner _projectilesSpawner;
        [SerializeField]
        private AudioSource _audioSource;
        [Space]
        [SerializeField]
        private int _magazineCapacity;
        [SerializeField]
        private float _reloadTime;
        [SerializeField]
        private AudioClip _fireSound;
        [Space]
        [SerializeField]
        private ProjectileSettings _projectileSettings;
        [Space]
        [SerializeField]
        private Transform _projectileSpawnPoint;


        private bool _isReloading;
        private int _projectilesLeftInMagazine;
        private float _currentReloadTime;


        public override void InitializeWeapon()
        {
            _projectilesLeftInMagazine = _magazineCapacity;
        }

        public override void HandleReloadPerformed()
        {
            if(_isReloading)
                return;

            Timing.RunCoroutine(ReloadCoroutine());
        }

        public override void HandleFirePerformed()
        {
            if(_isReloading)
                return;

            _projectilesLeftInMagazine--;
            FireProjectile();
            OnMagazineStatusChanged?.Invoke();
            
            if (_projectilesLeftInMagazine <= 0)
            {
                Timing.RunCoroutine(ReloadCoroutine());
            }
        }
        
        public MagazineStatus GetStatus()
        {
            MagazineStatus magazineStatus = new ()
            {
                IsReloading = _isReloading,
                ProjectilesLeftInMagazine = _projectilesLeftInMagazine,
                MagazineCapacity = _magazineCapacity,
                ReloadStatus = _currentReloadTime / _reloadTime,
            };
            return magazineStatus;
        }

        private IEnumerator<float> ReloadCoroutine()
        {
            _isReloading = true;
            float t = Time.time;
            while (_currentReloadTime < _reloadTime)
            {
                _currentReloadTime = Time.time - t;
                OnMagazineStatusChanged?.Invoke();
                yield return Timing.WaitForOneFrame;
            }
            
            _currentReloadTime = 0f;
            _isReloading = false;
            _projectilesLeftInMagazine = _magazineCapacity;
            OnMagazineStatusChanged?.Invoke();
        }

        private void FireProjectile()
        {
            if (_fireSound != null)
            {
                _audioSource.PlayOneShot(_fireSound);
            }
            Projectile spawnedProjectile = _projectilesSpawner.SpawnProjectile(_projectileSpawnPoint.position, _projectileSpawnPoint.rotation);
            spawnedProjectile.SetProjectileProperties(_projectileSettings);
        }


        [Serializable]
        public class ProjectileSettings
        {
            [SerializeField]
            private float projectileSpeed;
            [SerializeField]
            private int projectileDamage;
            [SerializeField]
            private GameObject hitFx;
            [SerializeField]
            private AudioClip hitSfx;


            public float Speed => projectileSpeed;
            public int Damage => projectileDamage;
            public GameObject HitFx => hitFx;
            public AudioClip HitSfx => hitSfx;
        } 
    }
}


