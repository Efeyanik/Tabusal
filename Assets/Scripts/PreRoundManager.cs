using TMPro;
using UnityEngine;

public class PreRoundManager : MonoBehaviour
{
    
    public TextMeshProUGUI starterTeam;
    public TextMeshProUGUI txt_preRoundTime;
    public TextMeshProUGUI txt_preRoundPass;
    public TextMeshProUGUI txt_preRoundTabu;
    public TextMeshProUGUI txt_preRoundPoint;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        if (LocalizationManager.Instance != null)
            LocalizationManager.Instance.OnLanguageChanged += RefreshTexts;

        RefreshTexts();
    }

    void OnDisable()
    {
        if (LocalizationManager.Instance != null)
            LocalizationManager.Instance.OnLanguageChanged -= RefreshTexts;
    }

    private void RefreshTexts()
    {
        string setTime = LocalizationManager.Instance != null ? LocalizationManager.Instance.GetText("SET_TIME") : "Süre";
        string setPass = LocalizationManager.Instance != null ? LocalizationManager.Instance.GetText("SET_PASS") : "Pas Hakkı";
        string setTabu = LocalizationManager.Instance != null ? LocalizationManager.Instance.GetText("SET_TABU") : "Tabu Cezası";
        string setPoint = LocalizationManager.Instance != null ? LocalizationManager.Instance.GetText("SET_POINT") : "Hedef Puan";

        starterTeam.text = GameSettings.IsTeamAStartingFirst ? GameSettings.TeamAName : GameSettings.TeamBName;

        if (GameSettings.SelectedMode == true)
        {
            float timeValue = PlayerPrefs.GetFloat("TimeValue", 60f);
            float passValue = PlayerPrefs.GetFloat("PassValue", 3f);
            float tabuValue = PlayerPrefs.GetFloat("TabuValue", 3f);
            float pointValue = PlayerPrefs.GetFloat("PointValue", 30f);

            txt_preRoundTime.text = setTime + " : " + timeValue;
            txt_preRoundPass.text = setPass + " : " + passValue;
            txt_preRoundTabu.text = setTabu + " : " + tabuValue;
            txt_preRoundPoint.text = setPoint + " : " + pointValue;

            txt_preRoundTabu.gameObject.SetActive(true);
        }
        else
        {
            float minTime = PlayerPrefs.GetFloat("BombTimeMinValue", 30f);
            float maxTime = PlayerPrefs.GetFloat("BombTimeMaxValue", 90f);
            float passValue = PlayerPrefs.GetFloat("BombPassValue", 2f);
            float pointValue = PlayerPrefs.GetFloat("BombPointValue", 5f);

            txt_preRoundTime.text = setTime + " : " + minTime + " - " + maxTime;
            txt_preRoundPass.text = setPass + " : " + passValue;
            txt_preRoundPoint.text = setPoint + " : " + pointValue;

            txt_preRoundTabu.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
