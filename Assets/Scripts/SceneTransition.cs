using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public CanvasGroup canvasGroup; // Untuk mengatur transparansi
    public float fadeDuration = 1f; // Waktu untuk setiap efek

    private void Awake()
    {
        if (canvasGroup == null)
        {
            canvasGroup = GetComponentInChildren<CanvasGroup>();
        }

        canvasGroup.alpha = 0; // Mulai dari transparansi penuh
    }

    // Fade In (membuat transparansi bertambah dari hitam)
    public IEnumerator FadeIn()
    {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(timer / fadeDuration);
            yield return null;
        }
    }

    // Fade Out (membuat hitam hingga layar penuh)
    public IEnumerator FadeOut()
    {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = 1 - Mathf.Clamp01(timer / fadeDuration);
            yield return null;
        }
    }

    // Fungsi utama untuk load scene dengan transisi
    public IEnumerator LoadSceneWithFade(string sceneName)
    {
        yield return StartCoroutine(FadeOut()); // Lakukan fade out
        SceneManager.LoadScene(sceneName); // Load scene tujuan
        yield return StartCoroutine(FadeIn()); // Lakukan fade in
    }
}
