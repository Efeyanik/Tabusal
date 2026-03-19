using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public enum GameMode
{
    Classic,
    Bomb
}

public class GameManager : MonoBehaviour
{
    public DataManager dataManager;
    public UIManager uiManager;
    public GameObject gamePanel;
    public GameObject endGamePanel;
    public GameObject interRoundPanel;
    public AudioSource audioSource;
    public AudioClip correctSound;
    public AudioClip errorSound;
    public AudioSource audioSource2;

    [Header("Oyun Ayarlarý")]
    public GameMode currentMode = GameMode.Classic;
    public float classicDuration = 60f;
    public Vector2 bombDurationRange = new Vector2(30f, 90f);
    public int tabooScore = 2;
    public int numberOfSkipsAllowed = 3;
    public int endScore = 30;


    [Header("Oyun Durumu")]
    public bool isGameActive = false;
    public float timeRemaining;
    public WordCard currentCard;
    public bool isTeamATurn = true;

    [Header("Puanlar")]
    public int scoreA = 0;
    public int scoreB = 0;

    private List<WordCard> playList;

    void Start()
    {
        dataManager = GetComponent<DataManager>();
        currentMode = GameSettings.SelectedMode;
        
        if (uiManager == null) uiManager = GetComponent<UIManager>();

        if (dataManager.allCards == null || dataManager.allCards.Count == 0)
        {
            Debug.LogError("Kartlar yüklenemedi!");
            return;
        }
        if (uiManager != null)
        {
            uiManager.SetupTeamNames(GameSettings.TeamAName, GameSettings.TeamBName);
        }

        // Mod ayarýný çekiyoruz
        currentMode = GameSettings.SelectedMode;

        playList = new List<WordCard>(dataManager.allCards);
        Debug.Log("Maç Baþlýyor: " + GameSettings.TeamAName + " vs " + GameSettings.TeamBName);
        StartNewRound();
    }

    void Update()
    {
        if (!isGameActive) return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0)
        {
            EndRound();
        }

        
    }

    void StartNewRound()
    {
        Debug.Log("--- YENÝ TUR BAÞLADI ---");

        interRoundPanel.SetActive(false);
        endGamePanel.SetActive(false);
        gamePanel.SetActive(true);

        if (currentMode == GameMode.Classic)
            timeRemaining = classicDuration;
        else if (currentMode == GameMode.Bomb)
            timeRemaining = Random.Range(bombDurationRange.x, bombDurationRange.y);

        numberOfSkipsAllowed = 3;
        isGameActive = true;
        GetNewCard();
    }

    void EndRound()
    {

        isGameActive = false;
        timeRemaining = 0;
        isTeamATurn = !isTeamATurn; // Sýrayý deðiþtir

        // Konsola kimin sýrasý olduðunu yaz
        string nextTeam = isTeamATurn ? GameSettings.TeamAName : GameSettings.TeamBName;
        Debug.Log("Sýradaki Takým: " + nextTeam);


        gamePanel.SetActive(false);
        interRoundPanel.SetActive(true);
        interRoundPanel.transform.Find("Txt_NextTeam").GetComponent<TextMeshProUGUI>().text = "Sýradaki Takým: " + nextTeam;
        interRoundPanel.transform.Find("Txt_InterScoreA").GetComponent<TextMeshProUGUI>().text = GameSettings.TeamAName + ": " + scoreA;
        interRoundPanel.transform.Find("Txt_InterScoreB").GetComponent<TextMeshProUGUI>().text = GameSettings.TeamBName + ": " + scoreB;


    }

    public void nextRound()
    {
        StartNewRound();
    }
    
    public void GetNewCard()
    {
        if (playList.Count == 0)
        {
            isGameActive = false;
            return;
        }

        int randomIndex = Random.Range(0, playList.Count);
        currentCard = playList[randomIndex];
        playList.RemoveAt(randomIndex);

        
        if (uiManager != null)
        {
            uiManager.UpdateCardUI(currentCard);
        }
    }

    // Pas geçme butonu için ayrý fonksiyon
    public void SkipCard()
    {
        if (!isGameActive) return;

        
        if (numberOfSkipsAllowed <= 0)
        {
            Debug.Log("Pas hakkýnýz kalmadý!");
            return;
        }
        else
        {
            numberOfSkipsAllowed--;
            GetNewCard();
            
            
        }
        
    }

    public void OnCorrectAnswer()
    {
        if (!isGameActive) return;

        if (isTeamATurn) scoreA++;
        else scoreB++;

        // SKORU UI'DA GÜNCELLE
        if (uiManager != null)
        {
            uiManager.UpdateScores();
        }

        if (scoreA >= endScore || scoreB >= endScore)
        {
            endGame();
        }

        audioSource.PlayOneShot(correctSound);

        GetNewCard();
    }

    public void OnTaboo()
    {
        if (!isGameActive) return;
        if (isTeamATurn) scoreA -= tabooScore;
        else scoreB -= tabooScore;
        // SKORU UI'DA GÜNCELLE
        if (uiManager != null)
        {
            uiManager.UpdateScores();
        }
        audioSource.PlayOneShot(errorSound);
        GetNewCard();
    }



    #region EndGame

    public void endGame()
    {
        isGameActive = false;
        gamePanel.SetActive(false);
        endGamePanel.SetActive(true);
        endGamePanel.transform.Find("Txt_Winner").GetComponent<TextMeshProUGUI>().text = (scoreA > scoreB) ? GameSettings.TeamAName + " Kazandý!" : GameSettings.TeamBName + " Kazandý!";
    }

    public void RestartGame()
    {
        // Puanlarý sýfýrla
        scoreA = 0;
        scoreB = 0;
        // Kart listesini yeniden oluþtur
        playList = new List<WordCard>(dataManager.allCards);
        // Yeni tur baþlat
        StartNewRound();
    }


    public void MainMenu()
    {

       UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");

    } 

    #endregion




}