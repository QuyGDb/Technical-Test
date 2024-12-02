using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleEvent : MonoBehaviour
{
    public event Action OnIdle;

    public void CallIdleEvent()
    {
        OnIdle?.Invoke();
    }
}
