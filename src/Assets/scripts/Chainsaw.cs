using UnityEngine;

public class Chainsaw : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;

    public float moveTime = 0.4f;
    public float waitTime = 1f;

    public AnimationCurve moveCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

    private Vector3 startPosition;
    private Vector3 targetPosition;

    private float timer = 0f;
    private float waitTimer = 0f;

    private bool moving = false;
    private bool goingToB = true;

    private void Start()
    {
        if (pointA != null)
        {
            transform.position = pointA.position;
        }

        startPosition = pointA.position;
        targetPosition = pointB.position;

        waitTimer = waitTime;
    }

    private void Update()
    {
        if (pointA == null || pointB == null)
        {
            return;
        }

        if (!moving)
        {
            waitTimer -= Time.deltaTime;

            if (waitTimer <= 0f)
            {
                moving = true;
                timer = 0f;

                startPosition = transform.position;
                targetPosition = goingToB ? pointB.position : pointA.position;
            }

            return;
        }

        timer += Time.deltaTime;

        float t = timer / moveTime;
        t = Mathf.Clamp01(t);

        float curvedT = moveCurve.Evaluate(t);

        transform.position = Vector3.Lerp(startPosition, targetPosition, curvedT);

        if (t >= 1f)
        {
            transform.position = targetPosition;

            moving = false;
            waitTimer = waitTime;
            goingToB = !goingToB;
        }
    }
}