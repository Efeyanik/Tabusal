using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static GameSettings;

public class BombModeGameManager : MonoBehaviour
{

    public DataManager dataManager;
    public BombUIManager bombModeUIManager;
    public GameObject gamePanel;
    public GameObject endGamePanel;
    public GameObject interRoundPanel;
    public CardSwipeManager cardSwipeManager;
    public GameObject InterAnswerPanel;
    public GameObject pausePanel;
    public AudioSource audioSource;
    public AudioClip tabooSound;
    public AudioClip correctSound;
    public AudioClip skipSound;
    public AudioClip buttonClickSound;





    [Header("Oyun Ayarları")]


    public Vector2 bombDurationRange;
    public float bombNumberOfSkipsAllowed;
    public float bombEndScore;
    public GameSettings.BombStartingRule[] bombStartingRule;
    public bool didTeamAStartLastRound = true; //sequantel modu için kullanılacak.
    public int bombStartingRuleIndex;

    [Header("Oyun Durumu")]
    public bool isGameActive = false;
    public float timeRemaining;
    public WordCard currentCard;
    public static bool isTeamATurn = true;

    [Header("Puanlar")]
    public float bombScoreA = 0;
    public float bombScoreB = 0;

    private List<WordCard> playList;

    void OnEnable()
    {
        if (LocalizationManager.Instance != null)
            LocalizationManager.Instance.OnLanguageChanged += RefreshLocalizedPanelTexts;
    }

    void OnDisable()
    {
        if (LocalizationManager.Instance != null)
            LocalizationManager.Instance.OnLanguageChanged -= RefreshLocalizedPanelTexts;
    }



    void Start()
    {
        bombStartingRule = (GameSettings.BombStartingRule[])System.Enum.GetValues(typeof(GameSettings.BombStartingRule));

        

        bombStartingRuleIndex = PlayerPrefs.GetInt("BombStartingRule", 0);
        bombDurationRange.x = PlayerPrefs.GetFloat("BombTimeMinValue",30f);
        bombDurationRange.y = PlayerPrefs.GetFloat("BombTimeMaxValue",90f);
        bombNumberOfSkipsAllowed = PlayerPrefs.GetFloat("BombPassValue",2f);
        bombEndScore = PlayerPrefs.GetFloat("BombPointValue",5f);

        isTeamATurn = GameSettings.IsTeamAStartingFirst;
        didTeamAStartLastRound = isTeamATurn;

        timeRemaining = Random.Range(bombDurationRange.x, bombDurationRange.y);
        

        dataManager = GetComponent<DataManager>();

        if (bombModeUIManager != null)
        {
            bombModeUIManager.SetupTeamNames(GameSettings.TeamAName, GameSettings.TeamBName);
        }

        playList = new List<WordCard>(dataManager.allCards);
        Debug.Log("Maç Başlıyor: " + GameSettings.TeamAName + " vs " + GameSettings.TeamBName);
        RefreshLocalizedPanelTexts();
        StartNewBombRound();

    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameActive) return;

        //Debug.Log(timeRemaining);
        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0)
        {
            if (isTeamATurn)
                bombScoreB += 1;
            if (!isTeamATurn)
                bombScoreA += 1;

            audioSource.volume = 0.6f;
            audioSource.PlayOneShot(tabooSound);
            endRound();
        }

    }




    public void StartNewBombRound()
    {
        interRoundPanel.SetActive(false);
        endGamePanel.SetActive(false);
        gamePanel.SetActive(true);

        timeRemaining = Random.Range(bombDurationRange.x, bombDurationRange.y);

        bombNumberOfSkipsAllowed = PlayerPrefs.GetFloat("BombPassValue", 2f);
        isGameActive = true;
        GetNewCard();




    }


    public void onCorrectAnswer()
    {
        isTeamATurn = !isTeamATurn;
        bombNumberOfSkipsAllowed = PlayerPrefs.GetFloat("BombPassValue", 2f);
        audioSource.volume = .5f;
        audioSource.PlayOneShot(correctSound);

        StartCoroutine(SwitchTeamRoutine());
    }

        private System.Collections.IEnumerator SwitchTeamRoutine()
        {
        
        isGameActive = false;
        gamePanel.SetActive(false);
        InterAnswerPanel.SetActive(true);

        
        string nextTeam = isTeamATurn ? GameSettings.TeamAName : GameSettings.TeamBName;
        InterAnswerPanel.transform.Find("Txt_NextTeam").GetComponent<TextMeshProUGUI>().text = nextTeam;
        SetTextIfFound(InterAnswerPanel, "TXT_INTERANSWER_NEXTTEAM", "Txt_InterAnswerNextTeam", "Txt_Kazanan", "Txt_Title");

        
        TextMeshProUGUI txtTimer = InterAnswerPanel.transform.Find("Txt_Timer").GetComponent<TextMeshProUGUI>();

        
        for (int i = 3; i > 0; i--)
        {
            txtTimer.text = i.ToString();

            
            yield return new WaitForSeconds(1f);
           
    
        }

        InterAnswerPanel.SetActive(false);
        gamePanel.SetActive(true);
        isGameActive = true;
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
            audioSource.volume = 1f;
            audioSource.pitch = 1f;
            audioSource.PlayOneShot(skipSound);
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
        audioSource.volume = 0.6f;
        audioSource.PlayOneShot(tabooSound);
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

        cardSwipeManager.frontWordText.text = currentCard.word;
        cardSwipeManager.forbidden0Text.text = currentCard.forbidden[0];
        cardSwipeManager.forbidden1Text.text = currentCard.forbidden[1];
        cardSwipeManager.forbidden2Text.text = currentCard.forbidden[2];
        cardSwipeManager.forbidden3Text.text = currentCard.forbidden[3];
        cardSwipeManager.forbidden4Text.text = currentCard.forbidden[4];



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

            DetermineNextRoundStarter();


            //isTeamATurn = !isTeamATurn;
            string nextTeam = isTeamATurn ? GameSettings.TeamAName : GameSettings.TeamBName;
            Debug.Log("Sıradaki Takım: " + nextTeam);


            


            interRoundPanel.transform.Find("Txt_NextTeam").GetComponent<TextMeshProUGUI>().text = nextTeam;
            SetTextIfFound(interRoundPanel, "UI_NEXT_TEAM", "Txt_Kazanan", "Txt_Title");
            SetTextIfFound(interRoundPanel, "TXT_TIME_IS_UP", "Txt_TimeIsUp");
            SetTextIfFound(interRoundPanel, "BTN_CONTINUE", "Btn_Continue/Txt_Continue", "Btn_Continue/Text (TMP)", "Btn_Continue/Text");
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
        RefreshLocalizedPanelTexts();
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
        GameSettings.TeamAName = LocalizationManager.Instance != null
            ? LocalizationManager.Instance.GetText("TXT_TEAM_A")
            : "Team A";
        GameSettings.TeamBName = LocalizationManager.Instance != null
            ? LocalizationManager.Instance.GetText("TXT_TEAM_B")
            : "Team B";
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");

    }


    public void DetermineNextRoundStarter()
    {

        switch (bombStartingRuleIndex)
        {
            case 0:
                // A başlar sonra b başlar (kimin kaybettiğinden bağımsız)
                isTeamATurn = !didTeamAStartLastRound;
                break;

            case 1:
                // %50 ihtimalle A, %50 ihtimalle B başlar
                isTeamATurn = Random.value > 0.5f;
                break;

            case 2:
               
                break;

            case 3:
                // Puanı az olan başlar. Beraberlik varsa rastgele başlar.
                if (bombScoreA < bombScoreB)
                    isTeamATurn = true;
                else if (bombScoreB < bombScoreA)
                    isTeamATurn = false;
                else
                    isTeamATurn = Random.value > 0.5f;
                break;
        }

        // Seçilen kişiyi bir sonraki tur için hafızaya al (Sequential modu için lazım)
        didTeamAStartLastRound = isTeamATurn;


    }

    public void PlayButtonSound()
    {
        audioSource.volume = .6f;
        audioSource.PlayOneShot(buttonClickSound);
    }

    private void RefreshLocalizedPanelTexts()
    {
        if (LocalizationManager.Instance == null) return;

        SetTextIfFound(interRoundPanel, "UI_NEXT_TEAM", "Txt_Kazanan", "Txt_Title");
        SetTextIfFound(interRoundPanel, "TXT_TIME_IS_UP", "Txt_TimeIsUp");
        SetTextIfFound(interRoundPanel, "BTN_CONTINUE", "Btn_Continue/Txt_Continue", "Btn_Continue/Text (TMP)", "Btn_Continue/Text");
        SetTextIfFound(InterAnswerPanel, "TXT_INTERANSWER_NEXTTEAM", "Txt_InterAnswerNextTeam", "Txt_Kazanan", "Txt_Title");
        SetTextIfFound(endGamePanel, "TXT_WINNER", "Txt_Kazanan", "Txt_WinnerTitle", "Txt_Title");
        SetTextIfFound(endGamePanel, "BTN_BACK", "Btn_MainMenu/Txt_MainMenu", "Btn_MainMenu/Text (TMP)", "Btn_MainMenu/Text", "Btn_Anamenu/Txt_MainMenu", "Btn_Anamenu/Text (TMP)", "Btn_Anamenu/Text");
        SetTextIfFound(endGamePanel, "BTN_RESTART", "Btn_RestartGame/Txt_RestartGame", "Btn_RestartGame/Text (TMP)", "Btn_RestartGame/Text", "Btn_Restart/Txt_Restart", "Btn_Restart/Text (TMP)", "Btn_Restart/Text");
    }

    private void SetTextIfFound(GameObject panel, string key, params string[] paths)
    {
        if (panel == null || LocalizationManager.Instance == null) return;

        string value = LocalizationManager.Instance.GetText(key);
        for (int i = 0; i < paths.Length; i++)
        {
            Transform t = panel.transform.Find(paths[i]);
            if (t != null)
            {
                TextMeshProUGUI txt = t.GetComponent<TextMeshProUGUI>();
                if (txt != null)
                {
                    txt.text = value;
                    return;
                }
            }
        }
    }



}
