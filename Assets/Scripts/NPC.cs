using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject interactionUI; // UI "Tekan E untuk bicara"
    public DialogManager dialogManager; // Referensi ke DialogManager
    public string[] firstDialogueLines; // Dialog panjang (pertama kali interaksi)
    public string[] shortDialogueLines; // Dialog pendek (interaksi ulang)
    
    private bool isNearNPC = false; // Apakah pemain dekat NPC
    private bool hasInteracted = false; // Apakah sudah berinteraksi sebelumnya
    private bool isInteracting = false; // Apakah pemain sedang dalam dialog
    private Transform player; // Referensi ke player
    private CharacterMovement characterMovement; // Script pergerakan player

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        characterMovement = player.GetComponent<CharacterMovement>();

        // Atur skala NPC
        transform.localScale = new Vector3(1f, 1f, 1f);

        // Awalnya sembunyikan UI interaksi
        interactionUI.SetActive(false);
    }

    void Update()
    {
        if (isNearNPC && !isInteracting)
        {
            interactionUI.SetActive(true); // Tampilkan UI hanya saat pemain dekat dan belum dalam dialog
        }
        else
        {
            interactionUI.SetActive(false); // Sembunyikan jika tidak dekat atau dalam dialog
        }

        if (isNearNPC && Input.GetKeyDown(KeyCode.E) && !isInteracting) // Jika pemain menekan tombol E untuk berinteraksi
        {
            StartInteraction();
        }

        if (dialogManager.IsDialogueFinished() && isInteracting)
        {
            EndInteraction();
        }
    }

    private void StartInteraction()
    {
        FacePlayer(); // NPC menghadap pemain
        interactionUI.SetActive(false); // Sembunyikan UI
        isInteracting = true; // Tandai bahwa dialog dimulai

        if (!hasInteracted)
        {
            dialogManager.StartDialogue(firstDialogueLines); // Mulai dialog pertama
            hasInteracted = true;
        }
        else
        {
            dialogManager.StartDialogue(shortDialogueLines); // Mulai dialog pendek
        }

        DisablePlayerMovement(true); // Nonaktifkan pergerakan pemain
    }

    private void EndInteraction()
    {
        isInteracting = false; // Tandai dialog selesai
        DisablePlayerMovement(false); // Aktifkan kembali pergerakan pemain
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isNearNPC = true; // Pemain masuk area NPC
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isNearNPC = false; // Pemain keluar dari area NPC
            interactionUI.SetActive(false); // Sembunyikan jika keluar
        }
    }

    private void FacePlayer()
    {
        Vector3 direction = player.position - transform.position;

        if (direction.x > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    private void DisablePlayerMovement(bool disable)
    {
        if (characterMovement != null)
        {
            characterMovement.enabled = !disable;
        }
    }
}
