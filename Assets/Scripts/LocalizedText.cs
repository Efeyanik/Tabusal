using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class LocalizedText : MonoBehaviour
{
    [Header("Sözlükteki Anahtar Kelime")]
    public string key; // Örn: BTN_PLAY yazacaksın

    private TextMeshProUGUI textComponent;

    void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        if (LocalizationManager.Instance != null)
            LocalizationManager.Instance.OnLanguageChanged += UpdateText;

        UpdateText();
    }

    void OnDisable()
    {
        if (LocalizationManager.Instance != null)
            LocalizationManager.Instance.OnLanguageChanged -= UpdateText;
    }

    // Bu metot çağrıldığında yazı kendini yeni dile göre anında günceller
    public void UpdateText()
    {
        if (textComponent != null && LocalizationManager.Instance != null)
        {
            textComponent.text = LocalizationManager.Instance.GetText(key);
        }
    }
}