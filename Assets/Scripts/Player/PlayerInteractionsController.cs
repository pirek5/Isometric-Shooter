using IsoShooter.Interactions;
using IsoShooter.Player;
using UnityEngine;

public class PlayerInteractionsController : MonoBehaviour
{
    [SerializeField]
    private LayerMask _interactableLayer;
    [SerializeField]
    private float _maxRange;
    [SerializeField]
    private Transform interactionsTransform;

    private IInteractable _currentInteractable;
    private ICharacterInput _characterInput;

    public void Initialize(ICharacterInput characterInput)
    {
        _characterInput = characterInput;
        _characterInput.OnInteractPerformed += TryInteract;
    }

    public void CleanUp()
    {
        _characterInput.OnInteractPerformed -= TryInteract;
    }
    
    public void Update()
    {
        HandleInteractableObjects();
    }

    private void HandleInteractableObjects()
    {
        if (Physics.Raycast(interactionsTransform.position, interactionsTransform.forward, out RaycastHit hit, _maxRange, _interactableLayer))
        {
            IInteractable newInteractable = hit.collider.GetComponent<IInteractable>();
            
            if(_currentInteractable == newInteractable)
                return;
            
            _currentInteractable?.OnExit();
            _currentInteractable = newInteractable;
            _currentInteractable?.OnEnter();
        }
        else
        {
            _currentInteractable?.OnExit();
            _currentInteractable = null;
        }
    }

    private void TryInteract()
    {
        _currentInteractable?.Interact();
    }
}
