using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    enum GameState
    {
        Idle,
        Play,
        Lose,
    }

    [SerializeField] public Player player;
    [SerializeField] public CameraScript cameraScript;
    [SerializeField] public PlatformsManager platformsManager;
    [SerializeField] public EnemiesManager enemiesManager;

    [SerializeField] public GameObject startCanvas;
    [SerializeField] public GameObject gameCanvas;
    [SerializeField] public GameObject endCanvas;

    private int _currentSessionScore;
    private GameState _gameState;

    private void Start()
    {
        ChangeGameState(GameState.Idle);
    }

    private void Update()
    {
        DisplayScore();
        FallChecker();
        KilledChecker();
    }

    private void FallChecker()
    {
        if (player.MaxHeight - Camera.main.orthographicSize > player.transform.position.y)
        {
            LoseProcess();
        }
    }

    private void KilledChecker()
    {
        if (enemiesManager.KilledByEnemy)
        {
            LoseProcess();
        }
    }
    
    private void LoseProcess()
    {
        ChangeGameState(GameState.Lose);
        TrySetScore((int) player.MaxHeight);
        
        _currentSessionScore = (int) player.MaxHeight;

        player.Restart();
        platformsManager.Restart();
        cameraScript.Restart();
        enemiesManager.Restart();
    }

    private void DisplayScore()
    {
        Text text;


        switch (_gameState)
        {
            case GameState.Idle:
                text = startCanvas.GetComponentInChildren<Text>();
                text.text = GetBestScore().ToString();
                break;
            case GameState.Play:
                text = gameCanvas.GetComponentInChildren<Text>();
                text.text = Math.Floor(player.MaxHeight).ToString();
                break;
            case GameState.Lose:
                text = endCanvas.GetComponentInChildren<Text>();
                text.text = _currentSessionScore.ToString();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void OnStartClick()
    {
        ChangeGameState(GameState.Play);

        player.Activate();
        platformsManager.Activate();
    }

    public void OnRestartClick()
    {
        ChangeGameState(GameState.Play);

        player.Activate();
        platformsManager.Activate();

        _currentSessionScore = 0;
    }

    private void ChangeGameState(GameState state)
    {
        _gameState = state;

        switch (state)
        {
            case GameState.Idle:
                startCanvas.SetActive(true);
                gameCanvas.SetActive(false);
                endCanvas.SetActive(false);
                break;
            case GameState.Play:
                startCanvas.SetActive(false);
                gameCanvas.SetActive(true);
                endCanvas.SetActive(false);
                break;
            case GameState.Lose:
                startCanvas.SetActive(false);
                gameCanvas.SetActive(false);
                endCanvas.SetActive(true);
                break;
        }
    }

    private int GetBestScore()
    {
        var score = PlayerPrefs.HasKey("score") ? PlayerPrefs.GetInt("score") : 0;
        return score;
    }

    private void TrySetScore(int score)
    {
        if (GetBestScore() < score)
        {
            PlayerPrefs.SetInt("score", score);
        }
    }
}