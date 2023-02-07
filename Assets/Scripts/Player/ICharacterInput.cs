using System;
using UnityEngine;

namespace IsoShooter.Player
{
    public interface ICharacterInput
    {
        public event Action OnReloadPerformed;
        public event Action OnFireCanceled;
        public event Action OnFirePerformed;
        public Vector3 MovementInput { get; }
        public Vector3 AimDestination { get; }
    }
}
