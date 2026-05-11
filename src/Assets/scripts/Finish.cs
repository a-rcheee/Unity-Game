using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    public string nextLevelName = "Level2";

    private void OnTriggerEnter2D(Collider2D other)
    {
        NewMonoBehaviourScript player = other.GetComponentInParent<NewMonoBehaviourScript>();

        if (player != null)
        {
            PlayerPrefs.SetInt("Level2Unlocked", 1);
            PlayerPrefs.Save();

            SceneManager.LoadScene(nextLevelName);
        }
    }
}