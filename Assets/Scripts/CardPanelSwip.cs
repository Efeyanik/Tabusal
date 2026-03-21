using UnityEngine;
using TMPro;
using DG.Tweening; // DOTween kütüphanesini unutmuyoruz

public class CardSwipeManager : MonoBehaviour
{   

    public GameManager gameManager; 


    [Header("Kart Referansları")]
    public RectTransform cardFrontRect; // Hareket edecek olan asıl kartın (CardPanel)
    public CanvasGroup cardFrontCanvasGroup; // Şeffaflık için

    [Header("Kelime Referansları")]
    
    public TextMeshProUGUI frontWordText;
    public TextMeshProUGUI forbidden0Text;
    public TextMeshProUGUI forbidden1Text;
    public TextMeshProUGUI forbidden2Text;
    public TextMeshProUGUI forbidden3Text;
    public TextMeshProUGUI forbidden4Text;

    public TextMeshProUGUI backWordText;  
    public TextMeshProUGUI backForbidden0Text;
    public TextMeshProUGUI backForbidden1Text;
    public TextMeshProUGUI backForbidden2Text;
    public TextMeshProUGUI backForbidden3Text;
    public TextMeshProUGUI backForbidden4Text;


    public void SwipeCardLeft()
    {
        // 1. Ön kartı (CardPanel) sola fırlat (-1200 piksel) ve aynı anda şeffaflaştır (0.3 saniye)
        
        cardFrontRect.DOAnchorPosX(-1200f, 0.3f).SetEase(Ease.InBack,1f);
        cardFrontCanvasGroup.DOFade(0f, 0.3f).OnComplete(() =>
        {
            




            // 2. [SİHİRBAZLIK NUMARASI]: Ön kartın yazısını, arka kartın (zaten oyuncunun gördüğü) yazısıyla aynı yap!
            frontWordText.text = backWordText.text;
            forbidden0Text.text = backForbidden0Text.text;
            forbidden1Text.text = backForbidden1Text.text;
            forbidden2Text.text = backForbidden2Text.text;
            forbidden3Text.text = backForbidden3Text.text;
            forbidden4Text.text = backForbidden4Text.text;  

            // 3. Ön kartı çaktırmadan tekrar merkeze (X=0) ışınla ve %100 görünür yap
            // Oyuncu, arka kartı görmeye devam eder ama aslında biz ön kartı onun üstüne geri koyduk. Değişimi asla fark etmeyecek.
            cardFrontRect.anchoredPosition = new Vector2(0f, cardFrontRect.anchoredPosition.y);
            cardFrontCanvasGroup.alpha = 1f;

            // --- BUNDAN SONRA ---
            
        });
    }
}