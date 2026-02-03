using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    public string finishTag = "Finish";
    public string menuSceneName = "Menu";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(finishTag))
        {
            SceneManager.LoadScene(menuSceneName);
        }
    }
}
