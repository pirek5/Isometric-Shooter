using TMPro;
using UnityEngine;

namespace IsoShooter.Interactions
{
    public class ActivableObject : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private TextMeshProUGUI _floatingText;
        [SerializeField]
        private Renderer _objectRenderer;
        [Space]
        [SerializeField]
        private string _standardText;
        [SerializeField]
        private string _onEnterText;
        [SerializeField]
        private string _activatedText;
        [SerializeField]
        private Color _activatedColor;
        
        private bool _wasActivated;


        private void Awake()
        {
            _floatingText.SetText(_standardText);
        }

        public void OnEnter()
        {
            if(_wasActivated)
                return;
        
            _floatingText.SetText(_onEnterText);
        }

        public void OnExit()
        {
            if(_wasActivated)
                return;
        
            _floatingText.SetText(_standardText);
        }

        public void Interact()
        {
            if(_wasActivated)
                return;
        
            _wasActivated = true;
            _objectRenderer.material.color = _activatedColor;
            _floatingText.SetText(_activatedText);
        }
    }
}


