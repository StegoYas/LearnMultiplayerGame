using UnityEngine;
using UnityEngine.Rendering.Universal; // Tambahkan namespace untuk Light2D

public class Checkpoint : MonoBehaviour
{
    private bool isActivated = false; // Status apakah checkpoint telah diaktifkan
    private Animator animator;
    private Light2D light2D; // Referensi ke komponen Light2D

    void Start()
    {
        animator = GetComponent<Animator>();
        light2D = GetComponent<Light2D>(); // Ambil komponen Light2D

        if (light2D != null)
        {
            light2D.intensity = 0; // Pastikan cahaya dimulai dalam keadaan mati
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Hanya aktif jika objek yang memasuki collider adalah player
        if (other.CompareTag("Player") && !isActivated)
        {
            isActivated = true; // Tandai checkpoint sebagai aktif
            animator.SetTrigger("Activate"); // Jalankan animasi checkpoint

            // Nyalakan cahaya dengan intensitas tertentu
            if (light2D != null)
            {
                light2D.intensity = 1.5f; // Sesuaikan intensitas sesuai kebutuhan
            }

            // Dapatkan skrip PlayerHealth dari objek player
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // Atur checkpoint baru untuk player
                playerHealth.SetCheckpoint(transform);
                Debug.Log("Checkpoint activated at: " + transform.position);
            }
            else
            {
                Debug.LogWarning("PlayerHealth script not found on Player!");
            }
        }
    }

    // Opsional: Tambahkan fungsi reset untuk reset level atau debugging
    public void ResetCheckpoint()
    {
        isActivated = false;

        // Matikan cahaya saat checkpoint di-reset
        if (light2D != null)
        {
            light2D.intensity = 0;
        }

        Debug.Log("Checkpoint reset at: " + transform.position);
    }
}
