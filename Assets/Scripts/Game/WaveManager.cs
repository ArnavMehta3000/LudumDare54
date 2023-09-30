using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Spawning")]
    [SerializeField]
    private Turret _turret;
    public GameObject _enemyPrefab;
    [SerializeField]
    private float _spawnRadius = 5.0f;
    [SerializeField]
    [Tooltip("Time = wave number | Value = enemy count")]
    private AnimationCurve _spawnCountCurve;
    [SerializeField]
    [Tooltip("Time = wave number | Value = time between successive spawns")]
    private AnimationCurve _spawnRateCurve;

    private int _waveNumber = 0;
    private bool _waveOngoing = false;
    private int _waveSpawnCount;
    private float _waveSpawnRate;
    private int _currentSpawnedCount;
    private float _spawnTime = 0.0f;

    public int WaveNumber { get => _waveNumber; set => _waveNumber = value; }


    private void Start()
    {
        BeginWave(_waveNumber);
    }

    private void Update()
    {
        if (_waveOngoing)
        {
            DoWave();
        }
    }

    private void BeginWave(int waveNumber)
    {
        _waveOngoing = true;
        _waveNumber = waveNumber;
        _currentSpawnedCount = 0;

        // Query spawn rate and count at the start of the wave
        _waveSpawnCount = (int)_spawnCountCurve.Evaluate(_waveNumber);
        _waveSpawnRate = _spawnRateCurve.Evaluate(_waveNumber);

        Debug.Log($"Wave {_waveNumber} | Spawn Count {_waveSpawnCount} | Spawn Rate {_waveSpawnRate}");
    }

    private void DoWave()
    {
        _spawnTime += Time.deltaTime;
        if (_currentSpawnedCount <= _waveSpawnCount && _spawnTime > _waveSpawnRate)
        {
            _spawnTime = 0.0f;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        EnemyBase enemy = Instantiate(GetEnemyToSpawn(), Random.insideUnitCircle.normalized * _spawnRadius, Quaternion.identity, transform).GetComponent<EnemyBase>();

        enemy.SetTarget(_turret.transform.position, _turret.AttractionRadius, GetEnemyTargetposition());
        _currentSpawnedCount++;
    }

    private GameObject GetEnemyToSpawn()
    {
        return _enemyPrefab;
    }

    private Vector3 GetEnemyTargetposition()
    {
        Vector3 turretposition = _turret.transform.position;
        Vector3 offset = Random.insideUnitCircle.normalized * _turret.AttractionRadius;
        return turretposition + offset;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _spawnRadius);
    }
}
