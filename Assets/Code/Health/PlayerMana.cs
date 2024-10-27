using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PlayerMana : MonoBehaviour
{
    [SerializeField] int maxMana = 100; // Default value for maxHealth
    public int currentMana;
    public ManaBar manaBar;
    public UnityEvent OnDeath;

    public AudioClip shootSound;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        currentMana = maxMana;
        manaBar.updateBar(currentMana, maxMana);
    }

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Space) && currentMana > 0) // Only allow if currentMana is greater than 0
        // {
        //     TakeDamage(10);
        // }
    }

    public void TakeDamage(int damage)
    {
        if (currentMana <= 0)
        {
            return; // No further damage if player is already out of mana
        }

        currentMana -= damage;
        if (currentMana <= 0)
        {
            currentMana = 0; // Prevent mana from going below 0
        }

        manaBar.updateBar(currentMana, maxMana);
    }

    public void BuffMana(float manaAmount)
    {
        // Nếu lượng máu hiện tại đã đầy, không cần hồi máu
        if (currentMana >= maxMana)
        {
            currentMana = maxMana;
            return;
        }

        // Tăng lượng máu nhưng không vượt quá maxHealth
        currentMana += Mathf.FloorToInt(manaAmount); // Chuyển đổi từ float sang int nếu cần
        if (currentMana > maxMana)
        {
            currentMana = maxMana; // Đảm bảo không vượt quá giá trị maxHealth
        }

        // Cập nhật thanh máu
        manaBar.updateBar(currentMana, maxMana);

        Debug.Log("Player health increased by: " + manaAmount);
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
