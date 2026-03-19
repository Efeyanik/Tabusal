using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class UIDustFlow : MonoBehaviour
{
    public float verticalSpeed = 0.02f;   // yukarý akýþ
    public float xWaveAmplitude = 0.01f;   // sað-sol
    public float xWaveSpeed = 0.6f;

    public float alphaPulseSpeed = 0.3f;
    public float alphaPulseAmount = 0.08f;

    public float phaseOffset = 0f;

    RawImage img;
    Rect uv;
    Color baseColor;
    float originalX; // Baþlangýç X konumunu saklamak için

    void Awake()
    {
        img = GetComponent<RawImage>();
        uv = img.uvRect;
        baseColor = img.color;
        originalX = uv.x;
    }

    void Update()
    {
        float t = Time.time + phaseOffset;

        // UV Y akýþý
        uv.y = Mathf.Repeat(uv.y - verticalSpeed * Time.deltaTime, 1f);

        // X dalga
        float wave = Mathf.Sin(t * xWaveSpeed) * xWaveAmplitude;
        uv.x = originalX + wave;

        img.uvRect = uv;

        // Alpha nefesi
        float pulse = Mathf.Sin(t * alphaPulseSpeed) * alphaPulseAmount;
        img.color = new Color(
            baseColor.r,
            baseColor.g,
            baseColor.b,
            Mathf.Clamp01(baseColor.a + pulse)
        );
    }
}
