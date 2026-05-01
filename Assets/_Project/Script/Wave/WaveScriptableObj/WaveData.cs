
using UnityEngine;

[CreateAssetMenu(fileName = "NewWave", menuName = "MyGame/Wave Data")]
public class WaveData : ScriptableObject
{
    [System.Serializable]
    public struct EnemySpawnEntry
    {
        public GameObject EnemyPrefab;
        public int Count;
    }

    [Header("Nemici di questa ondata")]
    public EnemySpawnEntry[] Enemies;

    [Header("Portali")]
    public bool UseBothPortals = false;     /// false = portale random, true = entrambi gli angoli

    [Header("Timing")]
    public float DelayBetweenSpawns = 1f;   /// pausa tra un drop e l'altro
    public float DelayAfterWave = 3f;       /// pausa prima della prossima ondata
}