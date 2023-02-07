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
        private PlayerSettings _playerSettings;

        private ICharacterInput _playerInput;


        public void Initialize(ICharacterInput characterInput, PlayerSettings playerSettings)
        {
            _playerInput = characterInput;
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
            _rigidBody.MovePosition(_character.position + _playerInput.MovementInput * _playerSettings.MovementSpeed * Time.fixedDeltaTime);
        }

        private void RotatePlayer()
        {
            Vector3 rotDestination = new Vector3(_playerInput.AimDestination.x, _character.position.y, _playerInput.AimDestination.z) - transform.position;
            Quaternion playerRotation = Quaternion.LookRotation(rotDestination, Vector3.up);
            _rigidBody.MoveRotation(playerRotation);
        }
    }
}
