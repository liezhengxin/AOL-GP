using UnityEngine;
using System.Collections;

public class CharMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public Rigidbody2D rb;
    public Animator animator;
    public CameraSwitch camSwitch;

    Vector2 movement;

    void Update()
    {
        if (!isHurt)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            Vector2 input = movement.normalized;

            animator.SetFloat("Speed", input.sqrMagnitude);

            if (input != Vector2.zero)
            {
                lastMoveDir = input;
                animator.SetFloat("MoveX", input.x);
                animator.SetFloat("MoveY", input.y);
            }
        }
    }

    void FixedUpdate()
    {
        if (!isHurt)
            rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("NPC")){
            Debug.Log("A Person...");
        }
    }
}