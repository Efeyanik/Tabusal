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
        float timeValue = PlayerPrefs.GetFloat("TimeValue", 60f);
        float passValue = PlayerPrefs.GetFloat("PassValue", 3f);
        float tabuValue = PlayerPrefs.GetFloat("TabuValue", 3f);
        float pointValue = PlayerPrefs.GetFloat("PointValue", 30f);


        txt_preRoundPass.text = "Pas Hakkı : " + passValue.ToString();
        txt_preRoundPoint.text = "Hedef Puan : " + pointValue.ToString();
        txt_preRoundTabu.text = "Tabu Cezası : " + tabuValue.ToString();
        txt_preRoundTime.text = "Süre : " + timeValue.ToString();


         if (GameManager.isTeamATurn)
         {
             starterTeam.text = GameSettings.TeamAName;
         }
         else
         {
             starterTeam.text = GameSettings.TeamBName;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
