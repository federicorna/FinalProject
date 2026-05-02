
using UnityEngine;

public enum PowerUpType
{
    MoveSpeed,
    FireRate,
    BulletDamage
}

[CreateAssetMenu(fileName = "NewPowerUp", menuName = "MyGame/PowerUp Data")]
public class PowerUpData : ScriptableObject
{
    [Header("Identità")]
    public string PowerUpName = "PowerUp";
    public PowerUpType Type;

    [Header("Valore")]
    public float Value = 1f;
}