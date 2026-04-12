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

    private Dictionary<string, string> dictES = new Dictionary<string, string>()
    {   // main menu
        { "BTN_PLAY", "JUGAR" },
        { "BTN_CONTINUE", "CONTINUAR" },
        { "BTN_RESTART", "REINICIAR" },
        { "BTN_TRUE", "CORRECTO" },
        { "BTN_TABOO", "TABÚ" },
        { "BTN_PASS", "PASAR" },
        { "TXT_STANDARD_MODE", "Modo Estándar" },
        { "TXT_BOMB_MODE", "Modo Bomba" },
        { "BTN_CHANGELANGUAGE","CAMBIAR IDIOMA" },
        // settings
        { "BTN_SET_STANDARD", "Estándar" },
        { "BTN_SET_BOMB", "Bomba" },
        { "BTN_BACK", "MENÚ PRINCIPAL" },

        // Standard mode settings
        { "SET_TIME", "Tiempo de Ronda" },
        { "SET_PASS", "Pases Permitidos" },
        { "SET_TABU", "Penalización Tabú" },
        { "SET_POINT", "Puntuación Objetivo" },

        // Bomb mode settings
        { "SET_BOMB_RULES", "Regla de Inicio" },
        { "SET_BOMB_MIN_TIME", "Tiempo Mín." },
        { "SET_BOMB_MAX_TIME", "Tiempo Máx." },
        { "SET_BOMB_PASS", "Pases Permitidos" },
        { "SET_BOMB_POINT", "Puntuación Objetivo" },

        // pre-round
        { "BTN_START", "INICIAR" },
        { "TXT_STARTER", "EQUIPO INICIAL" },
        { "PH_TEAM_A", "Equipo A" },
        { "PH_TEAM_B", "Equipo B" },

        // game rules
        { "RULE_SEQ_NAME", "Inicio Secuencial" },
        { "RULE_SEQ_DESC", "<color=#FFE100>Inicio Secuencial:</color> Cada ronda comienza un equipo diferente. (A-B-A-B)\nEl Equipo A empieza la primera ronda." },
        { "RULE_RAND_NAME", "Aleatorio" },
        { "RULE_RAND_DESC", "<color=#FFE100>Aleatorio:</color> El equipo inicial de cada ronda se decide completamente al azar." },
        { "RULE_LOSER_NAME", "Empieza el Perdedor" },
        { "RULE_LOSER_DESC", "<color=#FFE100>Empieza el Perdedor:</color> El equipo donde explota la bomba comienza la siguiente ronda.\nLa primera ronda se determina al azar." },
        { "RULE_COMP_NAME", "Competitivo" },
        { "RULE_COMP_DESC", "<color=#FFE100>Competitivo:</color> Empieza el equipo con menos puntos. En caso de empate, se decide al azar." },

        // info text
        {"TXT_INFO", "Disfruta la experiencia clásica de tabú con el Modo Estándar.\\r\\n\\r\\n\\r\\nEl Modo Bomba está construido sobre la mecánica original de tabú y agrega una nueva emoción al juego.\\r\\n\\r\\n\\r\\n<color=#FFE100>Reglas del Modo Bomba:</color>\\r\\n- El tiempo exacto restante es desconocido.\\r\\n\\r\\n- Cuando una palabra se adivina correctamente, el turno pasa al equipo rival.\\r\\n\\r\\n- Si el tiempo se acaba, el equipo que está explicando o el que comete tabú pierde la ronda y el rival gana 1 punto.\\r\\n\\r\\n- El primer equipo que alcance la puntuación objetivo gana la partida.\\r\\n\\r\\n\\r\\nNo olvides personalizar la partida desde la configuración.\\r\\n" },

        // dynamic texts
        { "TXT_TEAM_A", "Equipo A" },
        { "TXT_TEAM_B", "Equipo B" },
        { "UI_EXTRA_TIME", "¡TIEMPO EXTRA!\nNuevo Objetivo: " },
        { "UI_MUST_PASS", " Puntos para Ganar)" },
        { "UI_LAST_CHANCE", "<color=#FFE100>¡ÚLTIMA OPORTUNIDAD!</color>" },
        { "UI_NEXT_TEAM", "SIGUIENTE EQUIPO" },
        { "TXT_INTERANSWER_NEXTTEAM", "Siguiente Equipo" },
        { "TXT_TIME_IS_UP", "LA BOMBA EXPLOTÓ" },
        { "TXT_WINNER", "GANADOR" },
        { "UI_SKIPS_ALLOWED", "Pases Permitidos" },
        { "TXT_PAUSED", "PAUSADO" }
    };

    private Dictionary<string, string> dictFR = new Dictionary<string, string>()
    {   // menu principal
        { "BTN_PLAY", "JOUER" },
        { "BTN_CONTINUE", "CONTINUER" },
        { "BTN_RESTART", "RECOMMENCER" },
        { "BTN_TRUE", "VRAI" },
        { "BTN_TABOO", "TABOU" },
        { "BTN_PASS", "PASSER" },
        { "TXT_STANDARD_MODE", "Mode Standard" },
        { "TXT_BOMB_MODE", "Mode Bombe" },
        { "BTN_CHANGELANGUAGE","CHANGER LA LANGUE" },
        // paramètres
        { "BTN_SET_STANDARD", "Standard" },
        { "BTN_SET_BOMB", "Bombe" },
        { "BTN_BACK", "MENU PRINCIPAL" },

        // paramètres mode standard
        { "SET_TIME", "Durée de Manche" },
        { "SET_PASS", "Passes Autorisées" },
        { "SET_TABU", "Pénalité Tabou" },
        { "SET_POINT", "Score Cible" },

        // paramètres mode bombe
        { "SET_BOMB_RULES", "Règle de Départ" },
        { "SET_BOMB_MIN_TIME", "Durée Min." },
        { "SET_BOMB_MAX_TIME", "Durée Max." },
        { "SET_BOMB_PASS", "Passes Autorisées" },
        { "SET_BOMB_POINT", "Score Cible" },

        // pré-manche
        { "BTN_START", "DÉMARRER" },
        { "TXT_STARTER", "ÉQUIPE DE DÉPART" },
        { "PH_TEAM_A", "Équipe A" },
        { "PH_TEAM_B", "Équipe B" },

        // règles de jeu
        { "RULE_SEQ_NAME", "Départ Séquentiel" },
        { "RULE_SEQ_DESC", "<color=#FFE100>Départ Séquentiel:</color> Une équipe différente commence chaque manche. (A-B-A-B)\nL'équipe A commence la première manche." },
        { "RULE_RAND_NAME", "Aléatoire" },
        { "RULE_RAND_DESC", "<color=#FFE100>Aléatoire:</color> L'équipe qui commence chaque manche est choisie totalement au hasard." },
        { "RULE_LOSER_NAME", "Le Perdant Commence" },
        { "RULE_LOSER_DESC", "<color=#FFE100>Le Perdant Commence:</color> L'équipe sur laquelle la bombe explose commence la manche suivante.\nLa première manche est déterminée aléatoirement." },
        { "RULE_COMP_NAME", "Compétitif" },
        { "RULE_COMP_DESC", "<color=#FFE100>Compétitif:</color> L'équipe avec le moins de points commence. En cas d'égalité, c'est aléatoire." },

        // texte d'info
        {"TXT_INFO", "Profitez de l'expérience classique du tabou avec le Mode Standard.\\r\\n\\r\\n\\r\\nLe Mode Bombe est basé sur les mécaniques originales du tabou et apporte une nouvelle intensité au jeu.\\r\\n\\r\\n\\r\\n<color=#FFE100>Règles du Mode Bombe :</color>\\r\\n- Le temps exact restant est inconnu.\\r\\n\\r\\n- Quand un mot est correctement deviné, le tour passe à l'équipe adverse.\\r\\n\\r\\n- Si le temps est écoulé, l'équipe qui explique ou celle qui fait une faute tabou perd la manche et l'équipe adverse gagne 1 point.\\r\\n\\r\\n- La première équipe qui atteint le score cible gagne la partie.\\r\\n\\r\\n\\r\\nN'oubliez pas de personnaliser votre partie dans les paramètres.\\r\\n" },

        // textes dynamiques
        { "TXT_TEAM_A", "Équipe A" },
        { "TXT_TEAM_B", "Équipe B" },
        { "UI_EXTRA_TIME", "PROLONGATIONS !\nNouvel Objectif : " },
        { "UI_MUST_PASS", " Points pour Gagner)" },
        { "UI_LAST_CHANCE", "<color=#FFE100>DERNIÈRE CHANCE !</color>" },
        { "UI_NEXT_TEAM", "ÉQUIPE SUIVANTE" },
        { "TXT_INTERANSWER_NEXTTEAM", "Équipe Suivante" },
        { "TXT_TIME_IS_UP", "LA BOMBE A EXPLOSÉ" },
        { "TXT_WINNER", "GAGNANT" },
        { "UI_SKIPS_ALLOWED", "Passes Autorisées" },
        { "TXT_PAUSED", "EN PAUSE" }
    };

    void Awake()
    {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
        else { Destroy(this); return; }

        // Sahne açılır açılmaz kayıtlı dili uygula
        string savedLang = PlayerPrefs.GetString("SelectedLanguage", "en");
        SetLanguage(savedLang);
    }

    // Seçilen dile göre sözlüğü değiştirir
    public void SetLanguage(string langCode)
    {
        string normalized = string.IsNullOrWhiteSpace(langCode) ? "en" : langCode.Trim().ToLowerInvariant();
        switch (normalized)
        {
            case "tr":
                CurrentLanguageCode = "tr";
                currentDictionary = dictTR;
                break;
            case "es":
                CurrentLanguageCode = "es";
                currentDictionary = dictES;
                break;
            case "fr":
                CurrentLanguageCode = "fr";
                currentDictionary = dictFR;
                break;
            default:
                CurrentLanguageCode = "en";
                currentDictionary = dictEN;
                break;
        }
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