using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;

    [Header("Kart Bileþenleri")]
    public TextMeshProUGUI txtMainWord;
    public TextMeshProUGUI[] txtForbiddenWords;

    [Header("Puan & Bilgi")]
    public TextMeshProUGUI txtTimer;
    public TextMeshProUGUI txtScoreA;
    public TextMeshProUGUI txtScoreB;
    public TextMeshProUGUI txtnumberOfSkipsAllowed;

    [Header("Buton Yazýlarý")]
    public TextMeshProUGUI txtBtnCorrect;
    public TextMeshProUGUI txtBtnTaboo;
    public TextMeshProUGUI txtBtnPass;

    // YENÝ: Takým isimlerini hafýzada tutmak için deðiþkenler
    public string teamAName = "A Takýmý";
    public string teamBName = "B Takýmý";

    void Awake()
    {
        TryAutoAssignActionButtonTexts();
    }

    // YENÝ: Oyun baþlarken isimleri buraya göndereceðiz
    public void SetupTeamNames(string nameA, string nameB)
    {
        teamAName = nameA;
        teamBName = nameB;
        UpdateScores(); // Ýsimler gelir gelmez ekrana yazsýn
    }

    void OnEnable()
    {
        if (LocalizationManager.Instance != null)
            LocalizationManager.Instance.OnLanguageChanged += RefreshLocalizedTexts;

        RefreshLocalizedTexts();
    }

    void OnDisable()
    {
        if (LocalizationManager.Instance != null)
            LocalizationManager.Instance.OnLanguageChanged -= RefreshLocalizedTexts;
    }

    void Update()
    {
        if (gameManager.isGameActive)
        {
            
           txtTimer.text = Mathf.CeilToInt(gameManager.timeRemaining).ToString();
           
        }
    }

    public void UpdateCardUI(WordCard card)
    {
        txtMainWord.text = card.word;
        for (int i = 0; i < txtForbiddenWords.Length; i++)
        {
            if (i < card.forbidden.Count)
                txtForbiddenWords[i].text = card.forbidden[i];
            else
                txtForbiddenWords[i].text = "";
        }
        UpdateScores();
    }

    public void UpdateScores()
    {
        string skipLabel = LocalizationManager.Instance != null
            ? LocalizationManager.Instance.GetText("UI_SKIPS_ALLOWED")
            : "Pas Hakký";

        txtScoreA.text = teamAName + ": " + gameManager.scoreA;
        txtScoreB.text = teamBName + ": " + gameManager.scoreB;
        txtnumberOfSkipsAllowed.text = skipLabel + ": " + gameManager.numberOfSkipsAllowed;
    }

    public void RefreshLocalizedTexts()
    {
        TryAutoAssignActionButtonTexts();

        if (LocalizationManager.Instance != null)
        {
            if (txtBtnCorrect != null) txtBtnCorrect.text = LocalizationManager.Instance.GetText("BTN_TRUE");
            if (txtBtnTaboo != null) txtBtnTaboo.text = LocalizationManager.Instance.GetText("BTN_TABOO");
            if (txtBtnPass != null) txtBtnPass.text = LocalizationManager.Instance.GetText("BTN_PASS");
        }

        UpdateScores();
    }

    private void TryAutoAssignActionButtonTexts()
    {
        Transform root = gameManager != null && gameManager.gamePanel != null
            ? gameManager.gamePanel.transform
            : null;

        if (root == null) return;

        if (txtBtnCorrect == null)
            txtBtnCorrect = FindTextByPaths(root, "BottomPanel/Btn_Correct/Txt_Correct", "BottomPanel/Btn_Correct/Text (TMP)", "BottomPanel/Btn_Correct/Text");

        if (txtBtnTaboo == null)
            txtBtnTaboo = FindTextByPaths(root, "BottomPanel/Btn_Taboo/Txt_Taboo", "BottomPanel/Btn_Taboo/Text (TMP)", "BottomPanel/Btn_Taboo/Text");

        if (txtBtnPass == null)
            txtBtnPass = FindTextByPaths(root, "BottomPanel/Btn_Pass/Txt_Pass", "BottomPanel/Btn_Pass/Text (TMP)", "BottomPanel/Btn_Pass/Text");
    }

    private TextMeshProUGUI FindTextByPaths(Transform root, params string[] paths)
    {
        for (int i = 0; i < paths.Length; i++)
        {
            Transform t = root.Find(paths[i]);
            if (t != null)
            {
                TextMeshProUGUI text = t.GetComponent<TextMeshProUGUI>();
                if (text != null) return text;
            }
        }

        return null;
    }
}