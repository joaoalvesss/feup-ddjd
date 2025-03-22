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
    public bool isHiding = false;
    private GameObject currentLocker = null;
    public Image item1Icon;
    public Image item2Icon;
    public Image item3Icon;


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
        else if (Input.GetKeyDown(KeyCode.E))
        {
	    Debug.Log("E pressed");
            if (isHiding)
            {
		Debug.Log("Exiting Locker");
                ExitLocker();
            }
            else if (currentLocker != null)
            {
		Debug.Log("Entering Locker");
                HideInLocker();
            }
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
                animator.SetTrigger("Die");
                        rb.linearVelocity = Vector2.zero;
                        rb.bodyType = RigidbodyType2D.Kinematic; 
                        this.enabled = false;
                transform.position = new Vector2(transform.position.x, transform.position.y - 1.5f);
                StartCoroutine(WaitForDeathAnimation());
            }
        }
        else if (other.CompareTag("Locker"))
        {
            currentLocker = other.gameObject;
            Debug.Log("Press E to hide in the locker.");
        }
    }

    private IEnumerator WaitForDeathAnimation()
    {
        yield return new WaitForSeconds(0.7f); 

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
        else if (other.CompareTag("Locker"))
        {
            currentLocker = null;
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
        if (cardIcon != null)
        {
            cardIcon.gameObject.SetActive(false);
        }
    }

    void HideInLocker()
    {
        isHiding = true;
        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic; 
        GetComponent<SpriteRenderer>().enabled = false; 
        rb.simulated = false; 
    }

    void ExitLocker()
    {
        isHiding = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
        GetComponent<SpriteRenderer>().enabled = true; 
        rb.simulated = true; 
    }

    public void ShowCollectibleIcon(CollectibleType type)
    {
        switch (type)
        {
            case CollectibleType.Item1:
                Debug.Log("Showing icon for Item1");
                if (item1Icon != null) item1Icon.gameObject.SetActive(true);
                break;
            case CollectibleType.Item2:
                Debug.Log("Showing icon for Item2");
                if (item2Icon != null) item2Icon.gameObject.SetActive(true);
                break;
            case CollectibleType.Item3:
                Debug.Log("Showing icon for Item3");
                if (item3Icon != null) item3Icon.gameObject.SetActive(true);
                break;
        }
    }
    public void HideAllCollectibleIcons()
    {
        if (item1Icon != null) item1Icon.gameObject.SetActive(false);
        if (item2Icon != null) item2Icon.gameObject.SetActive(false);
        if (item3Icon != null) item3Icon.gameObject.SetActive(false);
    }


}
