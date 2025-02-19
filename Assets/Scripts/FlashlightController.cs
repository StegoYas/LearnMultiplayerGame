using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    public GameObject flashlight;  // Referensi ke Spot Light 2D
    private bool isFlashlightOn = false;  // Status apakah senter menyala atau tidak

    void Start()
    {
        // Pastikan senter mati saat awal permainan
        if (flashlight != null)
        {
            flashlight.SetActive(false);
        }
    }

    void Update()
    {
        // Menghidupkan atau mematikan senter dengan tombol F
        if (Input.GetKeyDown(KeyCode.F))
        {
            isFlashlightOn = !isFlashlightOn;
            flashlight.SetActive(isFlashlightOn);
        }

        // Mengatur rotasi senter berdasarkan input tombol (8 arah)
        if (isFlashlightOn && flashlight != null)
        {
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
            {
                // Diagonal kanan atas
                flashlight.transform.localRotation = Quaternion.Euler(0, 0, -45);
            }
            else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
            {
                // Diagonal kanan bawah
                flashlight.transform.localRotation = Quaternion.Euler(0, 0, -135);
            }
            else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
            {
                // Diagonal kiri atas
                flashlight.transform.localRotation = Quaternion.Euler(0, 0, 45);
            }
            else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
            {
                // Diagonal kiri bawah
                flashlight.transform.localRotation = Quaternion.Euler(0, 0, 135);
            }
            else if (Input.GetKey(KeyCode.W))
            {
                // Ke atas
                flashlight.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                // Ke bawah
                flashlight.transform.localRotation = Quaternion.Euler(0, 0, 180);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                // Ke kiri
                flashlight.transform.localRotation = Quaternion.Euler(0, 0, 90);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                // Ke kanan
                flashlight.transform.localRotation = Quaternion.Euler(0, 0, -90);
            }
        }
    }
}
