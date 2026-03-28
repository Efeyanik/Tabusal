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
        // İki mod için de geçrli
        if (GameSettings.IsTeamAStartingFirst)
        {
            starterTeam.text = GameSettings.TeamAName;
        }
        else
        {
            starterTeam.text = GameSettings.TeamBName;
        }

        // klasik mod
        if (GameSettings.SelectedMode == true) 
        {
            float timeValue = PlayerPrefs.GetFloat("TimeValue", 60f);
            float passValue = PlayerPrefs.GetFloat("PassValue", 3f);
            float tabuValue = PlayerPrefs.GetFloat("TabuValue", 3f);
            float pointValue = PlayerPrefs.GetFloat("PointValue", 30f);

            txt_preRoundTime.text = "Süre : " + timeValue.ToString();
            txt_preRoundPass.text = "Pas Hakkı : " + passValue.ToString();
            txt_preRoundTabu.text = "Tabu Cezası : " + tabuValue.ToString();
            txt_preRoundPoint.text = "Hedef Puan : " + pointValue.ToString();

            // Klasik modda tabu yazısı görünür olmalı
            txt_preRoundTabu.gameObject.SetActive(true);
        }
        else //bomba modu
        {
            
            float minTime = PlayerPrefs.GetFloat("BombTimeMinValue", 30f);
            float maxTime = PlayerPrefs.GetFloat("BombTimeMaxValue", 90f);
            float passValue = PlayerPrefs.GetFloat("BombPassValue", 2f);
            float pointValue = PlayerPrefs.GetFloat("BombPointValue", 5f);

            // Süreyi "30 - 90" şeklinde aralık olarak gösteriyoruz
            txt_preRoundTime.text = "Süre : " + minTime.ToString() + " - " + maxTime.ToString();
            txt_preRoundPass.text = "Pas Hakkı : " + passValue.ToString();
            txt_preRoundPoint.text = "Hedef Puan : " + pointValue.ToString();

            // Bomba modunda Tabu Cezası slider'ı olmadığı için bu yazıyı gizliyoruz
            txt_preRoundTabu.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
