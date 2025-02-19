using UnityEngine;
using TMPro;
using System.Collections;

public class PlayerNgomong : MonoBehaviour
{
    public GameObject dialogPanel; // Panel untuk dialog
    public TextMeshProUGUI dialogText; // Komponen teks dialog
    [TextArea]
    public string dialogLine; // Teks dialog
    public float dialogDuration = 3f; // Durasi dialog (dapat diatur di Inspector)

    public GameObject tutorialPanel; // Panel untuk tutorial
    public TextMeshProUGUI tutorialText; // Komponen teks tutorial
    public string tutorialLine = "Gunakan WASD untuk bergerak dan tekan F untuk menggunakan senter.";
    public float tutorialDuration = 5f; // Durasi tutorial (dapat diatur di Inspector)

    private CharacterMovement characterMovement; // Referensi ke script pergerakan karakter

    void Start()
    {
        // Cari komponen CharacterMovement di GameObject karakter
        characterMovement = GetComponent<CharacterMovement>();

        // Sembunyikan panel tutorial di awal game
        tutorialPanel.SetActive(false);

        // Mulai dialog jika teks dialog tidak kosong
        if (!string.IsNullOrEmpty(dialogLine))
        {
            StartCoroutine(StartDialog());
        }
    }

    private IEnumerator StartDialog()
    {
        dialogPanel.SetActive(true); // Tampilkan panel dialog
        dialogText.text = dialogLine; // Tampilkan teks dialog
        LockCharacterMovement(true); // Kunci pergerakan karakter

        yield return new WaitForSeconds(dialogDuration); // Tunggu selama durasi dialog

        EndDialog(); // Akhiri dialog

        StartCoroutine(ShowTutorial()); // Tampilkan tutorial setelah dialog
    }

    private void EndDialog()
    {
        dialogPanel.SetActive(false); // Sembunyikan panel dialog
    }

    private IEnumerator ShowTutorial()
    {
        tutorialPanel.SetActive(true); // Tampilkan panel tutorial
        tutorialText.text = tutorialLine; // Tampilkan teks tutorial secara langsung

        yield return new WaitForSeconds(tutorialDuration); // Tunggu selama durasi tutorial

        EndTutorial(); // Akhiri tutorial
    }

    private void EndTutorial()
    {
        tutorialPanel.SetActive(false); // Sembunyikan panel tutorial
        LockCharacterMovement(false); // Buka kunci pergerakan karakter
    }

    private void LockCharacterMovement(bool isLocked)
    {
        if (characterMovement != null)
        {
            characterMovement.enabled = !isLocked; // Aktifkan/Nonaktifkan script pergerakan
        }
    }
}
