using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 100; // Default value for maxHealth
    int currentHealth;
    public HealthBar healthBar;
    public UnityEvent OnDeath;
    public AudioClip shootSound;
    private AudioSource audioSource;
    public Animator animator;
    public GameObject gameOverScreen;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        currentHealth = maxHealth;
        healthBar.updateBar(currentHealth, maxHealth);
    }

    void Update()
    {
        // TakeDamage(10);
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0)
        {
            return; // No further damage if player is already dead
        }

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0; // Prevent health from going below 0
            Die();
        }

        healthBar.updateBar(currentHealth, maxHealth);
    }

    private void Die()
    {
        Debug.Log("Player has died");
        OnDeath?.Invoke(); // Kích hoạt sự kiện OnDeath nếu có

        if (animator != null)
        {
            animator.SetTrigger("IsDead"); // Kích hoạt hoạt ảnh chết
        }

        // Thêm delay để đảm bảo hoạt ảnh chết có thời gian phát trước khi thực hiện các hành động khác
        StartCoroutine(HandleDeath());
    }
     private IEnumerator HandleDeath()
    {
        yield return new WaitForSeconds(2.12f); // Chờ 1 giây cho hoạt ảnh chết
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }
        Time.timeScale = 0f; // Dừng thời gian trò chơi
        ShowGameOverScreen();
    }
    private void ShowGameOverScreen()
    {
        // Thực hiện hành động hiển thị màn hình game over ở đây
        Debug.Log("Game Over screen should be shown.");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet")) // Assuming you want to detect collision with an enemy
        {
            TakeDamage(10);
            Destroy(collision.gameObject);
            PlayShootSound();
            
        }

    }
    void PlayShootSound()
    {
        if (shootSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
        else
        {
            Debug.LogWarning("Shooting sound or AudioSource is missing!");
        }
    }
    public void BuffHealth(float healthAmount)
{
    // Nếu lượng máu hiện tại đã đầy, không cần hồi máu
    if (currentHealth >= maxHealth)
    {
        currentHealth = maxHealth;
        return;
    }

    // Tăng lượng máu nhưng không vượt quá maxHealth
    currentHealth += Mathf.FloorToInt(healthAmount); // Chuyển đổi từ float sang int nếu cần
    if (currentHealth > maxHealth)
    {
        currentHealth = maxHealth; // Đảm bảo không vượt quá giá trị maxHealth
    }

    // Cập nhật thanh máu
    healthBar.updateBar(currentHealth, maxHealth);

    Debug.Log("Player health increased by: " + healthAmount);
}

}
