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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;

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
    }

    void Patrol()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPoint) < 2f)
        {   
            targetPoint = targetPoint == patrolPointA ? patrolPointB : patrolPointA;
            FlipGuard();
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
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * 1.2f * Time.deltaTime);
        
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
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<SpriteRenderer>().sprite = deadSprite;

	Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (Collider2D col in colliders)
        {
            col.enabled = false;
        }

        OnGuardDeath?.Invoke(); // Notify listeners (e.g., PlayerScript)
    }
}
