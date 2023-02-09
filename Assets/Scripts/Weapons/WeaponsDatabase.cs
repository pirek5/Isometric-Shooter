using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace IsoShooter.Weapons
{
    [CreateAssetMenu(menuName = "IsometricShooter/WeaponsDatabase")]
    public class WeaponsDatabase : ScriptableObject
    {
        private static readonly string _databasePath = "Assets/Data/WeaponsDatabase.asset"; //not ideal solution
        
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
        
#if UNITY_EDITOR
        public static List<string> GetAllWeaponsIds()
        {
            WeaponsDatabase database = GetCurrentDatabase();

            List<string> allWeaponIds = new ();
            foreach (GameObject definition in database._weaponsDefinitions)
            {
                if(definition.TryGetComponent(out Weapon weapon))
                {
                    allWeaponIds.Add(weapon.WeaponId);
                }
            }

            return allWeaponIds;
        }
        
        private static WeaponsDatabase GetCurrentDatabase()
        {
            return AssetDatabase.LoadAssetAtPath<WeaponsDatabase>(_databasePath);
        }
#endif
    }
}


