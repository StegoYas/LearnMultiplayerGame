using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public FadeManager fadeManager; // Referensi ke FadeManager

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Jika player menyentuh pintu
        {
            if (GameManager.instance.keysCollected >= GameManager.instance.totalKeysRequired)
            {
                // Jika sudah semua batu terkumpul, mulai fade sequence
                fadeManager.StartFadeSequence();
                Debug.Log("Player exited!");
            }
            else
            {
                // Jika belum cukup batu, tampilkan pesan dan jumlah yang kurang
                int keysLeft = GameManager.instance.totalKeysRequired - GameManager.instance.keysCollected;
                Debug.Log("The door is locked. Collect all keys!");
                // Jika MessageManager masih digunakan:
                MessageManager.instance.ShowMessage("Anda belum mengumpulkan semua batu! Sisa: " + keysLeft, 2f);
            }
        }
    }
}