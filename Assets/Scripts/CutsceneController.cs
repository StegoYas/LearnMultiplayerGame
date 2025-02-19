using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class CutsceneController : MonoBehaviour
{
    [Header("Movement Settings")]
    public Transform character; // Referensi ke karakter
    public float walkSpeed = 2f; // Kecepatan berjalan
    public float walkDurationBeforeDialogue = 2f; // Durasi berjalan pertama sebelum dialog
    public float walkDurationAfterDialogue = 8f; // Durasi berjalan kedua setelah dialog
    private Animator animator; // Animator untuk animasi

    [Header("Dialogue Settings")]
    public GameObject dialoguePanel; // Panel UI dialog
    public TextMeshProUGUI dialogueText; // TextMeshPro untuk dialog
    [TextArea]
    public string dialogue = "Halo, saya baru sampai!"; // Teks dialog

    void Start()
    {
        // Ambil referensi Animator dari karakter
        animator = character.GetComponent<Animator>();

        // Pastikan panel dialog tidak aktif di awal
        dialoguePanel.SetActive(false);

        // Mulai cutscene
        StartCoroutine(PlayCutscene());
    }

    private IEnumerator PlayCutscene()
    {
        // Langkah 1: Karakter berjalan ke atas
        SetWalking(true, 1f); // Aktifkan animasi berjalan ke atas
        yield return StartCoroutine(Walk(Vector3.up, walkDurationBeforeDialogue));
        SetWalking(false, 0f); // Matikan animasi berjalan

        // Langkah 2: Tampilkan dialog selama 3 detik
        dialoguePanel.SetActive(true);
        dialogueText.text = dialogue;
        yield return new WaitForSeconds(3f);
        dialoguePanel.SetActive(false);

        // Langkah 3: Karakter berjalan ke atas lagi
        SetWalking(true, 1f); // Aktifkan animasi berjalan ke atas
        yield return StartCoroutine(Walk(Vector3.up, walkDurationAfterDialogue));
        SetWalking(false, 0f); // Matikan animasi berjalan

        // Langkah 4: Pindah scene
        yield return StartCoroutine(LoadScene("MainScene"));
    }

    private IEnumerator Walk(Vector3 direction, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            // Gerakan karakter ke arah yang ditentukan (ke atas)
            character.position += direction * walkSpeed * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private void SetWalking(bool isWalking, float moveY)
    {
        if (animator != null)
        {
            animator.SetBool("isWalking", isWalking); // Aktifkan/Nonaktifkan animasi
            animator.SetFloat("MoveY", moveY); // Set arah gerakan (1 = atas)
        }
    }

    private IEnumerator LoadScene(string MainScene)
    {
        yield return new WaitForSeconds(0.5f); // Tunggu sebentar sebelum pindah scene
        SceneManager.LoadScene(MainScene); // Load scene baru
    }
}
