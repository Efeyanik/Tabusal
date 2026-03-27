using UnityEngine;
using UnityEngine.SceneManagement; // Sahne deðiþimi için þart
using TMPro;
using UnityEngine.UI;
using JetBrains.Annotations;


public class MainMenuManager : MonoBehaviour
{
    [Header("UI Elemanlarý")]
    public TMP_InputField inputTeamA;
    public TMP_InputField inputTeamB;
    public TMP_Dropdown dropdownMode;
    public GameObject menuPanel;
    public GameObject settingsPanel;
    public GameObject preRoundPanel;
    public GameObject standartSettingsPanel;
    public GameObject bombSettingsPanel;
    public Image imgClassicBtn;
    public Image imgBombBtn;
    public Image imgSettingsClasicBtn;
    public Image imgSettingsBombBtn;

    [Header("Renk Ayarlarý")]
    public Color activeColor = new Color(1f, 1f, 1f, 1f);       // Tam opak (Parlak)
    public Color inactiveColor = new Color(0.7f, 0.7f, 0.7f, 0.5f); // Yarý saydam ve biraz gri (Sönük)

    [Header("Slider Referanslarý")]
    public Slider sliderTime;
    public Slider sliderPass;
    public Slider sliderTabu;
    public Slider sliderPoint;

    [Header("Text Referanslarý")]
    public TextMeshProUGUI txtTime;
    public TextMeshProUGUI txtPass;
    public TextMeshProUGUI txtTabu;
    public TextMeshProUGUI txtPoint;


    void Start()
    {
        
        SelectClassicMode();
        OpenStandartSettings();
        sliderTime.value = PlayerPrefs.GetFloat("TimeValue", 60f) / 10f;
        sliderPoint.value = PlayerPrefs.GetFloat("PointValue", 50f) / 10f;

        
        sliderPass.value = PlayerPrefs.GetFloat("PassValue", 3f);
        sliderTabu.value = PlayerPrefs.GetFloat("TabuValue", 3f);

        UpdateTimeText(sliderTime.value);
        UpdatePassText(sliderPass.value);
        UpdateTabuText(sliderTabu.value);
        UpdatePointText(sliderPoint.value);
    }


    public void StartGame()
    {
        

        
        if (GameSettings.SelectedMode == true)
            SceneManager.LoadScene("SampleScene");
        else
            SceneManager.LoadScene("BombMode");

     
    }


    public void OpenSettings()
    {
        menuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    #region Update method'S

    

    public void UpdateTimeText(float deger)
    {
        float gercekSure = deger * 10f;
        txtTime.text = "Süre : " + gercekSure.ToString();
    }

    
    public void UpdatePassText(float deger)
    {
        txtPass.text = "Pas Hakký : " + deger.ToString();
    }

    
    public void UpdateTabuText(float deger)
    {
        txtTabu.text = "Tabu Cezasý : " + deger.ToString();
    }

    
    public void UpdatePointText(float deger)
    {
        float gercekPuan = deger * 10f;
        txtPoint.text = "Hedef Puan : " + gercekPuan.ToString();
    }

    #endregion


    public void UseSettings()
    {
        float timeValue = sliderTime.value * 10;
        float passValue = sliderPass.value;
        float tabuValue = sliderTabu.value;
        float pointValue = sliderPoint.value * 10;


        PlayerPrefs.SetFloat("TimeValue", timeValue);
        PlayerPrefs.SetFloat("PassValue", passValue);
        PlayerPrefs.SetFloat("TabuValue", tabuValue);
        PlayerPrefs.SetFloat("PointValue", pointValue);

        PlayerPrefs.Save();
    } 
      
    public void OpenPreRoundPanel()
    {
        // 1. Ýsimleri Kaydet (Boþsa varsayýlan kalsýn)
        if (!string.IsNullOrEmpty(inputTeamA.text))
            GameSettings.TeamAName = inputTeamA.text;

        if (!string.IsNullOrEmpty(inputTeamB.text))
            GameSettings.TeamBName = inputTeamB.text;

        menuPanel.SetActive(false);
        preRoundPanel.SetActive(true);
    }

    public void SelectClassicMode()
    {
        GameSettings.SelectedMode = true;
        imgClassicBtn.color = activeColor;
        imgBombBtn.color = inactiveColor;
        Debug.Log("Classic Mode Seçildi: " + GameSettings.SelectedMode);
    }

    public void SelectBombMode()
    {
        GameSettings.SelectedMode = false;
        imgClassicBtn.color = inactiveColor;
        imgBombBtn.color = activeColor;
        Debug.Log("Bomb Mode Seçildi: " + GameSettings.SelectedMode);
    }


    public void OpenStandartSettings()  
    {
        standartSettingsPanel.SetActive(true);
        bombSettingsPanel.SetActive(false);
        imgSettingsClasicBtn.color = activeColor;
        imgSettingsBombBtn.color = inactiveColor;
    }

    public void OpenBombSettings()
    {
        standartSettingsPanel.SetActive(false);
        bombSettingsPanel.SetActive(true);
        imgSettingsBombBtn.color = activeColor;
        imgSettingsClasicBtn.color = inactiveColor;
    }
}