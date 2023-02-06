using UnityEngine;

namespace IsoShooter.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody _rigidBody;
        [SerializeField]
        private Transform _character;
        [SerializeField]
        private Transform _gun;
        [SerializeField]
        private PlayerSettings _playerSettings;

        private ICharacterInput playerInput;


        private void Awake()
        {
            playerInput = GetComponent<ICharacterInput>();
        }

        private void FixedUpdate()
        {
            HandleMovement();
        }

        private void HandleMovement()
        {
            MovePlayer();
            RotatePlayer();
            RotateGun();
        }

        private void MovePlayer()
        {
            _rigidBody.MovePosition(_character.position + playerInput.MovementInput * _playerSettings.MovementSpeed * Time.fixedDeltaTime);
        }

        private void RotatePlayer()
        {
            Vector3 rotDestination = new Vector3(playerInput.AimDestination.x, _character.position.y, playerInput.AimDestination.z) - transform.position;
            Quaternion playerRotation = Quaternion.LookRotation(rotDestination, Vector3.up);
            _rigidBody.MoveRotation(playerRotation);
        }

        private void RotateGun()
        {
            Vector3 gunAimPosition = playerInput.AimDestination;
            if (gunAimPosition.y < _playerSettings.MinAimingHeight)
            {
                gunAimPosition = new Vector3(gunAimPosition.x, _playerSettings.MinAimingHeight, gunAimPosition.z);
            }
            
            float distanceToAim = Vector3.Distance(_character.position, gunAimPosition);
            if(distanceToAim < _playerSettings.MinGunAimingDistance)
                return;
            
            _gun.LookAt(playerInput.AimDestination);
        }
    }
}
