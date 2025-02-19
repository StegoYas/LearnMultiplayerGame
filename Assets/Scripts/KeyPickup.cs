using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Jika player menyentuh batu
        {
            GameManager.instance.AddKey(); // Tambah jumlah kunci di GameManager
            Destroy(gameObject); // Hancurkan batu setelah diambil
        }
    }
}