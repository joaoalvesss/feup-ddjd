using System;
using UnityEngine;
using UnityEngine.UI;

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

    // UI Elements
    public Image weaponIcon; 
    public Text weaponText; 

    // Shooting Variables
    public GameObject bulletPrefab; 
    public Transform shootPoint; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rb.freezeRotation = true;
        
        weaponIcon.gameObject.SetActive(false);
        weaponText.gameObject.SetActive(false);
    }

    void Update()
    {
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
            else if (other.CompareTag("StickyNote")) // Detect sticky note
        {
            nearbyStickyNote = other.gameObject;
        }
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
            if (other.CompareTag("StickyNote"))
        {
            nearbyStickyNote = null; // Reset when leaving the sticky note area
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
        equippedWeapon.transform.localPosition = Vector3.zero;
        equippedWeapon.transform.localRotation = Quaternion.identity;
        
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
}