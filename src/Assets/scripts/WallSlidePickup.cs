using UnityEngine;

public class WallSlidePickup : MonoBehaviour
{
    public AudioClip pickupSound;
    public float soundVolume = 3f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        NewMonoBehaviourScript player = other.GetComponent<NewMonoBehaviourScript>();

        if (player != null)
        {
            player.UnlockWallSlide();

            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position, soundVolume);
            }
            
            Destroy(gameObject);
        }
    }
}