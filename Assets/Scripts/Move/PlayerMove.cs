using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float flashBoost = 2f;
    private float flashTime;
    public float FlashTime;
    bool flashOnce;
    private Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer characterSR;
    public Vector3 moveInput;

    public PlayerMana playerMana;
    public AudioClip flashSound;
    private AudioSource audioSource;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        audioSource = gameObject.AddComponent<AudioSource>();
        playerMana = GetComponent<PlayerMana>();
    }

    void Update()
    {
        movePlayer();
        flashPlayer();
        
    }
    private void flashPlayer()
{
    if (Input.GetKeyDown(KeyCode.Space) && flashTime <= 0 && playerMana.currentMana > 0)
    {
        // Lưu vị trí ban đầu
        Vector3 startPosition = transform.position;
        
        // Tính toán vị trí dịch chuyển
        Vector3 flashPosition = startPosition + (moveInput.normalized * flashBoost);
        
        // Kiểm tra va chạm với obstacle
        RaycastHit hit;
        if (!Physics.Raycast(startPosition, moveInput.normalized, out hit, flashBoost) || !hit.collider.CompareTag("obstacle"))
        {
            // Nếu không gặp obstacle, thực hiện flash
            animator.SetBool("Flash", true);
            transform.position = flashPosition;

            flashTime = FlashTime;
            flashOnce = true;
            PlayFlashSound();

            // Tùy chọn, trừ mana nếu hành động flash cần tiêu tốn mana
            playerMana.TakeDamage(10); // Trừ 10 mana (hoặc một giá trị phù hợp)
        }
        else
        {
            // Nếu gặp obstacle, đặt lại vị trí về startPosition
            transform.position = startPosition;
            Debug.Log("Obstacle detected, cannot flash!");
        }
    }

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



    private void movePlayer()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");

        transform.position += moveInput * moveSpeed * Time.deltaTime;
        animator.SetFloat("Speed", moveInput.sqrMagnitude);
        if (moveInput.x != 0)
        {
            if (moveInput.x > 0)
            {
                characterSR.transform.localScale = new Vector3(1, 1, 0);
            }
            else
            {
                characterSR.transform.localScale = new Vector3(-1, 1, 0);
            }
        }
    }
    void PlayFlashSound()
    {
        if (audioSource != null && flashSound != null)
        {
            audioSource.PlayOneShot(flashSound);
        }
    }
}
