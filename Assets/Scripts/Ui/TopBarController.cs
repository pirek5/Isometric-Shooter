using IsoShooter.Player;
using UnityEngine;

namespace IsoShooter.Ui
{
    public class TopBarController : MonoBehaviour
    {
        [SerializeField]
        private TopBarReloadableWeaponSection _topBarWeaponSection;
        [SerializeField]
        private TopBarAbilitySection _abilitySection;
        [Space]
        [SerializeField]
        private GameObject _objectToShowOnTopBar;


        public void Start()
        {
            Initialize();
        }
        
        private void Initialize()
        {
            if(_objectToShowOnTopBar == null)
                return;

            PlayerWeaponController weaponController = _objectToShowOnTopBar.GetComponentInChildren<PlayerWeaponController>();
            _topBarWeaponSection.SetObjectToShow(weaponController);

            PlayerAbilitiesController abilitiesController = _objectToShowOnTopBar.GetComponentInChildren<PlayerAbilitiesController>();
            _abilitySection.SetObjectToShow(abilitiesController);
        }
    }
}


