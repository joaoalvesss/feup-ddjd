using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    public float flipSpeed = 2.5f;
    private bool flipped = false; 
    private float flipTimer = 0f; 

    public float flipInterval = 2f; 

    private void Update()
    {
        flipTimer += Time.deltaTime;
        if (flipTimer >= flipInterval)
        {
            FlipCamera();
            flipTimer = 0f;
        }
    }

    private void FlipCamera()
    {
        flipped = !flipped;
        Vector3 scale = transform.localScale;
        scale.x *= -1; 
        transform.localScale = scale;
    }
}
