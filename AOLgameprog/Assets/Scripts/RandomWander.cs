using UnityEngine;
using System.Collections;

public class RandomWander : MonoBehaviour
{
    public float moveSpeed = 1f;           // Kecepatan jalan
    public float moveInterval = 1.5f;      // Waktu tunggu antar gerakan
    public float moveRadius = 1f;          // Jarak gerakan acak
    public LayerMask obstacleMask;         // Layer yang dianggap halangan

    private Vector2 targetPosition;
    private bool isMoving = false;
    private Rigidbody2D rb;
    private Animator animator;

    [Range(0f, 1f)]
    public float idleChance = Random.Range(0.2f, 0.5f);   // 30% chance diem

    public float idleTimeMin = 2f;
    public float idleTimeMax = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        StartCoroutine(WanderRoutine());
    }

    IEnumerator WanderRoutine()
{
    Vector2[] directions = new Vector2[]
    {
        Vector2.up,
        Vector2.down,
        Vector2.left,
        Vector2.right
    };

    while (true)
    {
        if (!enabled)
        {
            yield return null;
            continue;
        }

        yield return new WaitForSeconds(moveInterval);

        // Random chance buat idle
        if (Random.value < idleChance)
        {
            isMoving = false;
            animator.SetFloat("Speed", 0f);

            float idleTime = Random.Range(idleTimeMin, idleTimeMax);
            yield return new WaitForSeconds(idleTime);
            continue; // skip movement, balik ke loop
        }

            // Pilih arah random dari 4 arah
            Vector2 randomDir = directions[Random.Range(0, directions.Length)] * moveRadius;
        targetPosition = (Vector2)transform.position + randomDir;
        if (randomDir.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (randomDir.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);

            // Cek collision
            if (!Physics2D.OverlapCircle(targetPosition, 0.2f, obstacleMask))
        {
            isMoving = true;
            animator.SetFloat("Speed", isMoving ? 1f : 0f);
                float moveTime = 0.5f;
            float elapsed = 0f;
            Vector2 startPos = transform.position;

            while (elapsed < moveTime)
            {
                if (!enabled) break;

                elapsed += Time.deltaTime;
                Vector2 newPos = Vector2.Lerp(startPos, targetPosition, elapsed / moveTime);
                rb.MovePosition(newPos);
                yield return null;
            }

            if (enabled)
                rb.MovePosition(targetPosition);

            isMoving = false;
            animator.SetFloat("Speed", 0f);
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
        }
    }
}


    private void OnDrawGizmosSelected()
    {
        // Biar kelihatan di Scene Editor radius random jalan
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, moveRadius);
    }
}