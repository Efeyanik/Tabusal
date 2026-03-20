using UnityEngine;
using TMPro;
using UnityEngine.EventSystems; // Tıklama olaylarını algılamak için gerekli

public class HidePlaceholder : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    private TMP_InputField inputField;

    void Start()
    {
        inputField = GetComponent<TMP_InputField>();
    }

    // Kutuya tıklandığında (odaklanıldığında) anında çalışır
    public void OnSelect(BaseEventData eventData)
    {
        if (inputField.placeholder != null)
        {
            inputField.placeholder.gameObject.SetActive(false);
        }
    }

    // Kutudan çıkıldığında (başka yere tıklandığında) çalışır
    public void OnDeselect(BaseEventData eventData)
    {
        // Eğer oyuncu hiçbir şey yazmadan çıktıysa, yazıyı geri getir
        if (string.IsNullOrEmpty(inputField.text) && inputField.placeholder != null)
        {
            inputField.placeholder.gameObject.SetActive(true);
        }
    }
}