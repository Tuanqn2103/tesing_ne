using UnityEngine;

public class BossManager : MonoBehaviour
{
    [SerializeField] private GameObject _boss; // Reference to the Boss GameObject
    [SerializeField] private GameObject _bossHealthBar; // Reference to the Boss Health Bar

    private void Start()
    {
        // Make sure the boss and health bar are inactive at the beginning
        if (_boss != null) _boss.SetActive(false);
        if (_bossHealthBar != null) _bossHealthBar.SetActive(false);
    }

    // Call this method to activate the boss and its health bar
    public void ActivateBoss()
    {
        if (_boss != null) _boss.SetActive(true);
        if (_bossHealthBar != null) _bossHealthBar.SetActive(true);
        Debug.Log("Boss and Health Bar activated!");
    }
}
