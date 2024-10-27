using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private float _minimumSpawnTime;
    [SerializeField] private float _maximumSpawnTime;
    [SerializeField] private int _maxEnemies = 5;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private GameObject _teleportationObject;
    [SerializeField] private GameObject _victoryManager;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private AudioClip _victoryAudioClip; // New audio clip for victory sound

    private float _timeUntilSpawn;
    private int _spawnedEnemies = 0;
    private bool _hasReachedMaxSpawn = false;

    private SpawnerManager _spawnerManager; // Reference to SpawnerManager
    private AudioSource _audioSource; // AudioSource component to play sound

    public void SetSpawnerManager(SpawnerManager manager)
    {
        _spawnerManager = manager;
    }

    void Awake()
    {
        SetTimeUntilSpawn();
        _uiManager.UpdateEnemyCount(_spawnedEnemies);
        _teleportationObject.SetActive(false);
        
        _audioSource = gameObject.AddComponent<AudioSource>(); // Add AudioSource component
        _audioSource.clip = _victoryAudioClip;
        _audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (!_hasReachedMaxSpawn)
        {
            _timeUntilSpawn -= Time.deltaTime;
            if (_timeUntilSpawn <= 0)
            {
                SpawnEnemyAtRandomPoint();
                _spawnedEnemies++;
                _uiManager.UpdateEnemyCount(_spawnedEnemies);

                if (_spawnedEnemies >= _maxEnemies)
                {
                    _hasReachedMaxSpawn = true;
                }

                SetTimeUntilSpawn();
            }
        }
    }

    private void SpawnEnemyAtRandomPoint()
    {
        if (_spawnPoints.Length == 0) return;

        Transform selectedSpawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
        GameObject newEnemy = Instantiate(_enemyPrefab, selectedSpawnPoint.position, selectedSpawnPoint.rotation);

        EnemyHealth enemyHealth = newEnemy.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.SetSpawner(this);
        }
    }

    private void SetTimeUntilSpawn()
    {
        _timeUntilSpawn = Random.Range(_minimumSpawnTime, _maximumSpawnTime);
    }

    public void DecreaseSpawnedEnemies()
    {
        if (_spawnedEnemies > 0)
        {
            _spawnedEnemies--;
            _uiManager.UpdateEnemyCount(_spawnedEnemies);
            CheckAllEnemiesDefeated();
        }
    }

    private void CheckAllEnemiesDefeated()
    {
        if (_spawnedEnemies == 0 && _hasReachedMaxSpawn)
        {
            if (_spawnerManager != null)
            {
                _spawnerManager.OnSpawnerCleared(); // Notify manager that this spawner is cleared
            }

            if (_victoryManager != null)
            {
                _victoryManager.SetActive(true);
                Time.timeScale = 0;
            }

            PlayVictorySound(); // Play victory sound when all enemies are defeated
        }
    }

    private void PlayVictorySound()
    {
        if (_audioSource != null && _victoryAudioClip != null)
        {
            _audioSource.Play();
        }
    }

    public void AddScore(int points)
    {
        _uiManager.AddScore(points);
    }
}
