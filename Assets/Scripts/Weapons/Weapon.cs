using UnityEngine;
using UnityEngine.Serialization;

namespace IsoShooter.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField]
        private string _weaponId;
        [SerializeField]
        private string _weaponDisplayName;

    
        public string WeaponId => _weaponId;
        public string WeaponDisplayName => _weaponDisplayName;


        public virtual void InitializeWeapon()
        {
            
        }

        public virtual void HandleReloadPerformed()
        {
            
        }

        public virtual void HandleFirePerformed()
        {
            
        }

        public virtual void HandleFireCanceled()
        {
            
        }
    }
}


