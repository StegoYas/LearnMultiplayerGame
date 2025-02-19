using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FadeManager : MonoBehaviour
{
    public CanvasGroup panelBackground; // Panel latar belakang hitam (tetap terlihat)
    public TMP_Text escapeText; // Teks "Your Escape"
    public TMP_Text thanksText; // Teks "Thanks for Playing"
    public AudioSource transitionMusic; // Musik transisi
    public float fadeDuration = 1f; // Durasi fade-in/out

    public void StartFadeSequence()
    {
        StartCoroutine(FadeSequence());
    }

    private IEnumerator FadeSequence()
    {
        // Pastikan panelBackground tetap terlihat
        panelBackground.alpha = 1;

        // Mainkan musik transisi, jika ada
        if (transitionMusic != null)
        {
            transitionMusic.Play();
        }

        // Fade-in teks "Your Escape"
        yield return StartCoroutine(FadeTextIn(escapeText));
        yield return new WaitForSeconds(1.5f); // Tahan teks selama 1.5 detik
        yield return StartCoroutine(FadeTextOut(escapeText));

        // Fade-in teks "Thanks for Playing"
        yield return StartCoroutine(FadeTextIn(thanksText));
        yield return new WaitForSeconds(1.5f); // Tahan teks selama 1.5 detik
        yield return StartCoroutine(FadeTextOut(thanksText));

        // Pindah ke Scene MainMenu
        SceneManager.LoadScene("MainMenu");
    }

    private IEnumerator FadeTextIn(TMP_Text text)
    {
        float timer = 0f;
        Color originalColor = text.color;
        text.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0); // Awal alpha = 0

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            text.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(0, 1, timer / fadeDuration));
            yield return null;
        }

        text.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1); // Akhir alpha = 1
    }

    private IEnumerator FadeTextOut(TMP_Text text)
    {
        float timer = 0f;
        Color originalColor = text.color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            text.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(1, 0, timer / fadeDuration));
            yield return null;
        }

        text.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0); // Akhir alpha = 0
    }
}