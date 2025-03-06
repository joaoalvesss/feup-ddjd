using System;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{   
    public Rigidbody2D rb;
    public float jumpForce = 25f;
    public float moveSpeed = 15f;
    private bool isFacingRight = true;
    private bool isGrounded = false;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        // Move Left & Right Smoothly
        float moveInput = 0f;
        if (Input.GetKey(KeyCode.D)) moveInput = 1f;
        if (Input.GetKey(KeyCode.A)) moveInput = -1f;

        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        animator.SetFloat("xVelocity", Mathf.Abs(rb.linearVelocity.x)); // Corrected from Math.Abs

        // Flip Sprite Based on Movement
        if (moveInput > 0 && !isFacingRight)
            Flip();
        else if (moveInput < 0 && isFacingRight)
            Flip();

        // Jump Mechanic (Fixed)
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) // Fixed condition
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // Use velocity instead of linearVelocity
            isGrounded = false;
            animator.SetBool("isJumping", true);
        }
    }

    // Flip the sprite when changing direction
    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    // Proper Ground Detection
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Ground")) // Ensure only ground sets isGrounded to true
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground")) // Ensure leaving ground sets isGrounded to false
        {
            isGrounded = false;
            animator.SetBool("isJumping", true);
        }
    }
}
