using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, ITakeDamage
{
    public float _currentHealth;
    private float _maxHealth =23;

    public static Action OnDeath;
    public static Action<float> OnDamage;
    private bool _dead;
    private void Start()
    {
        _currentHealth = _maxHealth;
    }
    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;
        Debug.Log("mal" + _currentHealth);

      

        OnDamage?.Invoke(_currentHealth / _maxHealth);

        if (_currentHealth <= 0 && !_dead)
        {
            Die();
        }

    }

    private void Die()
    {
        _dead = true;
        OnDeath?.Invoke();
    }
}