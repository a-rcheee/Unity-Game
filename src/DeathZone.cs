using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
    public string deathTag = "Death";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(deathTag))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
