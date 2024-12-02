using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : MonoBehaviour
{
    private IdleEvent idleEvent;
    private CharacterController characterController;

    private void Awake()
    {
        idleEvent = GetComponent<IdleEvent>();
        characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        idleEvent.OnIdle += Idle_OnIdle;
    }
    private void OnDisable()
    {
        idleEvent.OnIdle -= Idle_OnIdle;
    }

    private void Idle_OnIdle()
    {
        characterController.Move(Vector3.zero);
    }
}
