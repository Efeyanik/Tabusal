using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;



public class GameManager : MonoBehaviour
{
    public DataManager dataManager;
    public UIManager uiManager;
    public GameObject gamePanel;
    public GameObject endGamePanel;
    public GameObject interRoundPanel;
    public AudioClip correctSound;
    public AudioClip errorSound;
    public AudioClip skipSound;
    public AudioClip buttonClickSound;
    public AudioSource audioSource1;
    
    public CardSwipeManager cardSwipeManager;



    [Header("Oyun Ayarlarý")]

    public float classicDuration;
    public float tabooScore;
    public float numberOfSkipsAllowed;
    public float endScore;


    [Header("Oyun Durumu")]
    public bool isGameActive = false;
    public float timeRemaining;
    public WordCard currentCard;
    public static bool isTeamATurn = true;

    [Header("Puanlar")]
    public float scoreA = 0;
    public float scoreB = 0;

    private List<WordCard> playList;

    void Start()
    {
        classicDuration = PlayerPrefs.GetFloat("TimeValue", 60f);
        tabooScore = PlayerPrefs.GetFloat("TabuValue", 2f);
        numberOfSkipsAllowed = PlayerPrefs.GetFloat("PassValue", 3f);
        endScore = PlayerPrefs.GetFloat("PointValue", 30f);


        dataManager = GetComponent<DataManager>();
        
        
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

        
        timeRemaining = classicDuration;
       

        numberOfSkipsAllowed = PlayerPrefs.GetFloat("PassValue", 3f);
        isGameActive = true;
        GetNewCard();
    }

    void EndRound()
    {

    
            isGameActive = false;
            timeRemaining = 0;

            // Turu bitiren takým kimdi? (Sýrayý deðiþtirmeden önce hafýzaya alýyoruz)
            bool finishingTeamWasA = isTeamATurn;

            isTeamATurn = !isTeamATurn; // Sýrayý sýradaki takýma geçir

            
            // A takýmý baþladýðý için tam bir döngü B takýmý oynadýktan sonra biter.
            // Yani turu bitiren takým B (finishingTeamWasA == false) ise skorlarý kontrol etmeliyiz!
            if (finishingTeamWasA == false)
            {
                if (scoreA >= endScore || scoreB >= endScore)
                {
                    if (scoreA == scoreB) // Ýkisi de hedefi geçmiþ ve puanlar EÞÝT!
                    {
                        endScore += 10; // Hedefi 10 puan ileri atýyoruz (Uzatma)
                        // Ekraný Uzatma yazýsýyla aç
                        ShowInterRoundPanel("UZATMALAR!\nYeni Hedef: " + endScore, true);
                        return;
                    }
                    else // Biri diðerinden daha yüksek puan yapmýþ, KESÝN GALÝP!
                    {
                        endGame();
                        return;
                    }
                }
            }

            // Eðer oyun bitmediyse veya A takýmý daha yeni oynadýysa (B'nin oynamasý lazýmsa) normal devam et
            string nextTeam = isTeamATurn ? GameSettings.TeamAName : GameSettings.TeamBName;
            string topText = nextTeam;       
            bool highlightText = false;

            if (isTeamATurn == false && scoreA >= endScore)
            {

            topText = nextTeam + " (" + endScore + " Puaný Geçmelisin)";
            highlightText = true; // Yazýyý kýrmýzý/dikkat çekici yapmak için

            }

            // Ekraný göster
            ShowInterRoundPanel(topText, highlightText);
        

        }







    private void ShowInterRoundPanel(string topText, bool isHighlight)
    {
        gamePanel.SetActive(false); 
        interRoundPanel.SetActive(true);

        TextMeshProUGUI txtNextTeam = interRoundPanel.transform.Find("Txt_NextTeam").GetComponent<TextMeshProUGUI>();
        txtNextTeam.text = topText;

        GameObject sabitBaslik = interRoundPanel.transform.Find("Txt_Kazanan").gameObject;


        if (isHighlight)
        {
            txtNextTeam.color = UnityEngine.Color.red;
            sabitBaslik.GetComponent<TextMeshProUGUI>().text = "<color=#FFE100>SON ÞANS!</color>";
        }


        else
        {
            sabitBaslik.GetComponent<TextMeshProUGUI>().text = "SIRADAKÝ TAKIM";
            txtNextTeam.color = UnityEngine.Color.white;
        }

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

        cardSwipeManager.backWordText.text = currentCard.word;
        cardSwipeManager.backForbidden0Text.text = currentCard.forbidden[0];
        cardSwipeManager.backForbidden1Text.text = currentCard.forbidden[1];
        cardSwipeManager.backForbidden2Text.text = currentCard.forbidden[2];
        cardSwipeManager.backForbidden3Text.text = currentCard.forbidden[3]; 
        cardSwipeManager.backForbidden4Text.text = currentCard.forbidden[4];
        


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
            audioSource1.volume = 1f;
            audioSource1.pitch = 1f;
            audioSource1.PlayOneShot(skipSound);
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

        audioSource1.volume = 1f;
        audioSource1.pitch = 1.0f; 
        audioSource1.PlayOneShot(correctSound);

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
        audioSource1.volume = 0.5f;
        audioSource1.pitch = 1.28f; 
        audioSource1.PlayOneShot(errorSound);
        GetNewCard();
    }



    #region EndGame

    public void endGame()
    {
        isGameActive = false;
        gamePanel.SetActive(false);
        endGamePanel.SetActive(true);
        endGamePanel.transform.Find("Txt_Winner").GetComponent<TextMeshProUGUI>().text = (scoreA > scoreB) ? GameSettings.TeamAName : GameSettings.TeamBName;
    }

    public void RestartGame()
    {
        // Puanlarý sýfýrla
        scoreA = 0;
        scoreB = 0;
        // Kart listesini yeniden oluþtur
        playList = new List<WordCard>(dataManager.allCards);
        endScore = PlayerPrefs.GetFloat("PointValue", 30f); // Hedef puaný sýfýrla
        // Yeni tur baþlat
        StartNewRound();
    }


    public void MainMenu()
    {

        GameSettings.TeamAName = "A Takýmý";
        GameSettings.TeamBName ="B Takýmý";    
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");  
        
    }

    #endregion


    public void PlayButtonSound()
    {
        audioSource1.volume = .6f;
        audioSource1.PlayOneShot(buttonClickSound);
    }

}