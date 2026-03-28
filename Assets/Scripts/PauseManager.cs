using UnityEngine;

public class PauseManager : MonoBehaviour
{

    public GameManager GameManager;
    public BombModeGameManager BombModeGameManager;
    public GameObject pausePanel;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
}
