using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f; 
    public float lifetime = 20f; 

    private Vector2 direction;

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    void Update()
    {
        transform.Translate(speed * Time.deltaTime * direction); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Bullet hit: " + collision.gameObject.name + " with tag: " + collision.gameObject.tag);

        if (collision.CompareTag("Guard"))
    	{
    	    collision.GetComponent<GuardAI>().Die(); 
    	    Destroy(gameObject); 
    	}
    	else if (collision.CompareTag("Wall"))
    	{
    	    Destroy(gameObject);
    	}
    }

    void Start()
    {
        Destroy(gameObject, lifetime); 
    }
}
