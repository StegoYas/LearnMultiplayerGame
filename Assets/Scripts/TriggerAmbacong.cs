using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAmbacong : MonoBehaviour
{
    public GameObject ambacong; // Referensi ke GameObject Ambacong
    private Vector3 initialPosition; // Posisi awal Ambacong
    private bool isChasing = false; // Flag untuk mengecek apakah Ambacong sedang mengejar

    void Start()
    {
        initialPosition = ambacong.transform.position; // Simpan posisi awal Ambacong
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isChasing)
        {
            // Jika player masuk trigger dan Ambacong tidak sedang mengejar
            isChasing = true; // Set flag mengejar
            ambacong.GetComponent<AmbacongAI>().StartChasing(); // Mulai pengejaran
        }
    }

    public void ResetAmbacong()
    {
        // Reset posisi dan status Ambacong setelah jumpscare selesai
        ambacong.transform.position = initialPosition; // Kembalikan posisi awal
        ambacong.GetComponent<AmbacongAI>().StopChasing(); // Hentikan pengejaran
        isChasing = false; // Reset flag mengejar
    }
}