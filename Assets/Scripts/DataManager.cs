using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public List<WordCard> allCards;
    public string currentLanguage = "en";

    void Awake()
    {
        currentLanguage = NormalizeLanguageCode(PlayerPrefs.GetString("SelectedLanguage", "en"));
        LoadCards(currentLanguage);
    }

    void LoadCards(string langCode)
    {
        // 1. Resources klasöründeki "cards_tr" isimli dosyayý TextAsset olarak yükle
        TextAsset jsonFile = Resources.Load<TextAsset>("cards_"+langCode);

        if (jsonFile != null)
        {
            // 2. Dosya bulunduysa, içeriðini string olarak al
            string jsonString = jsonFile.text;

            // 3. Unity'nin JSON okuyucusu ile bu metni CardCollection sýnýfýna dönüþtür
            CardCollection data = JsonUtility.FromJson<CardCollection>(jsonString);

            // 4. Listemizi doldur
            allCards = data.cards;

            Debug.Log("Baþarýlý! Toplam yüklenen kart sayýsý: " + allCards.Count);

            // Test için ilk kartýn adýný yazdýralým
            Debug.Log("Ýlk Kart: " + allCards[0].word);
        }
        else
        {
            Debug.LogError("JSON dosyasý bulunamadý! Ýsmi 'cards_tr' mi? Resources klasöründe mi?");
        }
    }


    public void ChangeLanguage(string newLang)
    {
        currentLanguage = NormalizeLanguageCode(newLang);
        PlayerPrefs.SetString("SelectedLanguage", currentLanguage);
        LoadCards(currentLanguage);
    }

    private string NormalizeLanguageCode(string langCode)
    {
        if (string.IsNullOrWhiteSpace(langCode)) return "en";

        string code = langCode.Trim().ToLowerInvariant();
        if (code == "tr" || code == "en" || code == "es" || code == "fr")
            return code;

        return "en";
    }
}








