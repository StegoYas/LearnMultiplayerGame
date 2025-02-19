using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAmbacongEnd : MonoBehaviour
{
    public GameObject ambacong; // Referensi ke GameObject Ambacong

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Mengecek apakah yang masuk ke trigger adalah Player
        if (collision.CompareTag("Player"))
        {
            // Memanggil fungsi StopChasing di skrip AmbacongAI
            AmbacongAI ambacongAI = ambacong.GetComponent<AmbacongAI>();
            if (ambacongAI != null)
            {
                ambacongAI.StopChasing();
            }
        }
    }
}