using UnityEngine;
using System;

public class GuardAI : MonoBehaviour
{
    public Transform pointA, pointB;
    public float speed = 3f; 
    private Vector2 targetPoint;
    private bool chasingPlayer = false;
    public Transform player;
    public float detectionRange = 10f; 
    public LayerMask playerLayer;
    private Rigidbody2D rb;
    private Vector2 patrolPointA, patrolPointB; 
    private bool isDead = false;

    public Sprite deadSprite; // Assign in Inspector
    public static event Action OnGuardDeath; // Event for notifying death

    public GameObject cardPrefab;

    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        animator = GetComponent<Animator>();

        patrolPointA = pointA.position;
        patrolPointB = pointB.position;

        targetPoint = patrolPointB;
    }

    void Update()
    {
        if (isDead) return; // Stop movement if dead

        if (!chasingPlayer)
        {
            Patrol();
            CheckForPlayer();
        }
        else
        {
            ChasePlayer();
        }

	animator.SetBool("isChasing", chasingPlayer);
    }

    void Patrol()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);

        // Determine if we need to flip
        if ((targetPoint.x > transform.position.x && transform.localScale.x < 0) ||
            (targetPoint.x < transform.position.x && transform.localScale.x > 0))
        {
            FlipGuard();
        }

        if (Vector2.Distance(transform.position, targetPoint) < 2f)
        {   
            targetPoint = targetPoint == patrolPointA ? patrolPointB : patrolPointA;
        }
    }

    void CheckForPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.position - transform.position, detectionRange, playerLayer);
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            chasingPlayer = true;
        }
    }

    void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;

        // Move towards the player
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * 1.2f * Time.deltaTime);
    
        // Flip the guard if moving in the opposite direction
        if ((direction.x > 0 && transform.localScale.x < 0) || (direction.x < 0 && transform.localScale.x > 0))
        {
            FlipGuard();
        }

        // Stop chasing if player is too far
        if (Vector2.Distance(transform.position, player.position) > detectionRange + 2f)
        {
            chasingPlayer = false; 
        }
}

    void FlipGuard()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1; 
        transform.localScale = scale;
    }

    public void Die()
    {
        if (isDead) return; // Prevent multiple calls

        isDead = true;
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        GetComponent<SpriteRenderer>().sprite = deadSprite;

	transform.position = new Vector2(transform.position.x, transform.position.y - 2f); // Set Y to -1

	Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (Collider2D col in colliders)
        {
            col.enabled = false;
        }

	if (cardPrefab != null)
        {
            Instantiate(cardPrefab, transform.position, Quaternion.identity);
        }

        OnGuardDeath?.Invoke(); // Notify listeners (e.g., PlayerScript)
    }
}
