using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private Vector3 checkpointPosition;

    private void Start()
    {
        checkpointPosition = transform.position;
    }

    public void SetCheckpoint(Vector3 newCheckpointPosition)
    {
        checkpointPosition = newCheckpointPosition;
    }

    public void Die()
    {
        transform.position = checkpointPosition;
    }
}