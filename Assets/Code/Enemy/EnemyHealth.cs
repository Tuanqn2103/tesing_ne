using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    int currentHealth;
    public HealthBar healthBar;
    public UnityEvent OnDeath;
    public AudioClip shootSound;
    private AudioSource audioSource;
    private EnemySpawner spawner;

    // Tham chiếu đến player để buff mana
    private PlayerMana playerMana;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        currentHealth = maxHealth;
        healthBar.updateBar(currentHealth, maxHealth);
    }

    // Hàm gán player cho enemy
    public void SetPlayer(PlayerMana player)
    {
        playerMana = player;
    }

    public void SetSpawner(EnemySpawner enemySpawner)
    {
        spawner = enemySpawner;
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0)
        {
            return;
        }

        currentHealth -= damage;
        Debug.Log("Enemy: " + currentHealth);
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }

        healthBar.updateBar(currentHealth, maxHealth);
    }

    private void Die()
    {
        Debug.Log("Enemy has died");
        OnDeath?.Invoke();

        if (spawner != null)
        {
            spawner.AddScore(100);
            spawner.DecreaseSpawnedEnemies();
        }

        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("EnemyDeath");
            StartCoroutine(DestroyAfterAnimation(1.21f));
            PlayShootSound();
        }
        else
        {
            Debug.LogWarning("Animator not found!");
            Destroy(gameObject);
        }

        // Buff mana cho player khi enemy chết
        if (playerMana != null)
        {
            playerMana.BuffMana(20); // Buff thêm 20 mana cho player khi enemy chết
            Debug.Log("Mana buffed to player");
        }
        else
        {
            Debug.LogWarning("PlayerMana reference is missing!");
        }
    }

    private IEnumerator DestroyAfterAnimation(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet1"))
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
}

