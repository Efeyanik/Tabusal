using DG.Tweening.Core.Easing;
using TMPro;
using UnityEngine;

public class BombUIManager : MonoBehaviour
{   
    public BombModeGameManager bombModeGameManager;

    [Header("Kart Bileşenleri")]
    public TextMeshProUGUI txtMainWord;
    public TextMeshProUGUI[] txtForbiddenWords;

    [Header("Puan & Bilgi")]
    public TextMeshProUGUI txtTimer;
    public TextMeshProUGUI txtScoreA;
    public TextMeshProUGUI txtScoreB;
    public TextMeshProUGUI txtnumberOfSkipsAllowed;

    public string teamAName = "A Takımı";
    public string teamBName = "B Takımı";

    public void SetupTeamNames(string nameA, string nameB)
    {
        teamAName = nameA;
        teamBName = nameB;
        UpdateScores(); // İsimler gelir gelmez ekrana yazsın
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

        txtScoreA.text = teamAName + ": " + bombModeGameManager.bombScoreA;
        txtScoreB.text = teamBName + ": " + bombModeGameManager.bombScoreB;
        txtnumberOfSkipsAllowed.text = "Pas Hakkı: " + bombModeGameManager.bombNumberOfSkipsAllowed;
    }
}
