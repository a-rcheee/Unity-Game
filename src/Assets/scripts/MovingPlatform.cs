using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 0.5f;

    private Vector3 target;

    private void Start()
    {
        target = pointB.position;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            speed * Time.fixedDeltaTime
        );

        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            if (target == pointA.position)
            {
                target = pointB.position;
            }
            else
            {
                target = pointA.position;
            }
        }
    }
}