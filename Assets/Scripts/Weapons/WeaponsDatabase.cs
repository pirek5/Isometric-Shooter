using System.Collections;
using System.Collections.Generic;
using IsoShooter.Weapons;
using UnityEngine;

[CreateAssetMenu(menuName = "IsometricShooter/WeaponsDatabase")]
public class WeaponsDatabase : ScriptableObject
{
    [SerializeField]
    private List<GameObject> weaponsDefinitions;
    
    private Dictionary<string, Weapon> availableWeapons;
    
    
    private Dictionary<string, Weapon> AvailableWeapons
    {
        get
        {
            if (availableWeapons == null)
            {
                CreateAvailableWeaponsCollection();
            }

            return availableWeapons;
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
        availableWeapons = new Dictionary<string, Weapon>();
        foreach (GameObject weaponsDefinition in weaponsDefinitions)
        {
            if (weaponsDefinition.TryGetComponent(out Weapon weaponController))
            {
                availableWeapons.Add(weaponController.WeaponId, weaponController);
            }
        }
    }
}
