using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        NewMonoBehaviourScript player = other.GetComponentInParent<NewMonoBehaviourScript>();

        if (player != null)
        {
            player.Respawn();
        }
    }
}