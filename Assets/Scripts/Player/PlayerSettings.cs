using UnityEngine;

namespace IsoShooter.Player
{
    [CreateAssetMenu(menuName = "IsometricShooter/PlayerSettings")]
    public class PlayerSettings : ScriptableObject
    {
        [SerializeField]
        private float _movementSpeed;
        [SerializeField]
        private float _minGunAimingDistance;
        [SerializeField]
        private float _minAimingHeight;
        [WeaponId]
        [SerializeField]
        private string _startingWeapon;
        [SerializeField]
        private Ability _startingAbility;


        public float MovementSpeed => _movementSpeed;
        public float MinGunAimingDistance => _minGunAimingDistance;
        public float MinAimingHeight => _minAimingHeight;
        public string StartingWeapon => _startingWeapon;
        public Ability StartingAbility => _startingAbility;
    }
}


