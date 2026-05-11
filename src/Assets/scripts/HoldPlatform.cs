using UnityEngine;

public class HoldPlatform : MonoBehaviour
{
    public Transform topPoint;

    public float maxSpeed = 4f;
    public float acceleration = 8f;
    public float returnSpeed = 2f;
    public float waitTime = 1f;

    private Vector3 startPosition;

    private bool playerInside = false;
    private bool movingUp = false;
    private bool movingDown = false;
    private bool waiting = false;

    private float currentSpeed = 0f;

    private NewMonoBehaviourScript player;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        if (!movingUp && !movingDown && !waiting)
        {
            if (playerInside && player != null && player.IsWallHolding())
            {
                movingUp = true;
                currentSpeed = 0f;
            }
        }

        if (movingUp)
        {
            currentSpeed += acceleration * Time.deltaTime;
            currentSpeed = Mathf.Min(currentSpeed, maxSpeed);

            transform.position = Vector3.MoveTowards(
                transform.position,
                topPoint.position,
                currentSpeed * Time.deltaTime
            );

            if (Vector3.Distance(transform.position, topPoint.position) < 0.01f)
            {
                transform.position = topPoint.position;

                movingUp = false;
                waiting = true;
                currentSpeed = 0f;

                Invoke(nameof(StartMovingDown), waitTime);
            }
        }

        if (movingDown)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                startPosition,
                returnSpeed * Time.deltaTime
            );

            if (Vector3.Distance(transform.position, startPosition) < 0.01f)
            {
                transform.position = startPosition;
                movingDown = false;
            }
        }
    }

    private void StartMovingDown()
    {
        waiting = false;
        movingDown = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        NewMonoBehaviourScript p = other.GetComponentInParent<NewMonoBehaviourScript>();

        if (p != null)
        {
            playerInside = true;
            player = p;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        NewMonoBehaviourScript p = other.GetComponentInParent<NewMonoBehaviourScript>();

        if (p != null && p == player)
        {
            playerInside = false;
            player = null;
        }
    }
}