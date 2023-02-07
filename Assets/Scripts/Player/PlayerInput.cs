using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace IsoShooter.Player
{
    public class PlayerInput : MonoBehaviour, ISceneInjectee, ICharacterInput
    {
        public event Action OnReloadPerformed;
        public event Action OnFireCanceled;
        public event Action OnFirePerformed;
        
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
            _inputManager.InputActions.Player.Fire.performed += NotifyAboutFirePerformed;
            _inputManager.InputActions.Player.Fire.canceled += NotifyAboutFireCanceled;
            _inputManager.InputActions.Player.Reaload.performed += NotifyAboutReloadPerformed;
        }

        private void Update()
        {
            GatherMovementInput();
            TiltMovementInputToIsometricView();
            GatherAimInput();
        }
        
        private void OnDestroy()
        {
            if(_inputManager == null)
                return;

            _inputManager.InputActions.Player.Fire.performed -= NotifyAboutFirePerformed;
            _inputManager.InputActions.Player.Fire.canceled -= NotifyAboutFireCanceled;
            _inputManager.InputActions.Player.Reaload.performed -= NotifyAboutReloadPerformed;
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
            MovementInput = new Vector3(rawInput.x, 0f, rawInput.y);
        }

        private void TiltMovementInputToIsometricView()
        {
            MovementInput = _toIsometricViewMatrix.MultiplyPoint3x4(MovementInput);
        }

        private void NotifyAboutFirePerformed(InputAction.CallbackContext callbackContext)
        {
            OnFirePerformed?.Invoke();
        }

        private void NotifyAboutFireCanceled(InputAction.CallbackContext callbackContext)
        {
            OnFireCanceled?.Invoke();
        }
        
        private void NotifyAboutReloadPerformed(InputAction.CallbackContext callbackContext)
        {
            OnReloadPerformed?.Invoke();
        }
    }
}


