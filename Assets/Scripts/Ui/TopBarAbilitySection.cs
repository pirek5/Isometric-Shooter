using IsoShooter.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace IsoShooter.Ui
{
    public class TopBarAbilitySection : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _abilityNameText;
        [SerializeField]
        private Image _abilityReadyProgress;

        private PlayerAbilitiesController _currentAbilitiesController;
        private Ability _currentAbility;
        
        
        public void SetObjectToShow(PlayerAbilitiesController abilitiesController)
        {
            CleanUp();
            
            if (abilitiesController == null)
            {
                RefreshVisibility();
                return;
            }

            _currentAbilitiesController = abilitiesController;
            _currentAbilitiesController.OnAbilityChanged += HandleAbilityChanged;
            _currentAbilitiesController.OnAbilityStatusChanged += HandleAbilityStatusChanged;
            HandleAbilityChanged(_currentAbilitiesController.CurrentAbility);
            HandleAbilityStatusChanged();
        }

        private void RefreshVisibility()
        {
            gameObject.SetActive(_currentAbilitiesController != null && _currentAbility != null);
        }

        private void CleanUp()
        {
            if (_currentAbilitiesController == null) 
                return;
            
            _currentAbilitiesController.OnAbilityChanged -= HandleAbilityChanged;
            _currentAbilitiesController.OnAbilityStatusChanged -= HandleAbilityStatusChanged;
            _currentAbilitiesController = null;
            _currentAbility = null;
        }

        private void HandleAbilityChanged(Ability ability)
        {
            _currentAbility = ability;
            if (_currentAbility != null)
            {
                _abilityNameText.SetText(ability.AbilityDisplayName);
            }
            
            RefreshVisibility();
        }

        private void HandleAbilityStatusChanged()
        {
            AbilityStatus abilityStatus = _currentAbilitiesController.GetAbilityStatus();
            _abilityReadyProgress.fillAmount = abilityStatus.IsOnCooldown ? abilityStatus.AbilityReadyStatus : 1f;
        }
    }
}


