using DG.Tweening.Core.Easing;
using TMPro;
using UnityEngine;

public class BombUIManager : MonoBehaviour
{   
    public BombModeGameManager bombModeGameManager;

    void Awake()
    {
        TryAutoAssignActionButtonTexts();
    }

    [Header("Kart Bileşenleri")]
    public TextMeshProUGUI txtMainWord;
    public TextMeshProUGUI[] txtForbiddenWords;

    [Header("Puan & Bilgi")]
    public TextMeshProUGUI txtTimer;
    public TextMeshProUGUI txtScoreA;
    public TextMeshProUGUI txtScoreB;
    public TextMeshProUGUI txtnumberOfSkipsAllowed;

    [Header("Buton Yazıları")]
    public TextMeshProUGUI txtBtnCorrect;
    public TextMeshProUGUI txtBtnTaboo;
    public TextMeshProUGUI txtBtnPass;

    public string teamAName = "A Takımı";
    public string teamBName = "B Takımı";

    public void SetupTeamNames(string nameA, string nameB)
    {
        teamAName = nameA;
        teamBName = nameB;
        UpdateScores(); // İsimler gelir gelmez ekrana yazsın
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


    // Update is called once per frame
    void Update()
    {
        
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
            : "Pas Hakkı";


        txtScoreA.text = teamAName + ": " + bombModeGameManager.bombScoreA;
        txtScoreB.text = teamBName + ": " + bombModeGameManager.bombScoreB;
        txtnumberOfSkipsAllowed.text = skipLabel + ": " + bombModeGameManager.bombNumberOfSkipsAllowed;
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
        Transform root = bombModeGameManager != null && bombModeGameManager.gamePanel != null
            ? bombModeGameManager.gamePanel.transform
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
