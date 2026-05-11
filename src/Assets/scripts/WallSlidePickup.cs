using UnityEngine;

public class WallSlidePickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        NewMonoBehaviourScript player = other.GetComponent<NewMonoBehaviourScript>();

        if (player != null)
        {
            player.UnlockWallSlide();
            Destroy(gameObject);
        }
    }
}