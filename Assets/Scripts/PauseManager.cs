using UnityEngine;
using TMPro;

public class PauseManager : MonoBehaviour
{

    public GameManager GameManager;
    public BombModeGameManager BombModeGameManager;
    public GameObject pausePanel;
    public TextMeshProUGUI txtPausedTitle;
    public TextMeshProUGUI txtMainMenuButton;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RefreshLocalizedTexts();
    }

    void OnEnable()
    {
        if (LocalizationManager.Instance != null)
            LocalizationManager.Instance.OnLanguageChanged += RefreshLocalizedTexts;
    }

    void OnDisable()
    {
        if (LocalizationManager.Instance != null)
            LocalizationManager.Instance.OnLanguageChanged -= RefreshLocalizedTexts;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openPausePanel()
    {
        if (GameManager != null)
        {
            GameManager.isGameActive = false;
            pausePanel.SetActive(true);
        }
        else
        {
            BombModeGameManager.isGameActive = false;
            pausePanel.SetActive(true);
        }

    }

    public void closePausePanel()
    {
        if (GameManager != null)
        {
            GameManager.isGameActive = true;
            pausePanel.SetActive(false);
        }
        else
        {
            BombModeGameManager.isGameActive = true;
            pausePanel.SetActive(false);
        }

    }

    public void ReturnToMainMenu()
    {
        
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        
    }

    public void RefreshLocalizedTexts()
    {
        if (LocalizationManager.Instance == null) return;

        if (txtPausedTitle != null)
            txtPausedTitle.text = LocalizationManager.Instance.GetText("TXT_PAUSED");

        if (txtMainMenuButton != null)
            txtMainMenuButton.text = LocalizationManager.Instance.GetText("BTN_BACK");
    }
}
