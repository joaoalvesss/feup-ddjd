using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera[] cameras;
    public Transform player;

    private void Start()
    {
        foreach (Camera cam in cameras)
        {
            cam.enabled = false;
        }
        cameras[0].enabled = true; 
    }

    private void Update()
    {
        foreach (Camera cam in cameras)
        {
            if (IsPlayerVisible(cam))
            {
                SwitchToCamera(cam);
                break;
            }
        }
    }

    private bool IsPlayerVisible(Camera cam)
    {
        Vector3 viewportPos = cam.WorldToViewportPoint(player.position);
        return viewportPos.x >= 0 && viewportPos.x <= 1 &&
               viewportPos.y >= 0 && viewportPos.y <= 1 &&
               viewportPos.z > 0; 
    }

    private void SwitchToCamera(Camera cam)
    {
        foreach (Camera c in cameras)
        {
            c.enabled = (c == cam);
        }
    }
}
