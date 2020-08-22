﻿using System;
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

    [SerializeField] public GameObject startCanvas;
    [SerializeField] public GameObject gameCanvas;
    [SerializeField] public GameObject endCanvas;

    private int _currentSessionFullScore;
    private GameState _gameState;

    private void Start()
    {
        ChangeGameState(GameState.Idle);
    }

    private void FixedUpdate()
    {
        DisplayScore();
        FallChecker();
    }

    private void FallChecker()
    {
        if (player.MaxHeight - Camera.main.orthographicSize > player.transform.position.y)
        {
            LoseProcess();
        }
    }

    private void LoseProcess()
    {
        ChangeGameState(GameState.Lose);
        
        player.Restart();
        platformsManager.Restart();
        cameraScript.Restart();
    }

    private void DisplayScore()
    {
        var text = gameCanvas.GetComponentInChildren<Text>();
        text.text = Math.Floor(player.MaxHeight).ToString();
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
}