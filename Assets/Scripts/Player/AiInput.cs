using System;
using IsoShooter.Player;
using UnityEngine;

public class AiInput : MonoBehaviour, ICharacterInput
{
    public event Action OnReloadPerformed;
    public event Action OnFireCanceled;
    public event Action OnFirePerformed;
    public event Action OnAbilityPerformed;
    public Vector3 MovementInput { get; }
    public Vector3 AimDestination { get; }

    
    public void Update()
    {
        //Handle AI input;
    }
}
