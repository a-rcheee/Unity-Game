using UnityEngine;

public class DashPickup : MonoBehaviour
{
    public AudioClip pickupSound;
    public float soundVolume = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        NewMonoBehaviourScript player = other.GetComponentInParent<NewMonoBehaviourScript>();

        if (player != null)
        {
            player.UnlockDash();

            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position, soundVolume);
            }
            
            Destroy(gameObject);
        }
    }
}