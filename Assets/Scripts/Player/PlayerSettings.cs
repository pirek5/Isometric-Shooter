using IsoShooter.Abilities;
using IsoShooter.Weapons;
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
        private int _health;
        [WeaponId]
        [SerializeField]
        private string _startingWeapon;
        [SerializeField]
        private Ability _startingAbility;


        public float MovementSpeed => _movementSpeed;
        public float MinGunAimingDistance => _minGunAimingDistance;
        public int Health => _health;
        public string StartingWeapon => _startingWeapon;
        public Ability StartingAbility => _startingAbility;
    }
}


