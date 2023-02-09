using System;
using System.Collections.Generic;
using MEC;
using UnityEngine;

namespace IsoShooter.Player
{
    public class PlayerAbilitiesController : MonoBehaviour
    {
        public event Action<Ability> OnAbilityChanged;
        public event Action OnAbilityStatusChanged; 
        
        private ICharacterInput _playerInput;
        private PlayerSettings _playerSettings;
        
        private bool _isOnCooldown;
        private float _currentCooldown;
        
        
        public Ability CurrentAbility { get; private set; }

        
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
        
        public AbilityStatus GetAbilityStatus()
        {
            AbilityStatus abilityStatus = new ()
            {
                AbilityReadyStatus = _currentCooldown / CurrentAbility.AbilityCooldown,
                IsOnCooldown = _isOnCooldown,
            };
            return abilityStatus;
        }

        private void TrySelectAbility()
        {
            if(_playerSettings.StartingAbility == null)
                return;

            CurrentAbility = Instantiate(_playerSettings.StartingAbility);
            OnAbilityChanged?.Invoke(CurrentAbility);
        }

        private void TryPerformAbility()
        {
            if(CurrentAbility == null || _isOnCooldown)
                return;
            
            CurrentAbility.Perform(gameObject, _playerInput.AimDestination);
            Timing.RunCoroutine(CooldownCoroutine());
        }

        private IEnumerator<float> CooldownCoroutine()
        {
            _isOnCooldown = true;
            float t = Time.time;
            while (_currentCooldown < CurrentAbility.AbilityCooldown)
            {
                _currentCooldown = Time.time - t;
                OnAbilityStatusChanged?.Invoke();
                yield return Timing.WaitForOneFrame;
            }

            _currentCooldown = 0f;
            _isOnCooldown = false;
            OnAbilityStatusChanged?.Invoke();
        }
    }
    
    public struct AbilityStatus
    {
        /// <summary>
        /// value between 0 and 1, 0 means cooldown just started, 1 ability is ready to use 
        /// </summary>
        public float AbilityReadyStatus;
        public bool IsOnCooldown;
    }
}


