using UnityEngine;
using UnityEngine.SceneManagement; // Sahne deðiþimi için þart
using TMPro; // TextMeshPro elemanlarý için

public class MainMenuManager : MonoBehaviour
{
    [Header("UI Elemanlarý")]
    public TMP_InputField inputTeamA;
    public TMP_InputField inputTeamB;
    public TMP_Dropdown dropdownMode;

    public void StartGame()
    {
        // 1. Ýsimleri Kaydet (Boþsa varsayýlan kalsýn)
        if (!string.IsNullOrEmpty(inputTeamA.text))
            GameSettings.TeamAName = inputTeamA.text;

        if (!string.IsNullOrEmpty(inputTeamB.text))
            GameSettings.TeamBName = inputTeamB.text;

        // 2. Modu Kaydet
        // Dropdown'da 0. seçenek Klasik, 1. seçenek Bomba olsun dedik
        if (dropdownMode.value == 0)
            GameSettings.SelectedMode = GameMode.Classic;
        else
            GameSettings.SelectedMode = GameMode.Bomb;

        // 3. Oyun Sahnesini Yükle
        
        SceneManager.LoadScene("SampleScene");
    }
}