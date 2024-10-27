using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlash : MonoBehaviour
{
    public float flashBoost = 2f;
    public float FlashTime;
    private float flashTime;
    private bool flashOnce;

    private Animator animator;
    private AudioSource audioSource;
    private Vector3 moveInput;
    private PlayerMana playerMana;

    public AudioClip flashSound;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        audioSource = gameObject.AddComponent<AudioSource>();
        playerMana = GetComponent<PlayerMana>();
    }

    public void ExecuteFlash(Vector3 input)
    {
        // Check if flash can occur based on time and current mana
        if (flashTime <= 0 && playerMana.currentMana > 0)
        {
            moveInput = input;

            // Set animation and boost speed
            animator.SetBool("Flash", true);

            // Move player in current direction
            Vector3 flashPosition = transform.position + (moveInput.normalized * flashBoost);
            transform.position = flashPosition;

            flashTime = FlashTime;
            flashOnce = true;
            PlayFlashSound();

            // Deduct mana if flash action is tied to mana consumption
            playerMana.TakeDamage(10); // Adjust the amount as needed
        }

        // Manage flash animation state and flash time
        if (flashTime <= 0 && flashOnce)
        {
            animator.SetBool("Flash", false);
            flashOnce = false;
        }
        else
        {
            flashTime -= Time.deltaTime;
        }
    }

    private void PlayFlashSound()
    {
        if (audioSource != null && flashSound != null)
        {
            audioSource.PlayOneShot(flashSound);
        }
    }
}
