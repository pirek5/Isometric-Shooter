using UnityEngine;

namespace IsoShooter.Abilities
{
    [CreateAssetMenu(menuName = "IsometricShooter/Abilities/Teleport")]
    [System.Serializable]
    public class TeleportAbility : Ability
    {
        public override void Perform(GameObject performer, Vector3 abilityTargetPosition)
        {
            performer.transform.position = abilityTargetPosition;
        }
    }

}

