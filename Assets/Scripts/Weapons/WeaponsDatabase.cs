using System.Collections.Generic;
using UnityEngine;

namespace IsoShooter.Weapons
{
    [CreateAssetMenu(menuName = "IsometricShooter/WeaponsDatabase")]
    public class WeaponsDatabase : ScriptableObject
    {
        [SerializeField]
        private List<GameObject> _weaponsDefinitions;
    
        private Dictionary<string, Weapon> _availableWeapons;
    
    
        private Dictionary<string, Weapon> AvailableWeapons
        {
            get
            {
                if (_availableWeapons == null)
                {
                    CreateAvailableWeaponsCollection();
                }

                return _availableWeapons;
            }
        }
    
    
        public Weapon GetWeaponPrefab(string weaponId)
        {
            if (AvailableWeapons.ContainsKey(weaponId) == false)
            {
                Debug.LogError($"Couldn't get weapon with id: {weaponId}");
                return null;
            }

            return AvailableWeapons[weaponId];
        }
    
        private void CreateAvailableWeaponsCollection()
        {
            _availableWeapons = new Dictionary<string, Weapon>();
            foreach (GameObject weaponsDefinition in _weaponsDefinitions)
            {
                if (weaponsDefinition.TryGetComponent(out Weapon weaponController))
                {
                    _availableWeapons.Add(weaponController.WeaponId, weaponController);
                }
            }
        }
    }
}


