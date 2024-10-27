using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private ICollectableBehaviour _collectableBehaviour;
    private Collider2D _collider;

    private void Awake()
    {
        _collectableBehaviour = GetComponent<ICollectableBehaviour>();
        _collider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Kiểm tra nếu đối tượng va chạm là người chơi
        var player = collision.gameObject.GetComponent<PlayerMove>();
        if (player != null)
        {
            // Nếu là người chơi, thực hiện hành vi thu thập
            _collectableBehaviour.OnCollected(player.gameObject);
            Destroy(gameObject);
        }
        else
        {
            // Nếu không phải là người chơi, bỏ qua va chạm với đối tượng đó
            Physics2D.IgnoreCollision(collision.collider, _collider);
        }
    }
}
