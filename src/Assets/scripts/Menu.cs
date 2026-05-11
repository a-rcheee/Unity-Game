using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;
    public GameObject levelSelectPanel;

    public Slider volumeSlider;
    public Slider brightnessSlider;

    public CanvasGroup brightnessPanel;

    public Button level2Button;
    public Button level3Button;

    private void Start()
    {
        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(true);
        }

        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }

        if (levelSelectPanel != null)
        {
            levelSelectPanel.SetActive(false);
        }

        if (volumeSlider != null)
        {
            volumeSlider.value = AudioListener.volume;
        }

        if (brightnessSlider != null)
        {
            brightnessSlider.value = 1f;
        }

        if (brightnessPanel != null)
        {
            brightnessPanel.alpha = 0f;
        }

        if (level2Button != null)
        {
            bool level2Unlocked = PlayerPrefs.GetInt("Level2Unlocked", 0) == 1;
            level2Button.interactable = level2Unlocked;
        }

        if (level3Button != null)
        {
            bool level3Unlocked = PlayerPrefs.GetInt("Level3Unlocked", 0) == 1;
            level3Button.interactable = level3Unlocked;
        }

        SetBrightness(1f);
    }

    public void OpenLevelSelect()
    {
        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(false);
        }

        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }

        if (levelSelectPanel != null)
        {
            levelSelectPanel.SetActive(true);
        }
    }

    public void CloseLevelSelect()
    {
        if (levelSelectPanel != null)
        {
            levelSelectPanel.SetActive(false);
        }

        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(true);
        }
    }

    public void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenSettings()
    {
        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(false);
        }

        if (levelSelectPanel != null)
        {
            levelSelectPanel.SetActive(false);
        }

        if (settingsPanel != null)
        {
            settingsPanel.SetActive(true);
        }
    }

    public void CloseSettings()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }

        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(true);
        }
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