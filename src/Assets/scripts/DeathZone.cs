using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        NewMonoBehaviourScript player = other.GetComponent<NewMonoBehaviourScript>();

        if (player != null)
        {
            player.Respawn();
        }
    }
}
