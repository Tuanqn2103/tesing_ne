// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Bullet : MonoBehaviour
// {   
//     [SerializeField] private int damageAmount = 10; // Adjust this value as needed

//     void OnCollisionEnter2D(Collision2D collision)
//     {
//         // Check if the bullet collides with the player
//         if (collision.gameObject.CompareTag("Player"))
//         {
//             // Get the PlayerHealth component from the player GameObject
//             PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
//             if (playerHealth != null)
//             {
//                 // Call the TakeDamage method on the PlayerHealth component
//                 playerHealth.TakeDamage(damageAmount);
//             }

//             // Destroy the bullet on collision
//             Destroy(gameObject);
//         }
//         else
//         {
//             // If it hits something other than the player, just destroy the bullet
//             Destroy(gameObject);
//         }
//     }
// }
