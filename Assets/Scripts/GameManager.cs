using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public int coin = 0;

    public TextMeshProUGUI textMeshProCoin;

    public static GameManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void ShowCoinCount()
    {
        coin++;
        textMeshProCoin.SetText(coin.ToString());

        if (coin % 2 == 0)
        {
            Player player = FindFirstObjectByType<Player>();
            if (player != null)
            {
                player.MissileUp();
            }
        }
    }
}
