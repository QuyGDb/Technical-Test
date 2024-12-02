using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private FloatingJoystick joystick;
    private IdleEvent idleEvent;
    private MoveEvent moveEvent;

    private void Awake()
    {
        idleEvent = GetComponent<IdleEvent>();
        moveEvent = GetComponent<MoveEvent>();
    }
    private void Update()
    {
        if (joystick.Direction == Vector2.zero)
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
