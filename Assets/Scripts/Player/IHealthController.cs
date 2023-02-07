using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  interface IDamageable
{
    public void HandleDamage(int damage);
}

public interface IDestructible
{
    public void DestroyObject();
}