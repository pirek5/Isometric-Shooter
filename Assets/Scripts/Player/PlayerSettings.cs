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


        public float MovementSpeed => _movementSpeed;
        public float MinGunAimingDistance => _minGunAimingDistance;
        public float MinAimingHeight => _minAimingHeight;
    }
}


