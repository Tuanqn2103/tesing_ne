using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Kiểm tra nếu đối tượng hiện tại là Obstacle và va chạm với Player
        if (gameObject.CompareTag("Obstacle") && collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player va chạm với Obstacle!");
        }
    }
}
