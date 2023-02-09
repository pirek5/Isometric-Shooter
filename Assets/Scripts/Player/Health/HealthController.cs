using System;
using UnityEngine;

namespace IsoShooter.Player
{
    public class HealthController : MonoBehaviour, IDamageable
    {
        public event Action OnHealthDepleted;

        private int _currentHealth;


        public void Initialize(int maxHealth)
        {
            _currentHealth = maxHealth;
        }
    
        public void HandleDamage(int damage)
        {
            _currentHealth -= damage;
            if (_currentHealth <= 0)
            {
                OnHealthDepleted?.Invoke();
            }
        }
    }
}


