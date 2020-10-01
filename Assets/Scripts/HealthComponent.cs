using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField ]private int health;

    public event Action<int> OnChangeHealth;
    public event Action OnObjectDestroy;
    
    public void ChangeHealth(int amount)
    {
        health += amount;
        OnChangeHealth?.Invoke(amount);
        if (health > 0) return;
        OnObjectDestroy?.Invoke();
        Destroy(gameObject);
    }
    
}
