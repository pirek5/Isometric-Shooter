using UnityEngine;
using UnityEngine.InputSystem;

namespace IsoShooter.Player
{
    public class PlayerInput : MonoBehaviour, ISceneInjectee, ICharacterInput
    {
        [SerializeField]
        private Camera _currentCamera;
        [SerializeField]
        private Transform _playerTransform;
        
        [Inject]
        private InputManager _inputManager;

        private Matrix4x4 _toIsometricViewMatrix = Matrix4x4.Rotate(Quaternion.Euler(0f, 45f, 0f));
        private InputAction _movementAction;

        
        public Vector3 MovementInput { get; private set; }
        public Vector3 AimDestination { get; private set; }
        

        public void OnInjected()
        {
            _movementAction = _inputManager.InputActions.Player.Movement;
        }

        private void Update()
        {
            GatherMovementInput();
            TiltMovementInputToIsometricView();
            GatherAimInput();
        }
        
        private void Reset()
        {
            _playerTransform = GetComponent<Transform>();
        }
    
        private void GatherAimInput()
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Ray ray = _currentCamera.ScreenPointToRay(mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hit))
            {
                AimDestination = hit.point;
            }
        }

        private void GatherMovementInput()
        {
            Vector2 rawInput = _movementAction.ReadValue<Vector2>();
            MovementInput = new Vector3(rawInput.x, _playerTransform.position.y, rawInput.y);
        }

        private void TiltMovementInputToIsometricView()
        {
            MovementInput = _toIsometricViewMatrix.MultiplyPoint3x4(MovementInput);
        }
    }
}


