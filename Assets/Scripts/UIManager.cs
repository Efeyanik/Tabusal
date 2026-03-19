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

    // YENÝ: Takým isimlerini hafýzada tutmak için deðiþkenler
    private string teamAName = "A Takýmý";
    private string teamBName = "B Takýmý";

    // YENÝ: Oyun baþlarken isimleri buraya göndereceðiz
    public void SetupTeamNames(string nameA, string nameB)
    {
        teamAName = nameA;
        teamBName = nameB;
        UpdateScores(); // Ýsimler gelir gelmez ekrana yazsýn
    }

    void Update()
    {
        if (gameManager.isGameActive)
        {
            if (gameManager.currentMode == GameMode.Classic)
                txtTimer.text = Mathf.CeilToInt(gameManager.timeRemaining).ToString();
            else
                txtTimer.text = "???";
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

        txtScoreA.text = teamAName + ": " + gameManager.scoreA;
        txtScoreB.text = teamBName + ": " + gameManager.scoreB;
        txtnumberOfSkipsAllowed.text = "Pas Hakký: " + gameManager.numberOfSkipsAllowed;
    }
}