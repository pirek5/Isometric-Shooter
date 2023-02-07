using IsoShooter.Weapons;
using UnityEngine;

namespace IsoShooter.Player
{
    public class PlayerInitializer : MonoBehaviour, ISceneInjectee
    {
        [SerializeField]
        private PlayerMovement playerMovement;
        [SerializeField]
        private PlayerWeaponController weaponController;
        [SerializeField]
        private PlayerSettings playerSettings;
        [Inject]
        private WeaponsDatabase weaponsDatabase;
        
        private void OnDestroy()
        {
            weaponController.CleanUp();
        }

        public void OnInjected()
        {
            ICharacterInput input = GetComponent<ICharacterInput>();
            playerMovement.Initialize(input, playerSettings);
            weaponController.Initialize(input, playerSettings, weaponsDatabase);
        }
    }
}


