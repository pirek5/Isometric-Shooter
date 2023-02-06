using UnityEngine;

namespace IsoShooter.Player
{
    public interface ICharacterInput
    {
        public Vector3 MovementInput { get; }
        public Vector3 AimDestination { get; }
    }
}
