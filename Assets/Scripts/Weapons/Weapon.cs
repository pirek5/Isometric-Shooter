using UnityEngine;

namespace IsoShooter.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField]
        private string weaponId;
        [SerializeField]
        private string weaponDisplayName;

    
        public string WeaponId => weaponId;
        public string WeaponDisplayName => weaponDisplayName;


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


