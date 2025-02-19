using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("Game is quitting..."); // Hanya terlihat di editor
        Application.Quit(); // Berfungsi saat build
    }
}

