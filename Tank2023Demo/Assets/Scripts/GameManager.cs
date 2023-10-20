using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public event Action OnStart;
    public event Action OnPlayerDamaged;
    public event Action OnLevelComplete;
    public event Action OnPlayerLose;

    public TextMeshProUGUI Score;
    public TextMeshProUGUI TotalScoreUI;
    public TextMeshProUGUI EnemyCountUI;

    private int TotalScore;

    public RawImage[] HealthUI;

    public GameObject MainMenu;
    public GameObject LoseScreen;
    public GameObject LevelComplete;
    public GameObject Options;

    public int Hard=3;
    public int Medium=6;
    public int Easy=9;

    public int KillCount = 0;
    private void Awake()
    {
        if (Instance != null && Instance == this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        
        OnStart += HUDReset;
        OnStart += ObjectPool.Instance.EnemyPool.Clear;
        PlayerData.Instance?.SetHealth(Hard);
    }
    private void Update()
    {
        if (MainMenu.activeSelf)
        {
            PlayerData.Instance.isPlayerCanMove = false;
        }
        
    }
    public void StartButton()
    {
        PlayerData.Instance.gameObject.SetActive(true);
        TotalScore = 0;
        OnStart.Invoke();
        PlayerCanMove();
        Debug.Log("clikcked");

    }
    public void OnEnemyKilled(int Point)
    {
        TotalScore += Point;
        Score.text = "SCORE: " + TotalScore.ToString();
    }
    public void OnHealthChange()
    {
        foreach (var item in HealthUI)
        {
            if (item.gameObject.activeSelf)
            {
                item.gameObject.SetActive(false);
                break;
            }
        }
    }
    private void HUDReset()
    {
        int i = 0;
        while(HealthUI.Length > i)
        {
            HealthUI[i].gameObject.SetActive(true);
            i++;
        }
    }

    private void PlayerCanMove()
    {
        PlayerData.Instance.isPlayerCanMove = true;
    }
    public void GameOver()
    {
        KillReset();
        OnPlayerLose();
        PlayerData.Instance.gameObject.SetActive(false );
        LoseScreen.SetActive(true);
    }

    public void ScoreBoard()
    {
        TotalScoreUI.text = "SCORE: " + TotalScore.ToString();
        PlayerData.Instance.gameObject.SetActive(false);
        EnemyCountUI.text = "ENEMYCOUNT: " + LevelManager.Instance.EnemyCount;
        LevelComplete.gameObject.SetActive(true);
        KillReset();
    }
    public void NextButton()
    {
        PlayerData.Instance.gameObject.SetActive(true);
        LevelManager.Instance.LevelIncrease();
        ScoreBoard();
        OnStart.Invoke();
    }
    private void KillReset()
    {
        KillCount = 0;
    }

}
