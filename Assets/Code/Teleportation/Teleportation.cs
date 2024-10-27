using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    // Vị trí mà bạn muốn dịch chuyển đến
    public Transform teleportDestination;

    // Phương thức xử lý va chạm
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Kiểm tra xem đối tượng va chạm có tag là "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            // Dịch chuyển đối tượng "Player" đến vị trí đã định sẵn
            collision.gameObject.transform.position = teleportDestination.position;
        }
    }
}
