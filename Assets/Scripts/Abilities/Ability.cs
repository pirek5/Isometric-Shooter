using System;
using UnityEngine;

[System.Serializable]
public abstract class Ability : ScriptableObject
{
    [SerializeField]
    private float abilityCooldown;
    [SerializeField]
    private string abilityDisplayName;


    public float AbilityCooldown => abilityCooldown;
    public string AbilityDisplayName => abilityDisplayName;


    public abstract void Perform(GameObject performer, Vector3 abilityTargetPosition);
}
