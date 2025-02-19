using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Untuk UI Button
using TMPro;  // Untuk menggunakan TextMeshPro

public class DialogManager : MonoBehaviour
{
    public GameObject dialogPanel; // Panel untuk dialog (akan muncul dan hilang)
    public TMP_Text dialogText; // Menggunakan TMP_Text untuk TextMeshPro
    public Button nextButton; // Tombol untuk melanjutkan dialog
    public string[] dialogLines; // Array untuk dialog yang akan ditampilkan

    private int currentLine = 0; // Untuk melacak dialog yang ditampilkan

    void Start()
    {
        dialogPanel.SetActive(false); // Sembunyikan panel dialog saat permainan dimulai
        nextButton.onClick.AddListener(ShowNextDialog); // Set tombol untuk melanjutkan dialog
    }

    // Fungsi untuk memulai dialog
    public void StartDialogue(string[] lines)
    {
        dialogLines = lines;
        currentLine = 0; // Reset ke baris pertama
        ShowDialog(); // Tampilkan dialog pertama
    }

    // Fungsi untuk memulai dialog
    public void ShowDialog()
    {
        dialogPanel.SetActive(true); // Tampilkan panel dialog
        dialogText.text = dialogLines[currentLine]; // Tampilkan dialog pertama
        nextButton.gameObject.SetActive(true); // Aktifkan tombol next
    }

    // Fungsi untuk menampilkan dialog berikutnya
    private void ShowNextDialog()
    {
        currentLine++; // Pindah ke baris dialog berikutnya

        // Jika semua dialog selesai, tutup panel dialog
        if (currentLine >= dialogLines.Length)
        {
            dialogPanel.SetActive(false); // Tutup panel
            nextButton.gameObject.SetActive(false); // Sembunyikan tombol next
        }
        else
        {
            dialogText.text = dialogLines[currentLine]; // Update teks dialog
        }
    }

    // Fungsi untuk mengecek apakah dialog telah selesai
    public bool IsDialogueFinished()
    {
        return currentLine >= dialogLines.Length;
    }
}
