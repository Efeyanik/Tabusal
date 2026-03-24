using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BombModeGameManager : MonoBehaviour
{

    public DataManager dataManager;
    public BombUIManager bombModeUIManager;
    public GameObject gamePanel;
    public GameObject endGamePanel;
    public GameObject interRoundPanel;
    public CardSwipeManager cardSwipeManager;



    [Header("Oyun Ayarları")]


    public Vector2 bombDurationRange;
    public float bombNumberOfSkipsAllowed = 2f;
    public float bombEndScore = 5f;


    [Header("Oyun Durumu")]
    public bool isGameActive = false;
    public float timeRemaining;
    public WordCard currentCard;
    public static bool isTeamATurn = true;

    [Header("Puanlar")]
    public float bombScoreA = 0;
    public float bombScoreB = 0;

    private List<WordCard> playList;



    void Start()
    {
        bombDurationRange = new Vector2(30f, 90f);
        timeRemaining = Random.Range(bombDurationRange.x, bombDurationRange.y);
        

        dataManager = GetComponent<DataManager>();

        if (bombModeUIManager != null)
        {
            bombModeUIManager.SetupTeamNames(GameSettings.TeamAName, GameSettings.TeamBName);
        }

        playList = new List<WordCard>(dataManager.allCards);
        Debug.Log("Maç Başlıyor: " + GameSettings.TeamAName + " vs " + GameSettings.TeamBName);
        StartNewBombRound();

    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameActive) return;

        Debug.Log(timeRemaining);
        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0)
        {
            if (isTeamATurn)
                bombScoreB += 1;
            if (!isTeamATurn)
                bombScoreA += 1;

            endRound();
        }

    }




    public void StartNewBombRound()
    {
        interRoundPanel.SetActive(false);
        endGamePanel.SetActive(false);
        gamePanel.SetActive(true);

        timeRemaining = Random.Range(bombDurationRange.x, bombDurationRange.y);

        bombNumberOfSkipsAllowed = 2f;
        isGameActive = true;
        GetNewCard();




    }


    public void onCorrectAnswer()
    {
        isTeamATurn = !isTeamATurn;
        bombNumberOfSkipsAllowed = 2f;

        GetNewCard();

        

    }

    public void onSkip()
    {
        if (bombNumberOfSkipsAllowed <= 0)
        {
            Debug.Log("Pas hakkınız kalmadı!");
            return;
        }
        else
        {
            bombNumberOfSkipsAllowed--;
            GetNewCard();


        }
    }

    public void onTaboo()
    {
        if(isTeamATurn)
            bombScoreB += 1;
        if(!isTeamATurn)
            bombScoreA += 1;


        if (bombModeUIManager != null)
        {
            bombModeUIManager.UpdateScores();
        }
        
        endRound();
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



        if (bombModeUIManager != null)
        {
            bombModeUIManager.UpdateCardUI(currentCard);
        }


    }





    public void endRound()
    {
        isGameActive = false;
        timeRemaining = 0;

        if (bombScoreA >= bombEndScore || bombScoreB >= bombEndScore)
        {
            EndGame();
        }
        else
        {
            gamePanel.SetActive(false);
            interRoundPanel.SetActive(true);


            //isTeamATurn = !isTeamATurn;
            string nextTeam = isTeamATurn ? GameSettings.TeamAName : GameSettings.TeamBName;
            Debug.Log("Sıradaki Takım: " + nextTeam);




            
            interRoundPanel.transform.Find("Txt_NextTeam").GetComponent<TextMeshProUGUI>().text = nextTeam;
            interRoundPanel.transform.Find("Txt_InterScoreA").GetComponent<TextMeshProUGUI>().text = GameSettings.TeamAName + ": " + bombScoreA;
            interRoundPanel.transform.Find("Txt_InterScoreB").GetComponent<TextMeshProUGUI>().text = GameSettings.TeamBName + ": " + bombScoreB;
        }

       
    }


    public void NextRound()
    {
        
        StartNewBombRound();
    }



    public void EndGame()
    {
        Debug.Log("Oyun Bitti!");
        gamePanel.SetActive(false);
        interRoundPanel.SetActive(false);
        endGamePanel.SetActive(true);
        string winner = bombScoreA > bombScoreB ? GameSettings.TeamAName : GameSettings.TeamBName;
        endGamePanel.transform.Find("Txt_Winner").GetComponent<TextMeshProUGUI>().text = winner;
       
        return;
    }


    public void RestartGame()
    {
        // Puanları sıfırla
        bombScoreA = 0;
        bombScoreB = 0;
        // Kart listesini yeniden oluştur
        playList = new List<WordCard>(dataManager.allCards);
        // Yeni tur başlat
        StartNewBombRound();
    }

    public void MainMenu()
    {

        GameSettings.TeamAName = "A Takımı";
        GameSettings.TeamBName = "B Takımı";
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");

    }




}
