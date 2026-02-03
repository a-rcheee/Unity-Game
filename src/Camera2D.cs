using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 0.5f, -10f);
    public float smoothTime = 0.15f;

    public float deadZoneX = 0.6f;
    public float deadZoneY = 0.8f;

    public bool followX = true;
    public bool followY = false;

    private Vector3 velocity;
    private float fixedY;

    private void Start()
    {
        fixedY = transform.position.y;
    }
    private void Update()
    {
       if (target == null)
        { 
            return;
        }

        Vector3 desired = target.position + offset;
        Vector3 current = transform.position;

        float x = current.x;
        float y = current.y;

        if (followX)
        {
            float dx = desired.x - current.x;
            if (Mathf.Abs(dx) > deadZoneX)
            {
                x = desired.x - Mathf.Sign(dx) * deadZoneX;
            }
        }

        if (followY)
        {
            float dy = desired.y - current.y;
            if (Mathf.Abs(dy) > deadZoneY)
            {
                y = desired.y - Mathf.Sign(dy) * deadZoneY;
            }
        }
        else
        {
            y = fixedY;
        }

        transform.position = Vector3.SmoothDamp(current, new Vector3(x, y, desired.z), ref velocity, smoothTime);
    }
}
