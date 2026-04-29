
using UnityEngine;

/// <summary>
/// Gestisce il danno da contatto del nemico al player.
/// Sta sul prefab nemico. Legge il danno da EnemyController
/// e lo applica al HealthComponent del player.
/// </summary>
[RequireComponent(typeof(EnemyController))]
public class EnemyContactDamage : MonoBehaviour
{
    // Tag che il player DEVE avere nell'Inspector
    private const string PlayerTag = "Player";

    private EnemyController _enemyController;

    private void Awake()
    {
        _enemyController = GetComponent<EnemyController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(PlayerTag)) return;

        // Cerca HealthComponent sul player (o nel suo parent)
        HealthComponent playerHealth = other.GetComponentInParent<HealthComponent>();

        if (playerHealth == null || playerHealth.IsDead) return;

        playerHealth.TakeDamage(_enemyController.GetContactDamage());
    }
}