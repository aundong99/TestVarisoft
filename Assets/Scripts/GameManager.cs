using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private int totalEnemies;

    [SerializeField] private GameObject winPanel;


    public void ShowYouWin()
    {
        winPanel.SetActive(true);

        foreach (Transform child in winPanel.transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void RegisterEnemy()
    {
        totalEnemies++;
    }

    public void EnemyDied()
    {
        totalEnemies--;
        if (totalEnemies <= 0)
        {
            WinGame();
        }
    }

    private void WinGame()
    {
        ShowYouWin();
    }

}
