using UnityEngine;
using TMPro;
using UnityEngine.EventSystems; // Tıklama olaylarını algılamak için gerekli

public class HidePlaceholder : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [Header("Placeholder Localize Key")]
    public string placeholderKey;

    private TMP_InputField inputField;
    private TMP_Text placeholderText;

    void Awake()
    {
        inputField = GetComponent<TMP_InputField>();
        if (inputField != null)
        {
            placeholderText = inputField.placeholder as TMP_Text;

            inputField.onSelect.AddListener(HandleSelect);
            inputField.onDeselect.AddListener(HandleDeselect);
            inputField.onValueChanged.AddListener(HandleValueChanged);
        }
    }

    void OnEnable()
    {
        if (LocalizationManager.Instance != null)
            LocalizationManager.Instance.OnLanguageChanged += UpdatePlaceholderText;

        UpdatePlaceholderText();
    }

    void OnDisable()
    {
        if (LocalizationManager.Instance != null)
            LocalizationManager.Instance.OnLanguageChanged -= UpdatePlaceholderText;
    }

    void OnDestroy()
    {
        if (inputField != null)
        {
            inputField.onSelect.RemoveListener(HandleSelect);
            inputField.onDeselect.RemoveListener(HandleDeselect);
            inputField.onValueChanged.RemoveListener(HandleValueChanged);
        }
    }

    // Kutuya tıklandığında (odaklanıldığında) anında çalışır
    public void OnSelect(BaseEventData eventData)
    {
        SetPlaceholderVisible(false);
    }

    // Kutudan çıkıldığında (başka yere tıklandığında) çalışır
    public void OnDeselect(BaseEventData eventData)
    {
        SetPlaceholderVisible(inputField != null && string.IsNullOrEmpty(inputField.text));
    }

    private void HandleSelect(string _)
    {
        SetPlaceholderVisible(false);
    }

    private void HandleDeselect(string _)
    {
        SetPlaceholderVisible(inputField != null && string.IsNullOrEmpty(inputField.text));
    }

    private void HandleValueChanged(string value)
    {
        if (inputField == null) return;
        SetPlaceholderVisible(string.IsNullOrEmpty(value) && !inputField.isFocused);
    }

    private void SetPlaceholderVisible(bool visible)
    {
        if (inputField != null && inputField.placeholder != null)
        {
            inputField.placeholder.gameObject.SetActive(visible);
        }
    }

    public void UpdatePlaceholderText()
    {
        if (string.IsNullOrEmpty(placeholderKey)) return;
        if (placeholderText == null || LocalizationManager.Instance == null) return;

        placeholderText.text = LocalizationManager.Instance.GetText(placeholderKey);
    }

    
}