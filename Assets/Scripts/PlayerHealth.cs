using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Image[] hearts; // Array untuk menyimpan gambar hati
    public GameObject jumpscarePanel; // Panel untuk jumpscare
    public GameObject gameOverPanel; // Panel untuk game over
    public Transform respawnPoint; // Titik awal respawn jika belum checkpoint
    public AudioClip jumpscareSound; // Musik jumpscare
    public AudioSource audioSource; // Audio source untuk memainkan suara
    public float jumpscareDuration = 5f; // Durasi jumpscare

    private int currentHealth; // Jumlah nyawa saat ini
    private Transform checkpoint; // Titik checkpoint

    void Start()
    {
        currentHealth = hearts.Length; // Set health sesuai jumlah hati
        jumpscarePanel.SetActive(false); // Matikan jumpscare saat mulai
        gameOverPanel.SetActive(false); // Matikan game over panel saat mulai
        checkpoint = null; // Pastikan checkpoint awal null

        // Validasi AudioSource
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                Debug.LogWarning("AudioSource tidak terpasang pada Player!");
            }
        }

        // Pastikan player respawn di respawnPoint saat mulai
        transform.position = respawnPoint.position;
    }

    public void TakeDamage()
    {
        currentHealth--; // Kurangi health
        UpdateHeartsUI(); // Perbarui tampilan UI
        Debug.Log("Player took damage. Current Health: " + currentHealth);

        if (currentHealth > 0)
        {
            StartCoroutine(TriggerJumpscare());
        }
        else
        {
            GameOver();
        }
    }

    private void UpdateHeartsUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = i < currentHealth; // Tampilkan/hilangkan hati
        }
    }
    private IEnumerator TriggerJumpscare()
{
    jumpscarePanel.SetActive(true); // Tampilkan jumpscare

    if (jumpscareSound != null)
    {
        audioSource.PlayOneShot(jumpscareSound); // Putar suara jumpscare
    }

    yield return new WaitForSeconds(jumpscareDuration); // Tunggu durasi jumpscare

    jumpscarePanel.SetActive(false); // Sembunyikan jumpscare

    // Reset posisi Ambacong setelah jumpscare selesai
    TriggerAmbacong triggerAmbacong = FindObjectOfType<TriggerAmbacong>();
    if (triggerAmbacong != null)
    {
        triggerAmbacong.ResetAmbacong();
    }

    // Respawn player
    if (checkpoint != null)
    {
        transform.position = checkpoint.position; // Respawn ke checkpoint
    }
    else
    {
        transform.position = respawnPoint.position; // Respawn ke titik awal
    }
}

    private void GameOver()
    {
        Debug.Log("Game Over");
        gameOverPanel.SetActive(true); // Tampilkan panel game over
        Time.timeScale = 0; // Pause game
    }

    public void SetCheckpoint(Transform newCheckpoint)
    {
        if (newCheckpoint != null)
        {
            checkpoint = newCheckpoint; // Set checkpoint baru
            Debug.Log("Checkpoint set at: " + checkpoint.position);
        }
        else
        {
            Debug.LogWarning("Invalid checkpoint passed to SetCheckpoint.");
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1; // Lanjutkan waktu
        SceneManager.LoadScene("MainMenu"); // Kembali ke main menu
    }
}