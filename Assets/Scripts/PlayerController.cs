using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Animator animator;

    private Rigidbody2D rb;
    private bool isGrounded = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");

        // Movement
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);

        // Flip character sprite
        if (moveX < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (moveX > 0)
            transform.localScale = new Vector3(1, 1, 1);

        // Jumping
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // Dancing
        if (Input.GetKeyDown(KeyCode.Return))
        {
            animator.SetTrigger("Dance");
        }

        // Update animator parameters
        animator.SetFloat("Speed", Mathf.Abs(moveX));
        animator.SetBool("IsGrounded", isGrounded);
    }
}