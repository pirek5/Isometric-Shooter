using System.Collections.Generic;
using MEC;
using UnityEngine;

namespace IsoShooter.Player
{
    public class PlayerAbilitiesController : MonoBehaviour
    {
        private ICharacterInput _playerInput;
        private PlayerSettings _playerSettings;

        private Ability _currentSelectedAbility;
        private bool _isOnCooldown;

        
        public void Initialize(ICharacterInput characterInput, PlayerSettings playerSettings)
        {
            _playerInput = characterInput;
            _playerSettings = playerSettings;

            TrySelectAbility();

            _playerInput.OnAbilityPerformed += TryPerformAbility;
        }

        public void CleanUp()
        {
            _playerInput.OnAbilityPerformed -= TryPerformAbility;
        }

        private void TrySelectAbility()
        {
            if(_playerSettings.StartingAbility == null)
                return;

            _currentSelectedAbility = Instantiate(_playerSettings.StartingAbility);
        }

        private void TryPerformAbility()
        {
            if(_currentSelectedAbility == null || _isOnCooldown)
                return;
            
            _currentSelectedAbility.Perform(gameObject, _playerInput.AimDestination);
            Timing.RunCoroutine(CooldownCoroutine());
        }

        private IEnumerator<float> CooldownCoroutine()
        {
            _isOnCooldown = true;
            yield return Timing.WaitForSeconds(_currentSelectedAbility.AbilityCooldown);
            _isOnCooldown = false;
        }
    }
}


