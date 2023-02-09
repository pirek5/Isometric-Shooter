using System;
using TMPro;
using UnityEngine;

namespace IsoShooter.Interactions
{
    public class ActivableObject : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private TextMeshProUGUI floatingText;
        [SerializeField]
        private Renderer objectRenderer;
        [Space]
        [SerializeField]
        private string _standardText;
        [SerializeField]
        private string _onEnterText;
        [SerializeField]
        private string _activatedText;
        [SerializeField]
        private Color activatedColor;
        
        
        private bool wasActivated;


        private void Awake()
        {
            floatingText.SetText(_standardText);
        }

        public void OnEnter()
        {
            if(wasActivated)
                return;
        
            floatingText.SetText(_onEnterText);
        }

        public void OnExit()
        {
            if(wasActivated)
                return;
        
            floatingText.SetText(_standardText);
        }

        public void Interact()
        {
            if(wasActivated)
                return;
        
            wasActivated = true;
            objectRenderer.material.color = activatedColor;
            floatingText.SetText(_activatedText);
        }
    }
}


