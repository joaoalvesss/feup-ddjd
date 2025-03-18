using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerScript : MonoBehaviour
{   
    public Rigidbody2D rb;
    public float jumpForce = 25f;
    public float moveSpeed = 15f;
    private bool isFacingRight = true;
    private bool isGrounded = false;
    private Animator animator;
    private GameObject nearbyWeapon = null;
    public Transform weaponHoldPosition;
    private GameObject equippedWeapon = null;
    private GameObject nearbyStickyNote = null;
    private GameObject nearbyCard = null;
    private bool hasCard = false;

    public Image weaponIcon; 
    public Text weaponText;
    public Image cardIcon; 
    public GameObject bulletPrefab; 
    public Transform shootPoint; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rb.freezeRotation = true;
        
        weaponIcon.gameObject.SetActive(false);
        weaponText.gameObject.SetActive(false);
        if (cardIcon != null) cardIcon.gameObject.SetActive(false); 
    }

    void Update()
    {
        if (PasswordPrompt.isPasswordPanelOpen) 
        {
            rb.linearVelocity = Vector2.zero; 
            return;
        }
        
        float moveInput = 0f;
        if (Input.GetKey(KeyCode.D)) moveInput = 1f;
        if (Input.GetKey(KeyCode.A)) moveInput = -1f;

        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        animator.SetFloat("xVelocity", Mathf.Abs(rb.linearVelocity.x)); 

        if (moveInput > 0 && !isFacingRight) Flip();
        else if (moveInput < 0 && isFacingRight) Flip();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) 
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); 
            isGrounded = false;
            animator.SetBool("isJumping", true);
        }

        if (Input.GetKeyDown(KeyCode.E) && nearbyWeapon != null)
        {
            PickUpWeapon(nearbyWeapon);
        }

        if (Input.GetMouseButtonDown(0) && equippedWeapon != null)
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.E) && nearbyCard != null)
        {
            PickUpCard();
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Weapon")) 
        {
            nearbyWeapon = other.gameObject;
        }
        else if (other.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }
        else if (other.CompareTag("StickyNote")) 
        {
            nearbyStickyNote = other.gameObject;
        }
        else if (other.CompareTag("Card"))
        {
            nearbyCard = other.gameObject;
            Debug.Log("Press E to pick up the card.");
        }
        else if (other.CompareTag("Guard")) 
        {
            if (TryGetComponent<PlayerHealth>(out var playerHealth))
            {
		animator.SetTrigger("Die"); // Play death animation
                rb.linearVelocity = Vector2.zero; // Stop movement
                rb.bodyType = RigidbodyType2D.Kinematic; // Disable physics movement
                this.enabled = false;
		transform.position = new Vector2(transform.position.x, transform.position.y - 1.5f);
		StartCoroutine(WaitForDeathAnimation());
            }
        }
    }

    private IEnumerator WaitForDeathAnimation()
    {
        // Wait for a few seconds before transitioning to Game Over
        yield return new WaitForSeconds(0.7f); // Adjust time as needed (e.g., 2 seconds)

	PlayerHealth playerHealth = GetComponent<PlayerHealth>();
        playerHealth.TakeDamage(3);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Weapon"))
        {
            nearbyWeapon = null;
        }
        else if (other.CompareTag("Ground"))
        {
            isGrounded = false;
            animator.SetBool("isJumping", true);
        }
        else if (other.CompareTag("StickyNote"))
        {
            nearbyStickyNote = null; 
        }
        else if (other.CompareTag("Card"))
        {
            nearbyCard = null; 
        }
    }

    void PickUpWeapon(GameObject weapon)
    {
        Debug.Log("Picked up: " + weapon.name);

        if (equippedWeapon != null)
        {
            Destroy(equippedWeapon);
        }

        equippedWeapon = Instantiate(weapon, weaponHoldPosition.position, Quaternion.identity);
        equippedWeapon.transform.SetParent(weaponHoldPosition); 
        equippedWeapon.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        if (equippedWeapon.GetComponent<Collider2D>())
        {
            equippedWeapon.GetComponent<Collider2D>().enabled = false;
        }

        Destroy(weapon);

        weaponIcon.gameObject.SetActive(true);
        weaponText.gameObject.SetActive(true);

        weaponIcon.sprite = equippedWeapon.GetComponent<SpriteRenderer>().sprite;
    }

    void Shoot()
    {
        Debug.Log("Shooting!");
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);

        if (bullet.TryGetComponent<Bullet>(out var bulletScript))
        {
            Vector2 shootDirection = isFacingRight ? Vector2.right : Vector2.left;
            bulletScript.SetDirection(shootDirection);
        }
    }

    void OnEnable()
    {
        GuardAI.OnGuardDeath += DropWeapon; 
    }

    void OnDisable()
    {
        GuardAI.OnGuardDeath -= DropWeapon; 
    }

    void DropWeapon()
    {
        if (equippedWeapon != null)
        {
            Destroy(equippedWeapon);
            equippedWeapon = null;

            weaponIcon.gameObject.SetActive(false);
            weaponText.gameObject.SetActive(false);
        }
    }

    void PickUpCard()
    {
        hasCard = true;
        Debug.Log("Picked up the card!");
        Destroy(nearbyCard);
        nearbyCard = null;

        if (cardIcon != null)
        {
            cardIcon.gameObject.SetActive(true);
        }
    }

    public bool HasCard()
    {
        return hasCard;
    }

    public void DropCard()
    {
        // hasCard = false;
        if (cardIcon != null)
        {
            cardIcon.gameObject.SetActive(false);
        }
    }
}
