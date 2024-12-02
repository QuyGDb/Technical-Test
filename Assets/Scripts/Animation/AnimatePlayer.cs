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
    }
    private void OnDisable()
    {
        idleEvent.OnIdle -= Idle_OnIdle;
        moveEvent.OnMove -= Move_OnMove;
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
