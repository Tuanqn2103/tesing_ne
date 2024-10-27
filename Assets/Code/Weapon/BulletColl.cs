using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletColl : MonoBehaviour
{
    // Thêm một biến boolean để xác định viên đạn là "tốt" hay "xấu"
    public bool isGoodBullet;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isGoodBullet)
        {
            // Phá hủy viên đạn nếu va chạm với kẻ thù hoặc các đối tượng khác trừ người chơi
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Destroy(gameObject);
            }
            else if (!collision.gameObject.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
            // Nếu va chạm với người chơi, viên đạn sẽ bỏ qua và đi xuyên qua
        }
        else
        {
            // Phá hủy viên đạn nếu va chạm với người chơi hoặc các đối tượng khác trừ kẻ thù
            if (collision.gameObject.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
            else if (!collision.gameObject.CompareTag("Enemy"))
            {
                Destroy(gameObject);
            }
            // Nếu va chạm với kẻ thù, viên đạn sẽ bỏ qua và đi xuyên qua
        }
    }
}
