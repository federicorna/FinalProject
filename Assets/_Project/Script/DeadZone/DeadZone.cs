using UnityEngine;

/// <summary>
/// Assegnalo a un GameObject con un Collider trigger posizionato
/// sotto la mappa. Se il player ci cade dentro, viene distrutto.
/// </summary>
public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        HealthComponent health = other.GetComponentInParent<HealthComponent>();
        if (health == null || health.IsDead) return;

        health.TakeDamage(1);
    }
}