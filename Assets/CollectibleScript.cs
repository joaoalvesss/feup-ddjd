using UnityEngine;

public class Collectible : MonoBehaviour
{
    public float pickupRadius = 1.5f; 
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, player.position) <= pickupRadius && Input.GetKeyDown(KeyCode.E))
        {
            Collect();
        }
    }

    private void Collect()
    {
        Debug.Log("Collectible picked up!");
	    UIManager.Instance.AddScore(500);
        Destroy(gameObject); 
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, pickupRadius);
    }
}
