using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
public class PlayerDeathHandler : MonoBehaviour
{
    private HealthComponent _health;

    private void Awake()
    {
        _health = GetComponent<HealthComponent>();
        _health.OnDeath.AddListener(HandleDeath);
    }


    //--[f.ni]--

    private void OnDestroy()
    {
        _health.OnDeath.RemoveListener(HandleDeath);
    }

    private void HandleDeath()
    {
        Debug.Log("[Player] Morto!");
        gameObject.SetActive(false);    /// Nasconde il player temporaneamente
        GameManager.Instance.GameOver();
    }
}