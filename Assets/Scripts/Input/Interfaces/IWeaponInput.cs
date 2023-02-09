using System;
using UnityEngine;

namespace IsoShooter
{
    public interface IWeaponInput
    {
        public event Action OnReloadPerformed;
        public event Action OnFireCanceled;
        public event Action OnFirePerformed;

        public Vector3 AimDestination { get; }
    }
}
