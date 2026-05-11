using UnityEngine;

public class WallSlime : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;

    public float speed = 1f;

    private Transform targetPoint;

    private void Start()
    {
        targetPoint = pointB;
    }

    private void Update()
    {
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        if (pointA == null || pointB == null)
        {
            return;
        }

        transform.position = Vector2.MoveTowards(
            transform.position,
            targetPoint.position,
            speed * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, targetPoint.position) < 0.05f)
        {
            ChangeTarget();
            Flip();
        }
    }

    private void ChangeTarget()
    {
        if (targetPoint == pointA)
        {
            targetPoint = pointB;
        }
        else
        {
            targetPoint = pointA;
        }
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}