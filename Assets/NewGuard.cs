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

    public Sprite deadSprite; 
    public static event Action OnGuardDeath; 

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
        if (isDead) return; 

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
        if (player.GetComponent<PlayerScript>().isHiding)
        {
            chasingPlayer = false;
            return;
        }
        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.position - transform.position, detectionRange, playerLayer);
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            chasingPlayer = true;
        }
    }

    void ChasePlayer()
    {
	if (player.GetComponent<PlayerScript>().isHiding)
        {
            chasingPlayer = false;
            return;
        }
        Vector2 direction = (player.position - transform.position).normalized;

        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * 1.2f * Time.deltaTime);

        if ((direction.x > 0 && transform.localScale.x < 0) || (direction.x < 0 && transform.localScale.x > 0))
        {
            FlipGuard();
        }

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
        if (isDead) return; 

        isDead = true;
	animator.enabled = false;
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        GetComponent<SpriteRenderer>().sprite = deadSprite;

	transform.position = new Vector2(transform.position.x, transform.position.y - 3f); 

	Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (Collider2D col in colliders)
        {
            col.enabled = false;
        }

	if (cardPrefab != null)
        {
            Vector2 cardSpawnPosition = new Vector2(transform.position.x - 1f, transform.position.y);
	    Instantiate(cardPrefab, cardSpawnPosition, Quaternion.identity);
        }

        OnGuardDeath?.Invoke(); 
    }
}
