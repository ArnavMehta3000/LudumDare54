using System.Collections.Generic;
using UnityEngine;

namespace LD54
{
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

        private bool _waveOngoing = false;
        private int _waveSpawnCount;
        private float _waveSpawnRate;
        private int _currentSpawnedCount;
        private float _spawnTime = 0.0f;
        private List<EnemyBase> _enemyList = new();

        private void Update()
        {
            if (_waveOngoing)
            {
                DoWave();
            }
        }

        public void BeginWave(int waveNumber)
        {
            if (_enemyList.Count != 0)
                Debug.LogError("Trying to start wave with enemies still present!");

            _waveOngoing = true;
            _currentSpawnedCount = 0;
            _enemyList.Clear();

            // Query spawn rate and count at the start of the wave
            _waveSpawnCount = (int)_spawnCountCurve.Evaluate(waveNumber);
            _waveSpawnRate = _spawnRateCurve.Evaluate(waveNumber);

            Debug.Log($"Wave {waveNumber} | Spawn Count {_waveSpawnCount} | Spawn Rate {_waveSpawnRate}");
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

            enemy.Launch(_turret.transform.position, _turret.AttractionRadius, GetEnemyTargetposition(), this);
            _enemyList.Add(enemy);

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

        public void RemoveEnemy(EnemyBase enemy)
        {
            if (_enemyList.Contains(enemy))
            {
                _enemyList.Remove(enemy);
                Destroy(enemy.gameObject);
            }
        }
        

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _spawnRadius);
        }
    } 
}
