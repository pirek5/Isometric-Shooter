using System;
using IsoShooter.Weapons;
using UnityEngine;

namespace IsoShooter.Player
{
    public class PlayerWeaponController : MonoBehaviour
    {
        public event Action<Weapon> OnWeaponChanged;
        
        [SerializeField]
        private Transform _weaponSlot;
        
        private PlayerSettings _playerSettings;
        private WeaponsDatabase _weaponsDatabase;
        private IWeaponInput _weaponInput;
        
        
        public Weapon CurrentWeapon { get; private set; }


        public void Initialize(IWeaponInput input, PlayerSettings playerSettings, WeaponsDatabase weaponsDatabase)
        {
            _playerSettings = playerSettings;
            _weaponsDatabase = weaponsDatabase;
            _weaponInput = input;
            CreateSelectedWeapon();
            
            _weaponInput.OnFirePerformed += HandleFirePerformed;
            _weaponInput.OnFireCanceled += HandleFireCanceled;
            _weaponInput.OnReloadPerformed += HandleReloadPerformed;
        }

        public void CleanUp()
        {
            _weaponInput.OnFirePerformed -= HandleFirePerformed;
            _weaponInput.OnFireCanceled -= HandleFireCanceled;
            _weaponInput.OnReloadPerformed -= HandleReloadPerformed;
        }

        private void Update()
        {
            RotateGun();
        }

        private void CreateSelectedWeapon()
        {
            Weapon weaponPrefab = _weaponsDatabase.GetWeaponPrefab(_playerSettings.StartingWeapon);
            CurrentWeapon = Instantiate(weaponPrefab, _weaponSlot);
            CurrentWeapon.InitializeWeapon();
            OnWeaponChanged?.Invoke(CurrentWeapon);
        }

        private void HandleFirePerformed()
        {
            if(CurrentWeapon == null)
                return;
            
            CurrentWeapon.HandleFirePerformed();
        }

        private void HandleFireCanceled()
        {
            if(CurrentWeapon == null)
                return;
            
            CurrentWeapon.HandleFireCanceled();
        }

        private void HandleReloadPerformed()
        {
            if(CurrentWeapon == null)
                return;
            
            CurrentWeapon.HandleReloadPerformed();
        }
        
        private void RotateGun()
        {
            if(CurrentWeapon == null)
                return;
            
            Vector3 gunAimPosition = _weaponInput.AimDestination;

            float distanceToAim = Vector3.Distance(_weaponSlot.position, gunAimPosition);
            if(distanceToAim < _playerSettings.MinGunAimingDistance)
                return;
                
            CurrentWeapon.transform.LookAt(_weaponInput.AimDestination);
        }
    }
}


