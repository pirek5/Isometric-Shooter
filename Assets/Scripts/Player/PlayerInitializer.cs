using IsoShooter.Weapons;
using UnityEngine;

namespace IsoShooter.Player
{
    public class PlayerInitializer : MonoBehaviour, ISceneInjectee
    {
        [SerializeField]
        private PlayerMovement _playerMovement;
        [SerializeField]
        private PlayerWeaponController _weaponController;
        [SerializeField]
        private PlayerAbilitiesController _abilityController;
        [SerializeField]
        private HealthController _healthController;
        [SerializeField]
        private PlayerInteractionsController _interactionsController;
        [SerializeField]
        private PlayerSettings _playerSettings;
        [Inject]
        private WeaponsDatabase _weaponsDatabase;


        public void OnInjected()
        {
            ICharacterInput input = GetComponentInChildren<ICharacterInput>();

            InitializeAssignedControllers(input);
        }
        
        private void OnDestroy()
        {
            CleanUpControllers();
        }
        
        private void Reset()
        {
            _playerMovement = GetComponentInChildren<PlayerMovement>();
            _weaponController = GetComponentInChildren<PlayerWeaponController>();
            _abilityController = GetComponentInChildren<PlayerAbilitiesController>();
            _healthController = GetComponentInChildren<HealthController>();
            _interactionsController = GetComponentInChildren<PlayerInteractionsController>();
        }

        private void InitializeAssignedControllers(ICharacterInput input)
        {
            if (_playerMovement != null)
            {
                _playerMovement.Initialize(input, _playerSettings);
            }

            if (_weaponController != null)
            {
                _weaponController.Initialize(input, _playerSettings, _weaponsDatabase);
            }

            if (_abilityController != null)
            {
                _abilityController.Initialize(input, _playerSettings);
            }

            if (_interactionsController != null)
            {
                _interactionsController.Initialize(input);
            }

            if (_healthController != null)
            {
                _healthController.Initialize(_playerSettings.Health);
            }
        }
        
        private void CleanUpControllers()
        {
            if (_weaponController != null)
            {
                _weaponController.CleanUp();
            }

            if (_abilityController != null)
            {
                _abilityController.CleanUp();
            }

            if (_interactionsController != null)
            {
                _interactionsController.CleanUp();
            }
        }
    }
}


