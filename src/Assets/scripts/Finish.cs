using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    public string nextLevelName = "lvl_2";
    public string unlockKey = "Level2Unlocked";

    private void OnTriggerEnter2D(Collider2D other)
    {
        NewMonoBehaviourScript player = other.GetComponentInParent<NewMonoBehaviourScript>();

        if (player != null)
        {
            if (!string.IsNullOrEmpty(unlockKey))
            {
                PlayerPrefs.SetInt(unlockKey, 1);
                PlayerPrefs.Save();
            }

            SceneManager.LoadScene(nextLevelName);
        }
    }
}