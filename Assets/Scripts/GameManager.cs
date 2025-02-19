using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int keysCollected = 0; // Jumlah batu yang dikumpulkan
    public int totalKeysRequired = 5; // Total batu yang dibutuhkan

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

    public void AddKey()
    {
        keysCollected++;
        Debug.Log("Keys Collected: " + keysCollected);
    }
}