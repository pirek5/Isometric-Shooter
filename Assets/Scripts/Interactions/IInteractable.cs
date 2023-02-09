using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace IsoShooter.Interactions
{
    public interface IInteractable
    {
        public void OnEnter();
        public void OnExit();
        public void Interact();
    }
}


