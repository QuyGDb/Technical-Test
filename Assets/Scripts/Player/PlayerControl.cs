using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public FloatingJoystick joystick;
    private IdleEvent idleEvent;
    private MoveEvent moveEvent;

    private void OnEnable()
    {
        GameManager.Instance.OnGameStateChange += OnGameStateChange_PlayerControl;
    }
    private void OnDisable()
    {
        GameManager.Instance.OnGameStateChange -= OnGameStateChange_PlayerControl;
    }

    private void OnGameStateChange_PlayerControl(GameState gameState)
    {
        if (gameState == GameState.Start)
        {
            joystick.gameObject.SetActive(false);
        }
        if (gameState == GameState.LineOne)
        {
            joystick.gameObject.SetActive(true);
        }
        if (gameState == GameState.LineTwo)
        {
            joystick.gameObject.SetActive(false);
        }
        if (gameState == GameState.Win || gameState == GameState.Lost)
        {
            joystick.gameObject.SetActive(false);
        }
    }
    private void Awake()
    {
        idleEvent = GetComponent<IdleEvent>();
        moveEvent = GetComponent<MoveEvent>();
    }
    private void Update()
    {
        if (joystick.Direction == Vector2.zero && joystick.gameObject.activeSelf == true)
        {
            idleEvent.CallIdleEvent();
        }
        else
        {
            moveEvent.CallMoveEvent(joystick.Direction);
        }
    }

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckNullValue(this, nameof(joystick), joystick);
    }
#endif
    #endregion
}
