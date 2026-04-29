
using UnityEngine;

// Crea un menu Assets > Create > MyGame > Enemy Data
[CreateAssetMenu(fileName = "NewEnemyData", menuName = "MyGame/Enemy Data")]
public class EnemyData : ScriptableObject
{
    [Header("Identità")]
    public string EnemyName = "Nemico";

    [Header("Statistiche")]
    [Min(1)] public int MaxHealth = 10;
    [Min(0f)] public float MoveSpeed = 3f;

    [Header("Combattimento")]
    [Min(0)] public int ContactDamage = 1; // danno al tocco con il player
}