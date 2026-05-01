
using UnityEngine;

[RequireComponent(typeof(EnemyController))]
public class EnemyContactDamage : MonoBehaviour
{
    private const string PlayerTag = "Player";
    private EnemyController _enemyController;


    private void Awake()
    {
        _enemyController = GetComponent<EnemyController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"[ContactDamage] Colpito: {other.gameObject.name} | tag: {other.tag} | isTrigger: {other.isTrigger}");

        if (!other.CompareTag(PlayerTag))
        {
            Debug.Log($"[ContactDamage] Tag non corrispondente. Atteso: {PlayerTag}, ricevuto: {other.tag}");
            return;
        }

        HealthComponent playerHealth = other.GetComponentInParent<HealthComponent>();
        Debug.Log($"[ContactDamage] HealthComponent trovato: {playerHealth != null} | IsDead: {playerHealth?.IsDead}");

        if (playerHealth == null || playerHealth.IsDead) return;
        playerHealth.TakeDamage(_enemyController.GetContactDamage());
    }
}