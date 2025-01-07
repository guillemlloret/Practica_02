using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, ITakeDamage
{
    public float _currentHealth;
    public float _maxHealth = 150;

    public static Action OnDeath;
    public static Action<float> OnDamage;
    private bool _dead;
    public GameObject final;
    public Animator _animator;
    public GameObject vignette;
    private void Start()
    {
        _currentHealth = _maxHealth;
        final.SetActive(false);
        _animator.SetBool("Die", false);
        vignette.SetActive(false);  
    }
    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;
        Debug.Log("mal" + _currentHealth);

      

        OnDamage?.Invoke(_currentHealth / _maxHealth);

        if (_currentHealth <= 50)
        {
            vignette.SetActive(true);
        }

        if (_currentHealth <= 0 && !_dead)
        {
            Die();
        }

    }

    private void Die()
    {
        _animator.SetBool("Die", true);
        final.SetActive(true);
    }
}