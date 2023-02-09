using UnityEngine;
using UnityEngine.Serialization;

namespace IsoShooter.Abilities
{
    [System.Serializable]
    public abstract class Ability : ScriptableObject
    {
        [SerializeField]
        private float _abilityCooldown;
        [SerializeField]
        private string _abilityDisplayName;
        [SerializeField]
        private AudioClip _abilityPerformSound;

        public float AbilityCooldown => _abilityCooldown;
        public string AbilityDisplayName => _abilityDisplayName;
        public AudioClip AbilityPerformSound => _abilityPerformSound;


        public abstract void Perform(GameObject performer, Vector3 abilityTargetPosition);
    }
}


