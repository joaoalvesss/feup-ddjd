using Unity.VisualScripting;
using UnityEngine;


public class BirdScript : MonoBehaviour
{   
    public Rigidbody2D rb;
    public float jumpForce = 7;
    public LogicScript logicScript;
    public bool isAlive = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        logicScript = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update() {   
        if(Input.GetKeyDown(KeyCode.Space) && isAlive) {
            rb.linearVelocity = Vector2.up * jumpForce;
        }
    }
    
    public void OnCollisionEnter2D(Collision2D collision) {
        isAlive = false;
        logicScript.GameOver();
    }
}
