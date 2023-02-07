using UnityEngine;

namespace IsoShooter.Player
{
    public class PlayerInitializer : MonoBehaviour
    {
        [SerializeField]
        private PlayerMovement playerMovement;
        [SerializeField]
        private PlayerWeaponController weaponController;
        [SerializeField]
        private PlayerSettings playerSettings;
        
        private void Awake()
        {
            ICharacterInput input = GetComponent<ICharacterInput>();
            playerMovement.Initialize(input, playerSettings);
            weaponController.Initialize(input, playerSettings);
        }

        private void OnDestroy()
        {
            weaponController.CleanUp();
        }
    }
}


