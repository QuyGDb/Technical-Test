using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : SingletonMonobehaviour<GameManager>
{

    [HideInInspector] public LevelDetails currentLevel;
    [HideInInspector] public Action<GameState> OnGameStateChange;
    public GameState gameState;
    #region Vcam
    [SerializeField] private Cinemachine.CinemachineVirtualCamera secoundCamera;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera startCamera;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera MainCamera;
    #endregion
    public GameObject catIconContainer;
    public Transform playerTransform;
    public Transform tsunamiTransform;
    [HideInInspector] public int pickedUpAnimalCount;
    private bool isPhaseOneCompleted = true;
    private bool isPhaseTwoCompleted = true;
    [HideInInspector] public float timer;
    public void AddPickedUpAnimal()
    {
        pickedUpAnimalCount++;
        if (pickedUpAnimalCount == currentLevel.animalsQuantity)
        {
            HandleGameState(GameState.LineTwo);
        }

    }

    public void HandleGameState(GameState gameState)
    {
        this.gameState = gameState;
        switch (gameState)
        {
            case GameState.Start:
                startCamera.gameObject.SetActive(true);
                MainCamera.gameObject.SetActive(false);
                break;
            case GameState.LineOne:
                timer = 0f;
                startCamera.gameObject.SetActive(false);
                MainCamera.gameObject.SetActive(true);
                break;
            case GameState.LineTwo:
                Time.timeScale = 2f;
                secoundCamera.gameObject.SetActive(true);
                break;
            case GameState.Win:
                if (Settings.currentLevel == Settings.selectedLevel)
                {
                    Settings.currentLevel++;
                    PlayerPrefs.SetInt("Level", Settings.currentLevel);
                    PlayerPrefs.Save();
                }
                Time.timeScale = 1f;
                break;
            case GameState.Lost:
                Time.timeScale = 1f;
                break;
        }
        OnGameStateChange?.Invoke(gameState);
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (gameState == GameState.LineOne)
        {
            if (playerTransform.position.z >= currentLevel.phaseOneLength && isPhaseOneCompleted)
            {
                HandleGameState(GameState.LineTwo);
                isPhaseOneCompleted = false;
            }

        }
        if (gameState == GameState.LineTwo)
        {
            if (playerTransform.position.z >= currentLevel.phaseOneLength + currentLevel.phaseTwoLength && isPhaseTwoCompleted)
            {
                if (pickedUpAnimalCount == currentLevel.animalsQuantity)
                {
                    HandleGameState(GameState.Win);
                }
                else
                {
                    HandleGameState(GameState.Lost);
                }
                isPhaseTwoCompleted = false;
            }
        }
    }
}
