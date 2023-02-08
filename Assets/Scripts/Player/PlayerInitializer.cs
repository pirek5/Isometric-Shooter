using System.Collections.Generic;
using System.Linq;
using IsoShooter.Weapons;
using UnityEngine;
using UnityEngine.Serialization;

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
        private PlayerSettings _playerSettings;
        [Inject]
        private WeaponsDatabase _weaponsDatabase;


        public void OnInjected()
        {
            ICharacterInput input = GetComponentInChildren<ICharacterInput>();

            _playerMovement.Initialize(input, _playerSettings);
            _weaponController.Initialize(input, _playerSettings, _weaponsDatabase);
            _abilityController.Initialize(input, _playerSettings);
        }
        
        private void OnDestroy()
        {
            _weaponController.CleanUp();
            _abilityController.CleanUp();
        }
    }
}


