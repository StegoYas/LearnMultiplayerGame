using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Tambahkan namespace untuk TextMeshPro

public class MessageManager : MonoBehaviour
{
    public static MessageManager instance;
    public TMP_Text messageText; // Ganti dari Text ke TMP_Text

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowMessage(string message, float duration)
    {
        StopAllCoroutines();
        StartCoroutine(DisplayMessage(message, duration));
    }

    private IEnumerator DisplayMessage(string message, float duration)
    {
        messageText.text = message;
        messageText.gameObject.SetActive(true);
        yield return new WaitForSeconds(duration);
        messageText.gameObject.SetActive(false);
    }
}