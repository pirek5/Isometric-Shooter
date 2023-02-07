using IsoShooter.Weapons;
using UnityEngine;

namespace IsoShooter.Player
{
    public class PlayerWeaponController : MonoBehaviour
    {
        [SerializeField]
        private string _selectedWeapon;
        [SerializeField]
        private Transform _weaponSlot;
        
        private PlayerSettings _playerSettings;
        private WeaponsDatabase _weaponsDatabase;
        private ICharacterInput _playerInput;
        private Weapon _currentWeapon;


        public void Initialize(ICharacterInput input, PlayerSettings playerSettings, WeaponsDatabase weaponsDatabase)
        {
            _playerSettings = playerSettings;
            _weaponsDatabase = weaponsDatabase;
            _playerInput = input;
            CreateSelectedWeapon();
            
            _playerInput.OnFirePerformed += HandleFirePerformed;
            _playerInput.OnFireCanceled += HandleFireCanceled;
            _playerInput.OnReloadPerformed += HandleReloadPerformed;
        }

        public void CleanUp()
        {
            _playerInput.OnFirePerformed -= HandleFirePerformed;
            _playerInput.OnFireCanceled -= HandleFireCanceled;
            _playerInput.OnReloadPerformed -= HandleReloadPerformed;
        }

        private void Update()
        {
            RotateGun();
        }

        private void CreateSelectedWeapon()
        {
            Weapon weaponPrefab = _weaponsDatabase.GetWeaponPrefab(_selectedWeapon);
            _currentWeapon = Instantiate(weaponPrefab, _weaponSlot);
            _currentWeapon.InitializeWeapon();
        }

        private void HandleFirePerformed()
        {
            if(_currentWeapon == null)
                return;
            
            _currentWeapon.HandleFirePerformed();
        }

        private void HandleFireCanceled()
        {
            if(_currentWeapon == null)
                return;
            
            _currentWeapon.HandleFireCanceled();
        }

        private void HandleReloadPerformed()
        {
            if(_currentWeapon == null)
                return;
            
            _currentWeapon.HandleReloadPerformed();
        }
        
        private void RotateGun()
        {
            if(_currentWeapon == null)
                return;
            
            Vector3 gunAimPosition = _playerInput.AimDestination;
            if (gunAimPosition.y < _playerSettings.MinAimingHeight)
            {
                gunAimPosition = new Vector3(gunAimPosition.x, _playerSettings.MinAimingHeight, gunAimPosition.z);
            }
                
            float distanceToAim = Vector3.Distance(_weaponSlot.position, gunAimPosition);
            if(distanceToAim < _playerSettings.MinGunAimingDistance)
                return;
                
            _currentWeapon.transform.LookAt(_playerInput.AimDestination);
        }
    }
}


