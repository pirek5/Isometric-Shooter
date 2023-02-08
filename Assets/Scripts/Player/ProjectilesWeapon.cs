using System.Collections.Generic;
using IsoShooter.Weapons;
using MEC;
using UnityEngine;

public class ProjectilesWeapon : Weapon
{
    [SerializeField]
    private ProjectilesSpawner projectilesSpawner;
    [SerializeField]
    private int magazineCapacity;
    [SerializeField]
    private float reloadTime;
    [Space]
    [SerializeField]
    private ProjectileSettings projectileSettings;
    [Space]
    [SerializeField]
    private Transform projectileSpawnPoint;
    
    private bool isReloading;
    private int projectilesLeftInMagazine;
    
    
    public override void InitializeWeapon()
    {
        projectilesLeftInMagazine = magazineCapacity;
    }

    public override void HandleReloadPerformed()
    {
        if(isReloading)
            return;

        Timing.RunCoroutine(ReloadCoroutine());
    }

    public override void HandleFirePerformed()
    {
        if(isReloading)
            return;

        if (projectilesLeftInMagazine <= 0)
        {
            Timing.RunCoroutine(ReloadCoroutine());
            return;
        }

        projectilesLeftInMagazine--;
        FireProjectile();
    }

    private IEnumerator<float> ReloadCoroutine()
    {
        isReloading = true;
        yield return Timing.WaitForSeconds(reloadTime);
        isReloading = false;
        projectilesLeftInMagazine = magazineCapacity;
    }

    private void FireProjectile()
    {
        Projectile spawnedProjectile = projectilesSpawner.SpawnProjectile(projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        spawnedProjectile.SetProjectileProperties(projectileSettings);
    }
    
    
    [System.Serializable]
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
