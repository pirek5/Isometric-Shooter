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
        private PlayerSettings _playerSettings;
        [Inject]
        private WeaponsDatabase _weaponsDatabase;
        
        
        public void OnInjected()
        {
            ICharacterInput input = GetComponent<ICharacterInput>();
            _playerMovement.Initialize(input, _playerSettings);
            _weaponController.Initialize(input, _playerSettings, _weaponsDatabase);
        }
        
        private void OnDestroy()
        {
            _weaponController.CleanUp();
        }
    }
}


