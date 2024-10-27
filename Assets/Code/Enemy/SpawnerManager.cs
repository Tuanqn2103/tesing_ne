using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner[] _enemySpawners; // Array of EnemySpawners for each map
    [SerializeField] private BossManager _bossManager; // Reference to BossManager
    private int _currentSpawnerIndex = 0; // Track which spawner is currently active

    void Start()
    {
        // Set only the first spawner active at the start
        for (int i = 0; i < _enemySpawners.Length; i++)
        {
            _enemySpawners[i].gameObject.SetActive(i == 0);
            _enemySpawners[i].SetSpawnerManager(this); // Set manager reference in each spawner
        }
    }

    // Method called by EnemySpawner when all enemies are defeated
    public void OnSpawnerCleared()
    {
        // Deactivate the current spawner
        _enemySpawners[_currentSpawnerIndex].gameObject.SetActive(false);

        // Increment to the next spawner
        _currentSpawnerIndex++;

        // If there are more spawners, activate the next one
        if (_currentSpawnerIndex < _enemySpawners.Length)
        {
            _enemySpawners[_currentSpawnerIndex].gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("All spawners cleared!");

            // If this was the last spawner, activate the boss
            if (_bossManager != null)
            {
                _bossManager.ActivateBoss();
            }
        }
    }
}
