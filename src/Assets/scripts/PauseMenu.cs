using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    public string mainMenuSceneName = "Menu";

    public Slider volumeSlider;
    public Slider brightnessSlider;

    public CanvasGroup brightnessPanel;

    private bool isPaused = false;

    private void Start()
    {
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(false);
        }

        Time.timeScale = 1f;

        if (volumeSlider != null)
        {
            volumeSlider.value = AudioListener.volume;
        }

        if (brightnessSlider != null)
        {
            brightnessSlider.value = 1f;
        }

        SetBrightness(1f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ContinueGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true;

        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(true);
        }

        Time.timeScale = 0f;
    }

    public void ContinueGame()
    {
        isPaused = false;

        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(false);
        }

        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void SetVolume(float value)
    {
        AudioListener.volume = value;
    }

    public void SetBrightness(float value)
    {
        if (brightnessPanel != null)
        {
            brightnessPanel.alpha = 1f - value;
        }
    }
}