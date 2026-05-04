
using UnityEngine;

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