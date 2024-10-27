using UnityEngine;

public class LockRotation : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Kiểm tra nếu gameObject có tag "Bullet" hoặc "Enemy", thì đặt isKinematic
        if ((gameObject.CompareTag("Bullet") || gameObject.CompareTag("Enemy")) && rb != null)
        {
            rb.isKinematic = true; // Giữ cho vật thể không bị đẩy đi
        }
    }

    private void FixedUpdate()
    {
        // Lấy góc quay hiện tại
        Vector3 currentRotation = transform.eulerAngles;

        // Đặt rotation Z về 0 độ, giữ nguyên X và Y
        transform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Kiểm tra và xử lý va chạm tại đây nếu cần
        Debug.Log("Va chạm với: " + collision.gameObject.name);
    }
}
