using UnityEngine;
using UnityEngine.SceneManagement; // Sahne deðiþimi için þart
using TMPro;
using UnityEngine.UI;
using JetBrains.Annotations;
using UnityEditor;


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


    [Header("Renk Ayarlarý")]
    public Color activeColor = new Color(1f, 1f, 1f, 1f);       // Tam opak (Parlak)
    public Color inactiveColor = new Color(0.7f, 0.7f, 0.7f, 0.5f); // Yarý saydam ve biraz gri (Sönük)

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





    void Start()
    {
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

    public void UpdateBombTimeMinText(float deger)
    {
        float gercekSure = deger * 10f;
        txtBombTimeMin.text = "Minimum Süre : " + gercekSure.ToString();
    }

    public void UpdateBombTimeMaxText(float deger)
    {
        float gercekSure = deger * 10f;
        txtBombTimeMax.text = "Maksimum Süre : " + gercekSure.ToString();
    }

    public void UpdateBombPassText(float deger)
    {
        txtBombPass.text = "Pas Hakký : " + deger.ToString();
    }

    public void UpdateBombPointText(float deger)
    {
        txtBombPoint.text = "Hedef Puan : " + deger.ToString();
    }

    public void UpdateStartingRuleText(int deger)
    {
        
        if (deger == 0)
        {
            txtDescription.text = "<color=#FFE100>Sýralý Baþlangýç:</color> Her turda farklý bir takým baþlar. (A-B-A-B)\nÝlk turda A takýmý baþlar.";
            txtStartingRule.text = "Sýralý Baþlangýç";
        }
        else if (deger == 1)
        {
            txtDescription.text = "<color=#FFE100>Rastgele:</color> Her turda kimin baþlayacaðý tamamen rastgele belirlenir.";
            txtStartingRule.text = "Rastgele";
        }
        else if (deger == 2)
        {
            txtDescription.text = "<color=#FFE100>Kaybeden Baþlar:</color> Bombanýn patladýðý takým bir sonraki tura baþlar.\nÝlk turda ise rastgele belirlenir.";
            txtStartingRule.text = "Kaybeden Baþlar";
        }
        else if (deger == 3)
        {
            txtDescription.text = "<color=#FFE100>Rekabetçi:</color> Puaný geride olan takým baþlar. Beraberlik durumunda ise rastgele belirlenir.";
            txtStartingRule.text = "Rekabetçi";
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

        if (!string.IsNullOrEmpty(inputTeamB.text))
            GameSettings.TeamBName = inputTeamB.text;

        
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

}