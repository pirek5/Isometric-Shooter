using UnityEngine;

namespace IsoShooter
{
    public interface IMovementInput
    {
        public Vector3 MovementInput { get; }
        public Vector3 AimDestination { get; }
    }
}


