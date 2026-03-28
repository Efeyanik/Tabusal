using UnityEngine;
using DG.Tweening;
using UnityEngine.UI; // DOTween kütüphanesini dahil ediyoruz!

public class CardIdleAnimation : MonoBehaviour
{

    [Header("Animasyon Ayarları")]
    public float animationDuration = 2f; 
    public float moveDistance = 15f; 

    [Header("Bomba Rengi Ayarları")]
    public bool isBombMode = false; // Sadece bombada seçilecek.
    public Color targetColor;       
    public float colorDuration = 2f;

    public RawImage cardImage;

    void Start()
    {
        

        // Kartı olduğu yerden 15 birim yukarı doğru, 2 saniye içinde hareket ettir.
        // SetRelative(true): Şu anki konumunun üzerine 15 ekle demek.
        // SetLoops(-1, LoopType.Yoyo): Sonsuza kadar (-1) Yoyo gibi git-gel yap.
        // SetEase: Hareketi robotiklikten çıkarıp yumuşat.

        transform.DOLocalMoveY(moveDistance, animationDuration)
            .SetRelative(true)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine)
            .SetLink(gameObject);




        if (isBombMode)
        {
            
            if (cardImage != null)
            {
                
                cardImage.DOColor(targetColor, colorDuration)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetEase(Ease.InOutSine)
                    .SetLink(gameObject);
            }
        }


    }
}