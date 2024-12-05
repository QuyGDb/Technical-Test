using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatePlayer : MonoBehaviour
{
    private IdleEvent idleEvent;
    private MoveEvent moveEvent;
    private Animator animator;

    private void Awake()
    {
        idleEvent = GetComponent<IdleEvent>();
        moveEvent = GetComponent<MoveEvent>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        idleEvent.OnIdle += Idle_OnIdle;
        moveEvent.OnMove += Move_OnMove;
        GameManager.Instance.OnGameStateChange += OnGameStateChange_AnimatePlayer;

    }
    private void OnDisable()
    {
        idleEvent.OnIdle -= Idle_OnIdle;
        moveEvent.OnMove -= Move_OnMove;
        GameManager.Instance.OnGameStateChange += OnGameStateChange_AnimatePlayer;

    }

    private void OnGameStateChange_AnimatePlayer(GameState gameState)
    {
        if (gameState == GameState.LineTwo)
        {
            idleEvent.OnIdle -= Idle_OnIdle;
            moveEvent.OnMove -= Move_OnMove;
        }
        if (gameState == GameState.Lost || gameState == GameState.Win)
        {
            animator.SetBool(Settings.Run, false);
        }
    }
    private void Update()
    {
        if (GameManager.Instance.gameState == GameState.LineTwo)
        {
            animator.SetBool(Settings.Run, true);
            animator.SetFloat(Settings.MovementMultiplier, 1);
        }
    }
    private void Idle_OnIdle()
    {
        animator.SetBool(Settings.Run, false);
    }

    private void Move_OnMove(Vector2 direction)
    {
        animator.SetFloat(Settings.MovementMultiplier, direction.magnitude);
        animator.SetBool(Settings.Run, true);
    }
}
