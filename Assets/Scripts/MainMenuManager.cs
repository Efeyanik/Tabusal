using UnityEngine;
using UnityEngine.SceneManagement; // Sahne deðiþimi için þart
using TMPro;
using UnityEngine.UI;
using JetBrains.Annotations;
using UnityEditor;
using System.Collections;


public class MainMenuManager : MonoBehaviour
{
    [Header("UI Elemanlarý")]
    public TMP_InputField inputTeamA;
    public TMP_InputField inputTeamB;
    public GameObject menuPanel;
    public GameObject settingsPanel;
    public GameObject preRoundPanel;
    public GameObject standartSettingsPanel;
    public GameObject bombSettingsPanel;
    public Image imgClassicBtn;
    public Image imgBombBtn;
    public Image imgSettingsClasicBtn;
    public Image imgSettingsBombBtn;
    public AudioClip buttonClickSound;
    public AudioSource audioSource;
    public GameObject infoPanel;
    


    [Header("Renk Ayarlarý")]
    public Color activeColor = new Color(1f, 1f, 1f, 1f);       // Tam opak (Parlak)
    public Color inactiveColor = new Color(0.7f, 0.7f, 0.7f, 0.5f); // Yarý saydam ve biraz gri (Sönük)
    public Color bombActiveColor = new Color(1f, 0.5f, 0.5f, 1f); // Bomb modunda aktif renk (örneðin kýrmýzýmsý)
    public Color bombInactiveColor = new Color(0.7f, 0.5f, 0.5f, 0.5f); // Bomb modunda pasif renk (örneðin kýrmýzýmsý ama daha sönük)

    [Header("Slider Referanslarý")]
    public Slider sliderTime;
    public Slider sliderPass;
    public Slider sliderTabu;
    public Slider sliderPoint;

    public Slider sliderBombTimeMin;
    public Slider sliderBombTimeMax;
    public Slider sliderBombPass;
    public Slider sliderBombPoint;


    [Header("Text Referanslarý")]
    public TextMeshProUGUI txtTime;
    public TextMeshProUGUI txtPass;
    public TextMeshProUGUI txtTabu;
    public TextMeshProUGUI txtPoint;

    public TextMeshProUGUI txtBombTimeMin;
    public TextMeshProUGUI txtBombTimeMax;
    public TextMeshProUGUI txtBombPass;
    public TextMeshProUGUI txtBombPoint;

    public TextMeshProUGUI txtDescription;
    public TextMeshProUGUI txtStartingRule;

    [Header("Oyun Ayarlarý vs.")]
    public int startingRuleCounter;
    public GameSettings.BombStartingRule[] bombStartingRules = (GameSettings.BombStartingRule[])System.Enum.GetValues(typeof(GameSettings.BombStartingRule));

    private bool sliderListenersBound;





    void Start()
    {
        ApplyDefaultTeamNamesByLanguage();
        BindSliderListeners();

        startingRuleCounter = PlayerPrefs.GetInt("BombStartingRule", 0);
        SelectClassicMode();
        OpenStandartSettings();
        sliderTime.value = PlayerPrefs.GetFloat("TimeValue", 60f) / 10f;
        sliderPoint.value = PlayerPrefs.GetFloat("PointValue", 50f) / 10f;
        sliderPass.value = PlayerPrefs.GetFloat("PassValue", 3f);
        sliderTabu.value = PlayerPrefs.GetFloat("TabuValue", 3f);
        
        sliderBombTimeMin.value = PlayerPrefs.GetFloat("BombTimeMinValue", 30f) /10f;
        sliderBombTimeMax.value = PlayerPrefs.GetFloat("BombTimeMaxValue", 90f) /10f;
        sliderBombPass.value = PlayerPrefs.GetFloat("BombPassValue", 2f);
        sliderBombPoint.value = PlayerPrefs.GetFloat("BombPointValue", 5f);

        UpdateTimeText(sliderTime.value);
        UpdatePassText(sliderPass.value);
        UpdateTabuText(sliderTabu.value);
        UpdatePointText(sliderPoint.value);

        UpdateBombTimeMinText(sliderBombTimeMin.value);
        UpdateBombTimeMaxText(sliderBombTimeMax.value);
        UpdateBombPassText(sliderBombPass.value);
        UpdateBombPointText(sliderBombPoint.value);
        UpdateStartingRuleText(PlayerPrefs.GetInt("BombStartingRule", 0));

        ColorUtility.TryParseHtmlString("#F00017", out bombActiveColor);
        ColorUtility.TryParseHtmlString("#78010C", out bombInactiveColor);

        RefreshLocalizedDynamicTexts();
        StartCoroutine(RefreshLocalizedDynamicTextsNextFrame());
    }

    void OnDestroy()
    {
        UnbindSliderListeners();
    }

    private IEnumerator RefreshLocalizedDynamicTextsNextFrame()
    {
        yield return null;
        RefreshLocalizedDynamicTexts();
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

        RefreshLocalizedDynamicTexts();
        StartCoroutine(RefreshLocalizedDynamicTextsNextFrame());
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        menuPanel.SetActive(true);
    }




    #region Update method'S



    public void UpdateTimeText(float deger)
    {
        if (txtTime == null) return;
        float gercekSure = deger * 10f;
        txtTime.text = GetLocalized("SET_TIME", "Süre") + " : " + gercekSure;
    }

    public void UpdatePassText(float deger)
    {
        if (txtPass == null) return;
        txtPass.text = GetLocalized("SET_PASS", "Pas Hakký") + " : " + deger;
    }

    public void UpdateTabuText(float deger)
    {
        if (txtTabu == null) return;
        txtTabu.text = GetLocalized("SET_TABU", "Tabu Cezasý") + " : " + deger;
    }

    public void UpdatePointText(float deger)
    {
        if (txtPoint == null) return;
        float gercekPuan = deger * 10f;
        txtPoint.text = GetLocalized("SET_POINT", "Hedef Puan") + " : " + gercekPuan;
    }

    public void UpdateBombTimeMinText(float deger)
    {
        if (txtBombTimeMin == null) return;
        float gercekSure = deger * 10f;
        txtBombTimeMin.text = GetLocalized("SET_BOMB_MIN_TIME", "Min. Süre") + " : " + gercekSure;
    }

    public void UpdateBombTimeMaxText(float deger)
    {
        if (txtBombTimeMax == null) return;
        float gercekSure = deger * 10f;
        txtBombTimeMax.text = GetLocalized("SET_BOMB_MAX_TIME", "Max. Süre") + " : " + gercekSure;
    }

    public void UpdateBombPassText(float deger)
    {
        if (txtBombPass == null) return;
        txtBombPass.text = GetLocalized("SET_BOMB_PASS", "Pas Hakký") + " : " + deger;
    }

    public void UpdateBombPointText(float deger)
    {
        if (txtBombPoint == null) return;
        txtBombPoint.text = GetLocalized("SET_BOMB_POINT", "Hedef Puan") + " : " + deger;
    }

    public void UpdateStartingRuleText(int deger)
    {
        
        if (deger == 0)
        {
            txtDescription.text = GetLocalized("RULE_SEQ_DESC", "Sýralý Baþlangýç");
            txtStartingRule.text = GetLocalized("RULE_SEQ_NAME", "Sýralý Baþlangýç");
        }
        else if (deger == 1)
        {
            txtDescription.text = GetLocalized("RULE_RAND_DESC", "Rastgele");
            txtStartingRule.text = GetLocalized("RULE_RAND_NAME", "Rastgele");
        }
        else if (deger == 2)
        {
            txtDescription.text = GetLocalized("RULE_LOSER_DESC", "Kaybeden Baþlar");
            txtStartingRule.text = GetLocalized("RULE_LOSER_NAME", "Kaybeden Baþlar");
        }
        else if (deger == 3)
        {
            txtDescription.text = GetLocalized("RULE_COMP_DESC", "Rekabetçi");
            txtStartingRule.text = GetLocalized("RULE_COMP_NAME", "Rekabetçi");
        }
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

    public void UseBombSettings()
    {
        float bombTimeMinValue = sliderBombTimeMin.value * 10;
        float bombTimeMaxValue = sliderBombTimeMax.value * 10;
        float bombPassValue = sliderBombPass.value;
        float bombPointValue = sliderBombPoint.value;


        PlayerPrefs.SetFloat("BombTimeMinValue", bombTimeMinValue);
        PlayerPrefs.SetFloat("BombTimeMaxValue", bombTimeMaxValue);
        PlayerPrefs.SetFloat("BombPassValue", bombPassValue);
        PlayerPrefs.SetFloat("BombPointValue", bombPointValue);

        PlayerPrefs.Save();
    }



    public void OpenPreRoundPanel()
    {
        // 1. Ýsimleri Kaydet (Boþsa varsayýlan kalsýn)
        if (!string.IsNullOrEmpty(inputTeamA.text))
            GameSettings.TeamAName = inputTeamA.text;
        else
            GameSettings.TeamAName = GetDefaultTeamAName();

        if (!string.IsNullOrEmpty(inputTeamB.text))
            GameSettings.TeamBName = inputTeamB.text;
        else
            GameSettings.TeamBName = GetDefaultTeamBName();

        
        if (GameSettings.SelectedMode == false) // EÐER BOMBA MODUYSA
        {
            int startingRule = PlayerPrefs.GetInt("BombStartingRule", 0);

            if (startingRule == 0)
            {
                // Sýralý modda ilk tur her zaman A baþlar
                GameSettings.IsTeamAStartingFirst = true;
            }
            else
            {
                // Rastgele, Kaybeden veya Rekabetçi kurallarýnda ÝLK TUR her zaman rastgeledir
                GameSettings.IsTeamAStartingFirst = Random.value > 0.5f;
            }
        }
        else // EÐER KLASÝK MODSA
        {
            // Klasik modda her zaman A takýmý baþlar (kendi mantýðýna göre deðiþtirebilirsin)
            GameSettings.IsTeamAStartingFirst = true;
        }

        // 3. Panelleri Deðiþtir
        menuPanel.SetActive(false);
        preRoundPanel.SetActive(true);
    }


    #region ButtonMethods

    

    public void SelectClassicMode()
    {
        GameSettings.SelectedMode = true;
        imgClassicBtn.color = activeColor;
        imgBombBtn.color = bombInactiveColor;
        Debug.Log("Classic Mode Seçildi: " + GameSettings.SelectedMode);
    }

    public void SelectBombMode()
    {
        GameSettings.SelectedMode = false;
        imgClassicBtn.color = inactiveColor;
        imgBombBtn.color = bombActiveColor;
        Debug.Log("Bomb Mode Seçildi: " + GameSettings.SelectedMode);
    }


    public void OpenStandartSettings()  
    {
        standartSettingsPanel.SetActive(true);
        bombSettingsPanel.SetActive(false);
        imgSettingsClasicBtn.color = activeColor;
        imgSettingsBombBtn.color = inactiveColor;

        RefreshLocalizedDynamicTexts();
        StartCoroutine(RefreshLocalizedDynamicTextsNextFrame());
    }

    public void OpenBombSettings()
    {
        standartSettingsPanel.SetActive(false);
        bombSettingsPanel.SetActive(true);
        imgSettingsBombBtn.color = activeColor;
        imgSettingsClasicBtn.color = inactiveColor;

        RefreshLocalizedDynamicTexts();
        StartCoroutine(RefreshLocalizedDynamicTextsNextFrame());
    }


    public void LeftArrow()
    {
        switch (startingRuleCounter)
        {
            case 0:
                startingRuleCounter = bombStartingRules.Length - 1; // 0-1-2-3 
                break;

            case 1:
                startingRuleCounter -= 1;
                break;

            case 2:
                startingRuleCounter -= 1;
                break;
            case 3: 
                startingRuleCounter -= 1;
                break;
        }

        UpdateStartingRuleText(startingRuleCounter);


        PlayerPrefs.SetInt("BombStartingRule", startingRuleCounter);
        PlayerPrefs.Save();


    }

    public void RightArrow()
    {
        
        switch(startingRuleCounter)
        {
            case 0:
                startingRuleCounter += 1;
                break;
            case 1:
                startingRuleCounter += 1;
                break;
            case 2:
                startingRuleCounter += 1;
                break;
            case 3: 
                startingRuleCounter = 0; // 0-1-2-3 
                break;
        }

        UpdateStartingRuleText(startingRuleCounter);


        PlayerPrefs.SetInt("BombStartingRule", startingRuleCounter);
        PlayerPrefs.Save();
    }

    public void PlayButtonSound()
    {
        audioSource.volume = 0.6f;
        audioSource.PlayOneShot(buttonClickSound);
    }

    public void OpenInfoPanel()
    {
        infoPanel.SetActive(true);
    }

    public void CloseInfoPanel()
    {
        infoPanel.SetActive(false);
    }

    #endregion
    public void RefreshLocalizedDynamicTexts()
    {
        if (sliderTime != null) UpdateTimeText(sliderTime.value);
        if (sliderPass != null) UpdatePassText(sliderPass.value);
        if (sliderTabu != null) UpdateTabuText(sliderTabu.value);
        if (sliderPoint != null) UpdatePointText(sliderPoint.value);

        if (sliderBombTimeMin != null) UpdateBombTimeMinText(sliderBombTimeMin.value);
        if (sliderBombTimeMax != null) UpdateBombTimeMaxText(sliderBombTimeMax.value);
        if (sliderBombPass != null) UpdateBombPassText(sliderBombPass.value);
        if (sliderBombPoint != null) UpdateBombPointText(sliderBombPoint.value);

        UpdateStartingRuleText(startingRuleCounter);
        UpdateTeamInputPlaceholders();
    }

    private void UpdateTeamInputPlaceholders()
    {
        if (LocalizationManager.Instance == null) return;

        if (inputTeamA != null && inputTeamA.placeholder is TMP_Text placeholderA)
            placeholderA.text = LocalizationManager.Instance.GetText("PH_TEAM_A");

        if (inputTeamB != null && inputTeamB.placeholder is TMP_Text placeholderB)
            placeholderB.text = LocalizationManager.Instance.GetText("PH_TEAM_B");
    }

    private void ApplyDefaultTeamNamesByLanguage()
    {
        GameSettings.TeamAName = GetDefaultTeamAName();
        GameSettings.TeamBName = GetDefaultTeamBName();
    }

    private string GetDefaultTeamAName()
    {
        return LocalizationManager.Instance != null
            ? LocalizationManager.Instance.GetText("TXT_TEAM_A")
            : "Team A";
    }

    private string GetDefaultTeamBName()
    {
        return LocalizationManager.Instance != null
            ? LocalizationManager.Instance.GetText("TXT_TEAM_B")
            : "Team B";
    }

    private string GetLocalized(string key, string fallback)
    {
        return LocalizationManager.Instance != null
            ? LocalizationManager.Instance.GetText(key)
            : fallback;
    }

    private void BindSliderListeners()
    {
        if (sliderListenersBound) return;

        if (sliderTime != null) sliderTime.onValueChanged.AddListener(UpdateTimeText);
        if (sliderPass != null) sliderPass.onValueChanged.AddListener(UpdatePassText);
        if (sliderTabu != null) sliderTabu.onValueChanged.AddListener(UpdateTabuText);
        if (sliderPoint != null) sliderPoint.onValueChanged.AddListener(UpdatePointText);

        if (sliderBombTimeMin != null) sliderBombTimeMin.onValueChanged.AddListener(UpdateBombTimeMinText);
        if (sliderBombTimeMax != null) sliderBombTimeMax.onValueChanged.AddListener(UpdateBombTimeMaxText);
        if (sliderBombPass != null) sliderBombPass.onValueChanged.AddListener(UpdateBombPassText);
        if (sliderBombPoint != null) sliderBombPoint.onValueChanged.AddListener(UpdateBombPointText);

        sliderListenersBound = true;
    }

    private void UnbindSliderListeners()
    {
        if (!sliderListenersBound) return;

        if (sliderTime != null) sliderTime.onValueChanged.RemoveListener(UpdateTimeText);
        if (sliderPass != null) sliderPass.onValueChanged.RemoveListener(UpdatePassText);
        if (sliderTabu != null) sliderTabu.onValueChanged.RemoveListener(UpdateTabuText);
        if (sliderPoint != null) sliderPoint.onValueChanged.RemoveListener(UpdatePointText);

        if (sliderBombTimeMin != null) sliderBombTimeMin.onValueChanged.RemoveListener(UpdateBombTimeMinText);
        if (sliderBombTimeMax != null) sliderBombTimeMax.onValueChanged.RemoveListener(UpdateBombTimeMaxText);
        if (sliderBombPass != null) sliderBombPass.onValueChanged.RemoveListener(UpdateBombPassText);
        if (sliderBombPoint != null) sliderBombPoint.onValueChanged.RemoveListener(UpdateBombPointText);

        sliderListenersBound = false;
    }


}