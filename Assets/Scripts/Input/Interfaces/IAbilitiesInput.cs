using System;
using UnityEngine;

namespace IsoShooter
{
    public interface IAbilitiesInput
    {
        public event Action OnAbilityPerformed;
        public Vector3 AimDestination { get; }
    }
}


