using UnityEngine;
using UnityEngine.Tilemaps;

public class Checkpoint : MonoBehaviour
{
    public Transform respawnMarker;

    private bool playerInside = false;
    private bool activated = false;
    private NewMonoBehaviourScript player;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (playerInside && !activated && Input.GetKeyDown(KeyCode.F))
        {
            ActivateCheckpoint();
        }
    }

    private void ActivateCheckpoint()
    {
        activated = true;

        if (anim != null)
        {
            anim.SetTrigger("activate");
        }

        if (player != null)
        {
            Vector3 point = transform.position;

            if (respawnMarker != null)
            {
                point = respawnMarker.position;
            }

            player.SetCheckpoint(point);
        }
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