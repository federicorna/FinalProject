
using UnityEngine;
using MyGame.Weapons;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private PowerUpData _data;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        ApplyEffect(other);
        gameObject.SetActive(false);
    }

    private void ApplyEffect(Collider playerCollider)
    {
        GameObject player = playerCollider.transform.root.gameObject;

        switch (_data.Type)
        {
            case PowerUpType.MoveSpeed:
                PlayerMovement movement = player.GetComponent<PlayerMovement>();
                if (movement != null)
                    movement.AddSpeed((int)_data.Value);
                break;

            case PowerUpType.FireRate:
                GunManager gun = player.GetComponentInChildren<GunManager>();
                if (gun != null)
                    gun.SetFireRate(_data.Value);
                break;

            case PowerUpType.BulletDamage:
                GunManager gunDmg = player.GetComponentInChildren<GunManager>();
                if (gunDmg != null)
                    gunDmg.AddBulletDamage((int)_data.Value);
                break;
        }

        Debug.Log($"[PowerUp] Raccolto: {_data.PowerUpName}");
    }
}