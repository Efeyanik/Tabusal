using System;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    // Singleton yapısı: Bu sayede diğer tüm scriptler bu beyni kolayca bulabilir
    public static LocalizationManager Instance;

    public event Action OnLanguageChanged;
    public string CurrentLanguageCode { get; private set; } = "en";

    private Dictionary<string, string> currentDictionary;


    private Dictionary<string, string> dictTR = new Dictionary<string, string>()
    {   //anamenu
        { "BTN_PLAY", "OYNA" },
        { "BTN_CONTINUE", "DEVAM ET" },
        { "BTN_RESTART", "TEKRAR OYNA" },
        { "BTN_TRUE", "DOĞRU" },
        { "BTN_TABOO", "TABU" },
        { "BTN_PASS", "PAS" },
        { "TXT_STANDARD_MODE", "Standart Mod" },
        { "TXT_BOMB_MODE", "Bomba Modu" },
        { "BTN_CHANGELANGUAGE","DİLİ DEĞİŞTİR"  },
        //ayarlar
        { "BTN_SET_STANDARD", "Standart" },
        { "BTN_SET_BOMB", "Bomba" },
        { "BTN_BACK", "ANA MENÜ" },
         
        // Standart Mod Ayarları
        { "SET_TIME", "Tur Süresi" },
        { "SET_PASS", "Pas Hakkı" },
        { "SET_TABU", "Tabu Cezası" },
        { "SET_POINT", "Hedef Puan" },

        // Bomba Modu Ayarları
        { "SET_BOMB_RULES", "Başlangıç Kuralı" },
        { "SET_BOMB_MIN_TIME", "Min. Süre" },
        { "SET_BOMB_MAX_TIME", "Max. Süre" },
        { "SET_BOMB_PASS", "Pas Hakkı" },
        { "SET_BOMB_POINT", "Hedef Puan" },

        // --- OYUN ÖNCESİ (PreRoundPanel) ---
        { "BTN_START", "BAŞLA" },
        { "TXT_STARTER", "BAŞLAYACAK TAKIM" },
        { "PH_TEAM_A", "A Takımı" },
        { "PH_TEAM_B", "B Takımı" },



        
        //GAMERULES
        // 1. Sıralı Başlangıç
        { "RULE_SEQ_NAME", "Sıralı Başlangıç" },
        { "RULE_SEQ_DESC", "<color=#FFE100>Sıralı Başlangıç:</color> Her turda farklı bir takım başlar. (A-B-A-B)\nİlk turda A takımı başlar." },
        
        // 2. Rastgele Başlangıç
        { "RULE_RAND_NAME", "Rastgele" },
        { "RULE_RAND_DESC", "<color=#FFE100>Rastgele:</color> Her turda kimin başlayacağı tamamen rastgele belirlenir." },
        
        // 3. Kaybeden Başlar 
        { "RULE_LOSER_NAME", "Kaybeden Başlar" },
        { "RULE_LOSER_DESC", "<color=#FFE100>Kaybeden Başlar:</color> Bombanın patladığı takım bir sonraki tura başlar.\nİlk turda ise rastgele belirlenir." },

        //4.Rekabetçi
        {  "RULE_COMP_NAME", "Rekabetçi"  },
        { "RULE_COMP_DESC", "<color=#FFE100>Rekabetçi:</color> Puanı geride olan takım başlar. Beraberlik durumunda ise rastgele belirlenir." },





        //İNFO YAZISI
        {"TXT_INFO", "Standart mod ile klasik tabu deneyimini en güzel şekilde deneyimleyin.\r\n\r\n\r\nBomba Modu, orijinal tabu mantığı üzerine inşa edilmiş ve Tabu'ya yepyeni bir heyecan katan bir oyun modudur.\r\n\r\n\r\n<color=#FFE100>Bomba Modu Oyun Mantığı :</color>\r\n-Süre belli değildir.\r\n\r\n-Doğru bilindiğinde sıra rakip takıma geçer.\r\n\r\n-Süre bittiğinde anlatamamış takım ya da tabu yapmış olan takım o turu kaybeder ve rakip takım 1 puan kazanır.\r\n\r\n-Kazanma puanına ulaşan takım oyunu kazanır. \r\n\r\n\r\nOyun ayarlarından oyununuzu özelleştirmeyi unutmayınız.\r\n" },
        
        // --- DİNAMİK YAZILAR (GameManager'dan gelenler) ---
        { "TXT_TEAM_A", "A Takımı" },
        { "TXT_TEAM_B", "B Takımı" },
        { "UI_EXTRA_TIME", "UZATMALAR!\nYeni Hedef: " },
        { "UI_MUST_PASS", " Puanı Geçmelisin)" },
        { "UI_LAST_CHANCE", "<color=#FFE100>SON ŞANS!</color>" },
        { "UI_NEXT_TEAM", "SIRADAKİ TAKIM" },
        { "TXT_INTERANSWER_NEXTTEAM", "SIRADAKİ TAKIM" },
        { "TXT_TIME_IS_UP", "BOMBA PATLADI" },
        { "TXT_WINNER", "KAZANAN" },
        { "UI_SKIPS_ALLOWED", "Pas Hakkı" },
        { "TXT_PAUSED", "DURAKLATILDI" }
    };


    private Dictionary<string, string> dictEN = new Dictionary<string, string>()
    {   //anamenu
        { "BTN_PLAY", "PLAY" },
        { "BTN_CONTINUE", "CONTINUE" },
        { "BTN_RESTART", "RESTART" },
        { "BTN_TRUE", "TRUE" },
        { "BTN_TABOO", "TABOO" },
        { "BTN_PASS", "PASS" },
        { "TXT_STANDARD_MODE", "Standard Mode" },
        { "TXT_BOMB_MODE", "Bomb Mode" },
        { "BTN_CHANGELANGUAGE","CHANGE LANGUAGE" },
        //ayarlar
        { "BTN_SET_STANDARD", "Standard" },
        { "BTN_SET_BOMB", "Bomb" },
        { "BTN_BACK", "MAIN MENU" },

        // Standart Mod Ayarları
        { "SET_TIME", "Round Time" },
        { "SET_PASS", "Skips Allowed" },
        { "SET_TABU", "Taboo Penalty" },
        { "SET_POINT", "Target Score" },

        // Bomba Modu Ayarları
        { "SET_BOMB_RULES", "Starting Rule" },
        { "SET_BOMB_MIN_TIME", "Min Time" },
        { "SET_BOMB_MAX_TIME", "Max Time" },
        { "SET_BOMB_PASS", "Skips Allowed" },
        { "SET_BOMB_POINT", "Target Score" },

        // --- OYUN ÖNCESİ ---
        { "BTN_START", "START" },
        { "TXT_STARTER", "STARTING TEAM" },
        { "PH_TEAM_A", "Team A" },
        { "PH_TEAM_B", "Team B" },


        //GAMERULES
        // 1. Sequential Start
        { "RULE_SEQ_NAME", "Sequential Start" },
        { "RULE_SEQ_DESC", "<color=#FFE100>Sequential Start:</color> A different team starts each round. (A-B-A-B)\nTeam A starts the first round." },

        // 2. Random Start
        { "RULE_RAND_NAME", "Random" },
        { "RULE_RAND_DESC", "<color=#FFE100>Random:</color> The starting team for each round is determined entirely at random." },

        // 3. Loser Starts
        { "RULE_LOSER_NAME", "Loser Starts" },
        { "RULE_LOSER_DESC", "<color=#FFE100>Loser Starts:</color> The team that the bomb explodes on starts the next round.\nThe first round is determined at random." },

        // 4. Competitive
        { "RULE_COMP_NAME", "Competitive" },
        { "RULE_COMP_DESC", "<color=#FFE100>Competitive:</color> The team with fewer points starts the round. In case of a tie, it is determined at random." },




        //İNFO YAZISI
        {"TXT_INFO","Experience the classic taboo gameplay at its finest with the Standard Mode.\\r\\n\\r\\n\\r\\nBomb Mode is built on the original taboo mechanics, adding a brand new level of thrill to the game.\\r\\n\\r\\n\\r\\n<color=#FFE100>Bomb Mode Rules :</color>\\r\\n- The exact time remaining is unknown.\\r\\n\\r\\n- When a word is guessed correctly, the turn passes to the opposing team.\\r\\n\\r\\n- If the time runs out, the team currently explaining or the team that makes a taboo mistake loses the round, giving the opposing team 1 point.\\r\\n\\r\\n- The first team to reach the target score wins the game. \\r\\n\\r\\n\\r\\nDon't forget to customize your match from the game settings.\\r\\n\"" },




        // --- DİNAMİK YAZILAR ---
        { "TXT_TEAM_A", "Team A" },
        { "TXT_TEAM_B", "Team B" },
        { "UI_EXTRA_TIME", "OVERTIME!\nNew Target: " },
        { "UI_MUST_PASS", " Points to Win)" },
        { "UI_LAST_CHANCE", "<color=#FFE100>LAST CHANCE!</color>" },
        { "UI_NEXT_TEAM", "NEXT TEAM" },
        { "TXT_INTERANSWER_NEXTTEAM", "Next Team" },
        { "TXT_TIME_IS_UP", "BOMB EXPLODED" },
        { "TXT_WINNER", "WINNER" },
        { "UI_SKIPS_ALLOWED", "Skips Allowed" },
        { "TXT_PAUSED", "PAUSED" }
    };

    void Awake()
    {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
        else { Destroy(gameObject); return; }

        // Sahne açılır açılmaz kayıtlı dili uygula
        string savedLang = PlayerPrefs.GetString("SelectedLanguage", "en");
        SetLanguage(savedLang);
    }

    // Seçilen dile göre sözlüğü değiştirir
    public void SetLanguage(string langCode)
    {
        string normalized = string.IsNullOrWhiteSpace(langCode) ? "en" : langCode.Trim().ToLowerInvariant();
        CurrentLanguageCode = (normalized == "tr") ? "tr" : "en";
        currentDictionary = (CurrentLanguageCode == "tr") ? dictTR : dictEN;
        PlayerPrefs.SetString("SelectedLanguage", CurrentLanguageCode);
        PlayerPrefs.Save();
        OnLanguageChanged?.Invoke();
    }

    // Verilen anahtarın (Örn: BTN_PLAY) o anki dildeki karşılığını döndürür
    public string GetText(string key)
    {
        if (currentDictionary != null && currentDictionary.ContainsKey(key))
            return currentDictionary[key];

        return key;
    }
}