
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(HealthComponent))]
public class EnemyController : MonoBehaviour
{
    [Header("Dati del nemico")]
    [SerializeField] private EnemyData _data;

    private HealthComponent _health;

    private void Awake()
    {
        _health = GetComponent<HealthComponent>();
        _health.Initialize(_data.MaxHealth);

        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.speed = _data.MoveSpeed;
            agent.avoidancePriority = _data.AvoidancePriority;
        }

        _health.OnDeath.AddListener(HandleDeath);
        _health.OnDamageTaken.AddListener(HandleDamageTaken);
    }

    private void OnDestroy()
    {
        _health.OnDeath.RemoveListener(HandleDeath);
        _health.OnDamageTaken.RemoveListener(HandleDamageTaken);
    }

    //--[f.ni]--

    private void HandleDamageTaken(int currentHp, int maxHp)
    {
        Debug.Log($"{_data.EnemyName} colpito! HP: {currentHp}/{maxHp}");
    }

    private void HandleDeath()
    {
        Destroy(gameObject);
    }

    /// Pubblic per il danno da contatto con il player
    public int GetContactDamage() => _data != null ? _data.ContactDamage : 0;
    public string GetEnemyName() => _data != null ? _data.EnemyName : "";
}