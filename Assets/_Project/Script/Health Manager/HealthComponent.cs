
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Componente generico per la salute. Si applica a qualsiasi entità:
/// nemici, player, oggetti distruttibili.
/// NON sa nulla di nemici o player — gestisce solo i numeri.
/// </summary>
public class HealthComponent : MonoBehaviour
{
    private int _maxHealth = 1;

    // --- Events ispezionabili dall'Inspector ---
    [Header("Events")]
    public UnityEvent<int, int> OnDamageTaken; // (hpAttuali, hpMax)
    public UnityEvent OnDeath;
    public UnityEvent OnHealed;

    // --- Stato interno protetto ---
    private int _currentHealth;
    private bool _isDead;

    // Proprietà pubblica in sola lettura
    public int CurrentHealth => _currentHealth;
    public int MaxHealth => _maxHealth;
    public bool IsDead => _isDead;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    /// <summary>
    /// Inizializza la salute da uno ScriptableObject.
    /// Chiamato da EnemyController dopo che l'oggetto è stato creato.
    /// </summary>
    public void Initialize(int maxHealth)
    {
        _maxHealth = maxHealth;
        _currentHealth = _maxHealth;
        _isDead = false;
    }

    /// <summary>
    /// Riduce la vita. Ignora i danni se già morto.
    /// </summary>
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

    /// <summary>
    /// Ripristina vita. Utile per power-up o magie.
    /// </summary>
    public void Heal(int amount)
    {
        if (_isDead || amount <= 0) return;

        _currentHealth = Mathf.Min(_maxHealth, _currentHealth + amount);
        OnHealed?.Invoke();
    }

    private void Die()
    {
        _isDead = true;
        OnDeath?.Invoke();
    }
}