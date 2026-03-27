using UnityEngine;
using DG.Tweening; // DOTween kütüphanesini dahil ediyoruz!

public class CardIdleAnimation : MonoBehaviour
{

    [Header("Animasyon Ayarları")]
    public float animationDuration = 2f; // Varsayılan süre 2 saniye

    void Start()
    {
        

        // Kartı olduğu yerden 15 birim yukarı doğru, 2 saniye içinde hareket ettir.
        // SetRelative(true): Şu anki konumunun üzerine 15 ekle demek.
        // SetLoops(-1, LoopType.Yoyo): Sonsuza kadar (-1) Yoyo gibi git-gel yap.
        // SetEase: Hareketi robotiklikten çıkarıp yumuşat.

        transform.DOLocalMoveY(15f, animationDuration)
            .SetRelative(true)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }
}