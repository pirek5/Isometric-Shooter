using UnityEngine;

namespace IsoShooter.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody _rigidBody;

        private PlayerSettings _playerSettings;
        private IMovementInput _movementInput;


        public void Initialize(IMovementInput weaponInput, PlayerSettings playerSettings)
        {
            _movementInput = weaponInput;
            _playerSettings = playerSettings;
        }

        private void FixedUpdate()
        {
            HandleMovement();
        }

        private void HandleMovement()
        {
            MovePlayer();
            RotatePlayer();
        }

        private void MovePlayer()
        {
            _rigidBody.MovePosition(transform.position + _movementInput.MovementInput * _playerSettings.MovementSpeed * Time.fixedDeltaTime);
        }

        private void RotatePlayer()
        {
            Vector3 rotDestination = new Vector3(_movementInput.AimDestination.x, transform.position.y, _movementInput.AimDestination.z) - transform.position;
            Quaternion playerRotation = Quaternion.LookRotation(rotDestination, Vector3.up);
            _rigidBody.MoveRotation(playerRotation);
        }
    }
}
