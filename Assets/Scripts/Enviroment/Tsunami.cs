using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tsunami : MonoBehaviour
{
    private float velocity;
    private float phaseOneLength;
    private float phaseTwoLength;
    private void Awake()
    {
        GameManager.Instance.OnGameStateChange += OnGameStateChange_Tsunami;
    }
    private void OnDisable()
    {
        GameManager.Instance.OnGameStateChange -= OnGameStateChange_Tsunami;
    }
    private void OnDestroy()
    {
        DOTween.Kill(transform);
    }

    private void OnGameStateChange_Tsunami(GameState gameState)
    {
        if (gameState == GameState.Start)
        {
            transform.DOMoveZ(0, 15f).SetEase(Ease.Linear).OnComplete(() =>
            {
                velocity = GameManager.Instance.currentLevel.tsunamiVelocityPhaseOne;
                phaseOneLength = GameManager.Instance.currentLevel.phaseOneLength;
                float time = Time.time;
                transform.DOMoveZ(phaseOneLength, phaseOneLength / velocity).SetEase(Ease.Linear).OnComplete(() =>
                {
                    velocity = GameManager.Instance.currentLevel.tsunamiVelocityPhaseTwo;
                    phaseTwoLength = GameManager.Instance.currentLevel.phaseTwoLength;
                    transform.DOMoveZ(phaseOneLength + phaseTwoLength, phaseTwoLength / velocity).SetEase(Ease.Linear);
                });

            });
        }

        if (gameState == GameState.Lost || gameState == GameState.Win)
        {
            DOTween.Kill(transform);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.HandleGameState(GameState.Lost);
        }
    }
}
