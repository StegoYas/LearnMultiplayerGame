using System.Collections;
using UnityEngine;

public class AmbacongAI : MonoBehaviour
{
    public Transform player; // Referensi ke posisi pemain
    public float speed = 2.0f; // Kecepatan mengejar
    public float fadeTime = 1.0f; // Waktu untuk fade-in/out audio dan visual
    public float detectionRadius = 3.0f; // Jarak deteksi pemain
    
    private bool isChasing = false; // Flag untuk mengecek apakah sedang mengejar
    private SpriteRenderer spriteRenderer; // Referensi ke SpriteRenderer
    private AudioSource audioSource; // Referensi ke AudioSource
    public GameObject jumpscarePanel; // Panel jumpscare untuk menampilkan efek
    private Collider2D ambacongCollider; // Collider Ambacong

    private PlayerHealth playerHealth; // Referensi ke script PlayerHealth
    private Vector3 initialPosition; // Posisi awal Ambacong

    private void Start()
    {
        // Inisialisasi komponen
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        ambacongCollider = GetComponent<Collider2D>();

        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(1, 0, 0, 0); // Set warna awal sprite ke invisible (alpha = 0)
        }

        if (audioSource != null)
        {
            audioSource.volume = 0f;
            audioSource.Stop(); // Pastikan audio tidak bermain di awal
        }

        if (ambacongCollider != null)
        {
            ambacongCollider.enabled = false; // Nonaktifkan collider pada awalnya
        }

        initialPosition = transform.position; // Simpan posisi awal Ambacong
        playerHealth = player.GetComponent<PlayerHealth>(); // Temukan komponen PlayerHealth di scene
    }

    private void Update()
    {
        if (isChasing && player != null)
        {
            // Mengejar player
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            // Ubah orientasi Ambacong (hadap kiri atau kanan)
            if (direction.x > 0)
                transform.localScale = new Vector3(1, 1, 1); // Hadap kanan
            else if (direction.x < 0)
                transform.localScale = new Vector3(-1, 1, 1); // Hadap kiri
        }
    }

    public void StartChasing()
    {
        Debug.Log("StartChasing called.");
        StartCoroutine(FadeInAndChase());
    }

    private IEnumerator FadeInAndChase()
    {
        // Aktifkan collider saat mulai chasing
        if (ambacongCollider != null)
        {
            ambacongCollider.enabled = true;
        }

        // Fade-in Ambacong secara visual
        if (spriteRenderer != null)
        {
            float t = 0f;
            Color color = spriteRenderer.color;

            while (t < fadeTime)
            {
                t += Time.deltaTime;
                color.a = t / fadeTime; // Tingkatkan alpha secara bertahap
                spriteRenderer.color = color;
                yield return null;
            }
            spriteRenderer.color = new Color(1, 0, 0, 1); // Pastikan alpha penuh
        }

        // Mulai fade-in audio jika audioSource ada
        if (audioSource != null)
        {
            audioSource.Play();
            yield return StartCoroutine(FadeAudio(audioSource, true)); // Fade-in audio
        }

        isChasing = true; // Mulai mengejar player
    }

    public void StopChasing()
    {
        Debug.Log("StopChasing called.");
        StartCoroutine(FadeOutAndReturn());
    }

    private IEnumerator FadeOutAndReturn()
    {
        Debug.Log("Stop chasing and fading out...");

        isChasing = false;

        // Fade-out Ambacong secara visual
        if (spriteRenderer != null)
        {
            float t = fadeTime;
            Color color = spriteRenderer.color;

            while (t > 0)
            {
                t -= Time.deltaTime;
                color.a = t / fadeTime; // Kurangi alpha secara bertahap
                spriteRenderer.color = color;
                yield return null;
            }
            spriteRenderer.color = new Color(1, 0, 0, 0); // Pastikan alpha 0
        }

        // Mulai fade-out audio jika audioSource ada
        if (audioSource != null)
        {
            yield return StartCoroutine(FadeAudio(audioSource, false)); // Fade-out audio
        }

        // Nonaktifkan collider saat selesai chasing
        if (ambacongCollider != null)
        {
            ambacongCollider.enabled = false;
        }

        // Kembali ke posisi awal
        while (Vector3.Distance(transform.position, initialPosition) > 0.1f)
        {
            Vector3 direction = (initialPosition - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
            yield return null;
        }

        transform.position = initialPosition; // Pastikan posisi benar-benar di posisi awal
    }

    private IEnumerator FadeAudio(AudioSource audioSource, bool fadeIn)
    {
        float startVolume = fadeIn ? 0f : audioSource.volume;
        float targetVolume = fadeIn ? 1f : 0f;
        float time = 0;

        while (time < fadeTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, time / fadeTime);
            time += Time.deltaTime;
            yield return null;
        }

        audioSource.volume = targetVolume; // Pastikan volume sesuai
    }

    // Trigger untuk jumpscare saat Ambacong menyentuh player
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Cek apakah Ambacong sedang chasing atau visible
            if (isChasing || (spriteRenderer != null && spriteRenderer.color.a > 0))
            {
                TriggerJumpscare();
                PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(); // Pemain mendapatkan damage
                }
                StopChasing(); // Hentikan pengejaran setelah jumpscare
            }
            else
            {
                Debug.Log("Ambacong tidak aktif. Tidak memberikan damage.");
            }
        }
    }

    private void TriggerJumpscare()
    {
        if (jumpscarePanel != null)
        {
            jumpscarePanel.SetActive(true); // Tampilkan jumpscare panel
        }
        Debug.Log("Jumpscare triggered.");
    }
}