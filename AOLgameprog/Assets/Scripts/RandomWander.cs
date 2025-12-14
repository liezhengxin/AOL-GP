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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

        // Pilih arah random dari 4 arah
        Vector2 randomDir = directions[Random.Range(0, directions.Length)] * moveRadius;
        targetPosition = (Vector2)transform.position + randomDir;

        // Cek collision
        if (!Physics2D.OverlapCircle(targetPosition, 0.2f, obstacleMask))
        {
            isMoving = true;
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