using UnityEngine;

public class PipeMove : MonoBehaviour
{   
    public float speed = 4;
    public float destroyX = -30;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        transform.position += speed * Time.deltaTime * Vector3.left;
        if(transform.position.x < destroyX){
            Destroy(gameObject);
        }
    }
}
