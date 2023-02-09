using System;
using System.Collections.Generic;
using IsoShooter.Abilities;
using MEC;
using UnityEngine;

namespace IsoShooter.Player
{
    public class PlayerAbilitiesController : MonoBehaviour
    {
        public event Action<Ability> OnAbilityChanged;
        public event Action OnAbilityStatusChanged;
        
        [SerializeField]
        private AudioSource _audioSource;
        
        private IAbilitiesInput _abilitiesInput;
        private PlayerSettings _playerSettings;
        
        private bool _isOnCooldown;
        private float _currentCooldown;
        
        
        public Ability CurrentAbility { get; private set; }

        
        public void Initialize(IAbilitiesInput abilitiesInput, PlayerSettings playerSettings)
        {
            _abilitiesInput = abilitiesInput;
            _playerSettings = playerSettings;

            TrySelectAbility();

            _abilitiesInput.OnAbilityPerformed += TryPerformAbility;
        }

        public void CleanUp()
        {
            _abilitiesInput.OnAbilityPerformed -= TryPerformAbility;
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
            
            CurrentAbility.Perform(gameObject, _abilitiesInput.AimDestination);
            Timing.RunCoroutine(CooldownCoroutine());
            _audioSource.PlayOneShot(CurrentAbility.AbilityPerformSound);
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


