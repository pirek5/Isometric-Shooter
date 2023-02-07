using System;
using System.Collections;
using System.Collections.Generic;
using IsoShooter.Player;
using IsoShooter.Weapons;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerWeaponController : MonoBehaviour
{
    [SerializeField]
    private string _selectedWeapon;
    [SerializeField]
    private WeaponsDatabase _weaponsDatabase;
    [SerializeField]
    private Transform _weaponSlot;
    
    private PlayerSettings _playerSettings;
    private ICharacterInput _playerInput;
    private Weapon _currentWeapon;
    private Transform _weaponTransform;

    public void Initialize(ICharacterInput input, PlayerSettings playerSettings)
    {
        _playerSettings = playerSettings;
        _playerInput = input;
        CreateSelectedWeapon();
        _playerInput.OnFirePerformed += HandleFirePerformed;
        _playerInput.OnFireCanceled += HandleFireCanceled;
    }

    public void CleanUp()
    {
        _playerInput.OnFirePerformed -= HandleFirePerformed;
        _playerInput.OnFireCanceled -= HandleFireCanceled;
    }

    private void Update()
    {
        RotateGun();
    }

    private void CreateSelectedWeapon()
    {
        Weapon weaponPrefab = _weaponsDatabase.GetWeaponPrefab(_selectedWeapon);
        _currentWeapon = Instantiate(weaponPrefab, _weaponSlot);
        _currentWeapon.InitializeWeapon();
        _weaponTransform = _currentWeapon.GetComponent<Transform>();
    }

    private void HandleFirePerformed()
    {
        _currentWeapon?.HandleFirePerformed();
    }

    private void HandleFireCanceled()
    {
        _currentWeapon?.HandleFireCanceled();
    }

    private void HandleReloadPerformed()
    {
        _currentWeapon?.HandleReloadPerformed();
    }
    
    private void RotateGun()
    {
        if(_weaponTransform == null)
            return;
        
        Vector3 gunAimPosition = _playerInput.AimDestination;
        if (gunAimPosition.y < _playerSettings.MinAimingHeight)
        {
            gunAimPosition = new Vector3(gunAimPosition.x, _playerSettings.MinAimingHeight, gunAimPosition.z);
        }
            
        float distanceToAim = Vector3.Distance(_weaponSlot.position, gunAimPosition);
        if(distanceToAim < _playerSettings.MinGunAimingDistance)
            return;
            
        _weaponTransform.transform.LookAt(_playerInput.AimDestination);
    }
}
