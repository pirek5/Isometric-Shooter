using IsoShooter.Weapons;
using UnityEngine;

namespace IsoShooter.Player
{
    public class PlayerController : MonoBehaviour, ISceneInjectee
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
            InitializeAssignedControllers();
            AttachEvents();
        }
        
        private void OnDestroy()
        {
            DetachEvents();
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

        private void AttachEvents()
        {
            if (_healthController != null)
            {
                _healthController.OnHealthDepleted += DestroyPlayer;
            }
        }

        private void DetachEvents()
        {
            if (_healthController != null)
            {
                _healthController.OnHealthDepleted -= DestroyPlayer;
            }
        }

        private void InitializeAssignedControllers()
        {
            if (_playerMovement != null)
            {
                IMovementInput movementInput = GetComponentInChildren<IMovementInput>();
                _playerMovement.Initialize(movementInput, _playerSettings);
            }

            if (_weaponController != null)
            {
                IWeaponInput weaponInput = GetComponentInChildren<IWeaponInput>();
                _weaponController.Initialize(weaponInput, _playerSettings, _weaponsDatabase);
            }

            if (_abilityController != null)
            {
                IAbilitiesInput abilitiesInput = GetComponentInChildren<IAbilitiesInput>();
                _abilityController.Initialize(abilitiesInput, _playerSettings);
            }

            if (_interactionsController != null)
            {
                IInteractionsInput interactionsInput = GetComponentInChildren<IInteractionsInput>();
                _interactionsController.Initialize(interactionsInput);
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
        
        private void DestroyPlayer()
        {
            Destroy(gameObject);
        }
    }
}


