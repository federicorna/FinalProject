
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Cervello del nemico. Si occupa di:
/// 1. Leggere i dati da EnemyData (ScriptableObject)
/// 2. Inizializzare HealthComponent con i dati corretti
/// 3. Reagire agli eventi di danno e morte
///
/// Per creare un nuovo tipo di nemico: crea un nuovo EnemyData asset,
/// trascina questo script sul prefab, assegna l'asset. Zero nuovo codice.
/// </summary>
[RequireComponent(typeof(HealthComponent))]
public class EnemyController : MonoBehaviour
{
    [Header("Dati del nemico")]
    [SerializeField] private EnemyData _data;

    [Header("Effetti (opzionali)")]
    [SerializeField] private GameObject _deathParticlePrefab;
    [SerializeField] private GameObject _relicDropPrefab; // la reliquia di Muffin Knight

    // Riferimento al componente salute sullo stesso oggetto
    private HealthComponent _health;

    private void Awake()
    {
        _health = GetComponent<HealthComponent>();
        _health.Initialize(_data.MaxHealth);

        // Applica la velocità da EnemyData al NavMeshAgent
        // In questo modo EnemyData è l'unica fonte di verità per la velocità
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        if (agent != null)
            agent.speed = _data.MoveSpeed;

        _health.OnDeath.AddListener(HandleDeath);
        _health.OnDamageTaken.AddListener(HandleDamageTaken);
    }

    private void OnDestroy()
    {
        // Pulizia: rimuoviamo sempre i listener per evitare memory leak
        _health.OnDeath.RemoveListener(HandleDeath);
        _health.OnDamageTaken.RemoveListener(HandleDamageTaken);
    }

    // ----- Handlers degli eventi -----

    private void HandleDamageTaken(int currentHp, int maxHp)
    {
        // Qui puoi: aggiornare una barra HP, far lampeggiare lo sprite, ecc.
        // Debug.Log($"{_data.EnemyName} colpito! HP: {currentHp}/{maxHp}");
    }

    private void HandleDeath()
    {
        // Spawn particelle di morte
        if (_deathParticlePrefab != null)
            Instantiate(_deathParticlePrefab, transform.position, Quaternion.identity);

        // Drop della reliquia (meccanica core di Muffin Knight)
        if (_relicDropPrefab != null)
            Instantiate(_relicDropPrefab, transform.position, Quaternion.identity);

        // Distruggi il GameObject
        Destroy(gameObject);
    }

    // Esposto pubblicamente per il danno da contatto con il player
    public int GetContactDamage() => _data != null ? _data.ContactDamage : 0;
    public string GetEnemyName() => _data != null ? _data.EnemyName : "???";
}