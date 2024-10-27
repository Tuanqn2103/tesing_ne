using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _enemyCountTMP; // Reference to the TMP Text component for enemy count
    [SerializeField] private TextMeshProUGUI _scoreTMP; // Reference to the TMP Text component for score
    
    private int _score = 0; // Track the score

    // Update the UI to show the number of enemies spawned
    public void UpdateEnemyCount(int count)
    {
        if (_enemyCountTMP != null)
        {
            _enemyCountTMP.text = "ENEMY X " +count.ToString();
        }
    }

    // Function to increase the score
    public void AddScore(int points)
    {
        _score += points; // Add points to the score
        UpdateScoreUI(); // Update score display
    }

    // Update the UI to show the current score using TMP
    private void UpdateScoreUI()
    {
        if (_scoreTMP != null)
        {
            _scoreTMP.text = "Score: " + _score.ToString();
        }
    }
}
