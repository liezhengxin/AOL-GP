using UnityEngine;
using System.Collections;

public class CharMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public Rigidbody2D rb;
    public Animator animator;
    public CameraSwitch camSwitch;

    Vector2 movement;
    private bool isHurt = false; // freeze movement sementara

    void Update()
    {
        if (!isHurt)
        {
            // Ambil input dari keyboard
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            // Update parameter animator (buat transisi idle <-> walk)
            float speed = movement.magnitude;
            animator.SetFloat("Speed", speed);

            // Balik arah sprite kalau jalan kiri
            if (movement.x > 0)
                transform.localScale = new Vector3(1, 1, 1);
            else if (movement.x < 0)
                transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    void FixedUpdate()
    {
        if (!isHurt)
            rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("COW")){
            Debug.Log("MOO");
        }
        if (other.CompareTag("Chicken")){
            Debug.Log("Bbokk!");
        }
        if (other.CompareTag("CaveEntrance")){
            Debug.Log("TP TIME");
        }
        if (other.CompareTag("NPC")){
            Debug.Log("A Person...");
        }
        if (other.CompareTag("Enemy")){
            Debug.Log("Player is hurt!");

            // Play hurt animation
            if (animator != null)
            {
                animator.SetTrigger("Hurt");
            }

            // Recoil coroutine
            StartCoroutine(HurtRecoil(other.transform.position));
        }
    }

    private IEnumerator HurtRecoil(Vector3 sourcePos)
    {
        isHurt = true; // freeze movement
        float recoilForce = 5f;   // tweak sesuai kekuatan
        float recoilTime = 0.2f;  // durasi dorongan
        float elapsed = 0f;

        Vector2 recoilDir = ((Vector2)rb.position - (Vector2)sourcePos).normalized;

        while (elapsed < recoilTime)
        {
            rb.MovePosition(rb.position + recoilDir * recoilForce * Time.fixedDeltaTime);
            elapsed += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        isHurt = false; // kembali bisa jalan
    }
}