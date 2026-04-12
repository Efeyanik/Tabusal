using UnityEngine;

public class LanguageToggler : MonoBehaviour
{
    public DataManager dataManager;

    void Start()
    {
        string savedLang = PlayerPrefs.GetString("SelectedLanguage", "en");
        if (LocalizationManager.Instance != null)
        {
            LocalizationManager.Instance.SetLanguage(savedLang);
        }
        RefreshAllTexts();
    }

    public void ToggleLanguage()
    {
        string currentLang = LocalizationManager.Instance != null
            ? LocalizationManager.Instance.CurrentLanguageCode
            : PlayerPrefs.GetString("SelectedLanguage", "en");

        string newLang;
        switch (currentLang)
        {
            case "en":
                newLang = "tr";
                break;
            case "tr":
                newLang = "es";
                break;
            default:
                newLang = "en";
                break;
        }

        if (dataManager != null) dataManager.ChangeLanguage(newLang);
        if (LocalizationManager.Instance != null) LocalizationManager.Instance.SetLanguage(newLang);

        RefreshAllTexts();

        Debug.Log("Oyun Dili Değiştirildi: " + newLang.ToUpper());
    }

    private void RefreshAllTexts()
    {
        LocalizedText[] allLocalizedTexts = FindObjectsByType<LocalizedText>(FindObjectsSortMode.None);
        foreach (LocalizedText locText in allLocalizedTexts)
        {
            locText.UpdateText();
        }

        // LocalizedText olmayan dinamik alanları da yenile
        MainMenuManager[] menus = FindObjectsByType<MainMenuManager>(FindObjectsSortMode.None);
        foreach (MainMenuManager menu in menus)
        {
            menu.RefreshLocalizedDynamicTexts();
        }
    }
}