using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class EndingSequence : MonoBehaviour
{
    [Header("UI References")]
    public GameObject endingCanvas;
    public Image whiteOverlay;
    public TextMeshProUGUI endingText1;
    public TextMeshProUGUI endingText2;

    [Header("Settings")]
    public float fadeDuration = 2f;
    public float textDisplayDuration = 3f;

    private bool hasTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (hasTriggered) return;
        if (!other.CompareTag("Player")) return;

        hasTriggered = true;
        endingCanvas.SetActive(true);
        StartCoroutine(PlayEnding());
    }

    IEnumerator PlayEnding()
    {
        // Fade ke putih
        yield return StartCoroutine(FadeOverlay(0f, 1f));

        // Tampilkan teks pertama
        endingText1.gameObject.SetActive(true);
        yield return new WaitForSeconds(textDisplayDuration);

        // Ganti ke teks kedua
        endingText1.gameObject.SetActive(false);
        endingText2.gameObject.SetActive(true);
        yield return new WaitForSeconds(textDisplayDuration);
    }

    IEnumerator FadeOverlay(float from, float to)
    {
        float elapsed = 0f;
        Color color = whiteOverlay.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(from, to, elapsed / fadeDuration);
            whiteOverlay.color = color;
            yield return null;
        }

        color.a = to;
        whiteOverlay.color = color;
    }
}