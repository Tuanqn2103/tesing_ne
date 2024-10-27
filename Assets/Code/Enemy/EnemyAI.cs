using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public bool roaming = true;
    public float moveSpeed;
    public float nextWPDistance;
    public Seeker seeker;
    public bool updateContinuesPath;
    public SpriteRenderer enemySR;

    //Shoot Enemy
    public bool isShootable = true;
    public GameObject bullet;
    public float bulletSpeed;
    public float timeBtwFire;
    public Transform bulletSpawnPoint; // Vị trí bắn đạn ra
    private float fireCooldown;

    Path path;
    Coroutine moveCoroutine;
    bool reachDestination = false;

    private void Start()
    {
        InvokeRepeating("CalculatePath", 0f, 0.5f);
        reachDestination = true;
    }

    private void Update()
    {
        fireCooldown -= Time.deltaTime;
        if (fireCooldown < 0)
        {
            fireCooldown = timeBtwFire;
            //shoot
            enemyFireBullet();
        }
        UpdateEnemyDirection();
    }

    void enemyFireBullet()
    {
        if (bullet == null || !isShootable) return; // Kiểm tra nếu đạn và trạng thái bắn hợp lệ

        // Xác định vị trí bắn đạn
        Vector3 spawnPosition = bulletSpawnPoint != null ? bulletSpawnPoint.position : transform.position;

        var bulletTmp = Instantiate(bullet, spawnPosition, Quaternion.identity); // Tạo ra đạn mới tại vị trí bắn
        Rigidbody2D bulletRb = bulletTmp.GetComponent<Rigidbody2D>();
        PlayerMove player = FindObjectOfType<PlayerMove>(); // Tìm đối tượng người chơi

        if (player != null)
        {
            Vector3 direction = (player.transform.position - spawnPosition).normalized; // Xác định hướng tới người chơi từ vị trí bắn
            bulletRb.AddForce(direction * bulletSpeed, ForceMode2D.Impulse); // Thêm lực để bắn đạn theo hướng đó
        }
    }

    void UpdateEnemyDirection()
{
    PlayerMove player = FindObjectOfType<PlayerMove>();
    if (player != null)
    {
        // Xác định người chơi đang ở bên trái hay bên phải của kẻ địch
        bool isPlayerOnLeft = player.transform.position.x < transform.position.x;

        // Lật hướng của kẻ địch và điều chỉnh vị trí bắn đạn
        if (isPlayerOnLeft)
        {
            enemySR.flipX = true; // Lật về bên trái
            bulletSpawnPoint.localPosition = new Vector3(-Mathf.Abs(bulletSpawnPoint.localPosition.x), bulletSpawnPoint.localPosition.y, bulletSpawnPoint.localPosition.z);
        }
        else
        {
            enemySR.flipX = false; // Lật về bên phải
            bulletSpawnPoint.localPosition = new Vector3(Mathf.Abs(bulletSpawnPoint.localPosition.x), bulletSpawnPoint.localPosition.y, bulletSpawnPoint.localPosition.z);
        }
    }
}




    void CalculatePath()
    {
        Vector2 target = FindTarget();
        if (seeker.IsDone() && (reachDestination || updateContinuesPath))
        {
            seeker.StartPath(transform.position, target, onPathComplete);
        }
    }

    void onPathComplete(Path p)
    {
        if (p.error) return;
        path = p;
        MoveToTarget();
    }

    void MoveToTarget()
    {
        if (moveCoroutine != null) StopCoroutine(moveCoroutine);
        moveCoroutine = StartCoroutine(moveToTargetCoroutine());
    }

    IEnumerator moveToTargetCoroutine()
    {
        int currentWP = 0;
        reachDestination = false;
        while (currentWP < path.vectorPath.Count)
        {
            Vector2 direction = ((Vector2)path.vectorPath[currentWP] - (Vector2)transform.position).normalized;
            Vector2 force = direction * moveSpeed * Time.deltaTime;
            transform.position += (Vector3)force;
            float distance = Vector2.Distance(transform.position, path.vectorPath[currentWP]);

            if (distance < nextWPDistance)
            {
                currentWP++;
            }
            yield return null;
        }
        reachDestination = true;
    }

    Vector2 FindTarget()
    {
        Vector3 playerPos = FindObjectOfType<PlayerMove>().transform.position;
        if (roaming == true)
        {
            return (Vector2)playerPos + (Random.Range(10f, 50f) * new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)).normalized);
        }
        else
        {
            return playerPos;
        }
    }
}
