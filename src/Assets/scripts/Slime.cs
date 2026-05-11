using UnityEngine;

public class SlimePath : MonoBehaviour
{
    public Transform[] points;
    public float speed = 1f;

    public float rotationOffset = 0f;

    private int currentPointIndex = 0;
    private int nextPointIndex = 1;

    private void Start()
    {
        if (points == null || points.Length < 2)
        {
            enabled = false;
            return;
        }

        transform.position = points[currentPointIndex].position;
        RotateToNextPoint();
    }

    private void Update()
    {
        Transform nextPoint = points[nextPointIndex];

        transform.position = Vector3.MoveTowards(
            transform.position,
            nextPoint.position,
            speed * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, nextPoint.position) < 0.01f)
        {
            transform.position = nextPoint.position;

            currentPointIndex = nextPointIndex;
            nextPointIndex++;

            if (nextPointIndex >= points.Length)
            {
                nextPointIndex = 0;
            }

            RotateToNextPoint();
        }
    }

    private void RotateToNextPoint()
    {
        Vector2 direction = points[nextPointIndex].position - points[currentPointIndex].position;

        if (direction.sqrMagnitude < 0.001f)
        {
            return;
        }

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle + rotationOffset);
    }
}