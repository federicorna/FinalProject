
using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    private int _maxHealth = 1;

    [Header("Events")]
    public UnityEvent<int, int> OnDamageTaken;
    public UnityEvent OnDeath;

    private int _currentHealth;
    private bool _isDead;

    public int CurrentHealth => _currentHealth;
    public int MaxHealth => _maxHealth;
    public bool IsDead => _isDead;

    //--[f.ni]--

    /// Inizializza la salute 
    public void Initialize(int maxHealth)
    {
        _maxHealth = maxHealth;
        _currentHealth = _maxHealth;
        _isDead = false;
    }

    public void TakeDamage(int amount)
    {
        if (_isDead || amount <= 0) return;

        _currentHealth = Mathf.Max(0, _currentHealth - amount);
        OnDamageTaken?.Invoke(_currentHealth, _maxHealth);

        if (_currentHealth == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        _isDead = true;
        OnDeath?.Invoke();
    }
}