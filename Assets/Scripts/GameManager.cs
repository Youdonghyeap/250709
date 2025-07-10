using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int coin = 0;

    public TextMeshProUGUI textMeshProCoin;

    public static GameManager Instance { get; private set; }

    public GameObject GameOverUI;
    public TextMeshProUGUI resultText;

    public bool isGameover = false;

    [SerializeField]
    private int goal = 100;
    [SerializeField]
    private GameObject coinPrefab;
    [SerializeField]
    private int NumOfCoin = 10;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    void Start()
    {
        if (GameOverUI != null)
        {
            GameOverUI.SetActive(false);
        }   
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void ShowCoinCount()
    {
        coin++;
        textMeshProCoin.text = "Coin : " + coin;

        if (coin % 10 == 0)
        {
            Player player = FindFirstObjectByType<Player>();
            if (player != null)
            {
                player.MissileUp();
            }
        }

        if (Coin.Instance != null && !Coin.Instance.isFromCoinBomb)
        {
            TryActivateRandomAbility();
        }
    }

    private void TryActivateRandomAbility()
    {
        float chance = UnityEngine.Random.value; // 0.0 ~ 1.0 사이의 랜덤값

        if (chance <= 0.2f)  
        {
            int abilityIndex = 0; 
            ActivateAbility(abilityIndex);
        }
    }

    private void ActivateAbility(int index)
    {
        Player player = FindFirstObjectByType<Player>();
        if (player == null) return;

        switch (index)
        {
            case 0:
                ActivateCoinBomb(); 
                break;
        }
    }

    private void ActivateCoinBomb()
    {
        for (int i = 0; i < NumOfCoin; i++)
        {
            GameObject c = Instantiate(coinPrefab, transform.position, Quaternion.identity);

            // 폭탄에서 나온 코인이라고 표시
            Coin coinScript = c.GetComponent<Coin>();
            if (coinScript != null)
                coinScript.isFromCoinBomb = true;
        }
        Debug.Log($"코인 폭탄 발동!");
    }

    public void DecreaseGoal()
    {
        goal = goal - 1;
        if (goal <= 0)
        {
            SetGameOver(true);
        }
    }

    public void SetGameOver(bool success)
    {
        if (GameOverUI != null && isGameover == false)
        {
            isGameover = true;
            GameOverUI.SetActive(true);
        }

        if (resultText != null)
        {
            resultText.text = success ? "MISSION CLEAR!" : "GAME OVER!";
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(0);
        isGameover = false;
    }
}
