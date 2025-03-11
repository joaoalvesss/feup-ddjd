using UnityEngine;

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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        patrolPointA = pointA.position;
        patrolPointB = pointB.position;

        targetPoint = patrolPointB;
    }

    void Update()
    {
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
        Debug.Log("Patrolling... Target: " + targetPoint);

        if (Vector2.Distance(transform.position, targetPoint) < 2f)
        {   
            Debug.Log("Switching target...");

            if (targetPoint == patrolPointA)
            {
                targetPoint = patrolPointB;
                Debug.Log("Switching to B");
            }
            else
            {
                targetPoint = patrolPointA;
                Debug.Log("Switching to A");
            }

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
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * 1.5f * Time.deltaTime);
        
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
}
