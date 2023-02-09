using IsoShooter.Player;
using IsoShooter.Weapons;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace IsoShooter.Ui
{
    public class TopBarReloadableWeaponSection : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _weaponNameText;
        [SerializeField]
        private GameObject _reloadObject;
        [SerializeField]
        private Image _reloadProgress;
        [SerializeField]
        private TextMeshProUGUI _magazineText;
        
        private PlayerWeaponController _currentWeaponController;
        private IReloadableWeapon _currentWeapon;
        
        
        public void SetObjectToShow(PlayerWeaponController playerWeaponController)
        {
            CleanUp();
            
            if (playerWeaponController == null)
            {
                RefreshVisibility();
                return;
            }

            _currentWeaponController = playerWeaponController;
            _currentWeaponController.OnWeaponChanged += HandleWeaponChanged;
            HandleWeaponChanged(_currentWeaponController.CurrentWeapon);
            HandleMagazineStatusChanged();
        }

        private void RefreshVisibility()
        {
            gameObject.SetActive(_currentWeapon != null && _currentWeaponController != null);
        }
        
        private void CleanUp()
        {
            CleanUpController();
            CleanUpWeapon();
        }
        
        private void CleanUpController()
        {
            if (_currentWeaponController == null) 
                return;
            
            _currentWeaponController.OnWeaponChanged -= HandleWeaponChanged;
            _currentWeaponController = null;
        }

        private void CleanUpWeapon()
        {
            if (_currentWeapon == null) 
                return;
            
            _currentWeapon.OnMagazineStatusChanged -= HandleMagazineStatusChanged;
            _currentWeapon = null;
        }

        private void HandleWeaponChanged(Weapon weapon)
        {
            CleanUpWeapon();
            if (weapon is IReloadableWeapon reloadableWeapon)
            {
                _currentWeapon = reloadableWeapon;
                _currentWeapon.OnMagazineStatusChanged += HandleMagazineStatusChanged;
                _weaponNameText.SetText(weapon.WeaponDisplayName);
            }
            else
            {
                _currentWeapon = null;
            }
            RefreshVisibility();
        }

        private void HandleMagazineStatusChanged()
        {
            MagazineStatus magazineStatus = _currentWeapon.GetStatus();
            
            _reloadObject.SetActive(magazineStatus.IsReloading);
            _reloadProgress.fillAmount = magazineStatus.ReloadStatus;
            _magazineText.SetText($"{magazineStatus.ProjectilesLeftInMagazine} / {magazineStatus.MagazineCapacity}");
        }
    }
}


