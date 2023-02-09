using System;
using UnityEngine;

public class HealthController : MonoBehaviour, IDamageable, IDestructible
{
    
    public event Action OnBeforeDestroy;

    private int currentHealth;


    public void Initialize(int maxHealth)
    {
        currentHealth = maxHealth;
    }
    
    public void HandleDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            DestroyObject();
        }
    }

    public void DestroyObject()
    {
        OnBeforeDestroy?.Invoke();
        Destroy(gameObject);
    }
}
