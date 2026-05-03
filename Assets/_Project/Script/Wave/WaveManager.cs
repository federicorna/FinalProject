
using System.Collections;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Ondate")]
    [SerializeField] private WaveData[] _waves;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _waveText;

    [Header("Portali")]
    [SerializeField] private GameObject _portalPrefab;
    [SerializeField] private Transform _portalPointLeft;
    [SerializeField] private Transform _portalPointRight;

    [Header("Timing portale")]
    [SerializeField] private float _portalOpenDuration = 2f;

    [Header("PowerUp")]
    [SerializeField] private GameObject _powerUpSpeed;      /// ondata 3
    [SerializeField] private GameObject _powerUpFireRate;   /// ondata 5
    [SerializeField] private GameObject _powerUpDamage;     /// ondata 7

    private int _enemiesAlive = 0;
    private bool _waveStarted = false;


    private void Start()
    {
        StartCoroutine(RunWaves());
    }


    //--[f.ni]--

    private IEnumerator RunWaves()
    {
        int waveCount = 0;

        foreach (WaveData wave in _waves)
        {
            _waveStarted = false;   /// Reset prima di ogni ondata

            UpdateWaveText(waveCount + 1);

            yield return StartCoroutine(SpawnWave(wave));
            /// Aspetta che almeno un nemico sia spawnato e poi che siano tutti morti
            yield return new WaitUntil(() => _waveStarted && _enemiesAlive <= 0);

            waveCount++;
            ActivatePowerUp(waveCount);

            yield return new WaitForSeconds(wave.DelayAfterWave);
        }
        Debug.Log("Hai vinto!");
        GameManager.Instance.Victory();
    }

    private void UpdateWaveText(int current)
    {
        if (_waveText != null)
            _waveText.text = $"Ondata {current}/{_waves.Length}";
    }

    private void ActivatePowerUp(int completedWave)
    {
        if (completedWave == 3 && _powerUpSpeed != null)
            _powerUpSpeed.SetActive(true);
        else if (completedWave == 5 && _powerUpFireRate != null)
            _powerUpFireRate.SetActive(true);
        else if (completedWave == 7 && _powerUpDamage != null)
            _powerUpDamage.SetActive(true);
    }

    private IEnumerator SpawnWave(WaveData wave)
    {
        /// Scelta portali
        Transform[] spawnPoints = wave.UseBothPortals
            ? new[] { _portalPointLeft, _portalPointRight }
            : new[] { Random.value > 0.5f ? _portalPointLeft : _portalPointRight };

        /// Spawna i portali e aspetta che si aprano
        foreach (Transform point in spawnPoints)
        {
            GameObject portal = Instantiate(_portalPrefab, point.position, Quaternion.identity);
            Destroy(portal, _portalOpenDuration + CalculateTotalSpawnTime(wave));
        }

        yield return new WaitForSeconds(_portalOpenDuration);

        /// Droppa i nemici uno alla volta
        foreach (var entry in wave.Enemies)
        {
            for (int i = 0; i < entry.Count; i++)
            {
                /// Sceglie il portale per drop
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

                GameObject enemy = Instantiate(entry.EnemyPrefab, spawnPoint.position, Quaternion.identity);

                /// Traccia il nemico per sapere quando è morto
                TrackEnemy(enemy);

                _enemiesAlive++;
                _waveStarted = true;
                yield return new WaitForSeconds(wave.DelayBetweenSpawns);
            }
        }
    }

    private void TrackEnemy(GameObject enemy)
    {
        /// Ascolta OnDeath del HealthComponent del nemico appena spawnato
        HealthComponent health = enemy.GetComponent<HealthComponent>();
        if (health != null)
            health.OnDeath.AddListener(() => _enemiesAlive--);
    }

    private float CalculateTotalSpawnTime(WaveData wave)
    {
        int total = 0;
        foreach (var e in wave.Enemies) total += e.Count;
        return total * wave.DelayBetweenSpawns;
    }
}