using UnityEngine;

public class VisionCone : MonoBehaviour
{
    public float damage = 0.5f; 
    public float damageCooldown = 1f; 
    private bool canDamage = true; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && canDamage)
        {
            Debug.Log("Player detected! Losing a life.");
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
            StartCoroutine(DamageCooldown()); 
        }
    }

    private System.Collections.IEnumerator DamageCooldown()
    {
        canDamage = false;
        yield return new WaitForSeconds(damageCooldown);
        canDamage = true;
    }
}
