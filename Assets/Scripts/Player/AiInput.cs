using System;
using UnityEngine;

namespace IsoShooter.Player
{
    public class AiInput : MonoBehaviour, IWeaponInput, IMovementInput, IInteractionsInput, IAbilitiesInput
    {
        public event Action OnReloadPerformed;
        public event Action OnFireCanceled;
        public event Action OnFirePerformed;
        public event Action OnAbilityPerformed;
        public event Action OnInteractPerformed;
    
        public Vector3 MovementInput { get; }
        public Vector3 AimDestination { get; }

    
        public void Update()
        {
            //Handle AI input;
        }
    }
}


