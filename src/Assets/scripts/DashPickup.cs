using UnityEngine;

public class DashPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        NewMonoBehaviourScript player = other.GetComponentInParent<NewMonoBehaviourScript>();

        if (player != null)
        {
            player.UnlockDash();
            Destroy(gameObject);
        }
    }
}