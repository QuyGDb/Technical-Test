using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEvent : MonoBehaviour
{
    public event Action<Vector2> OnMove;

    public void CallMoveEvent(Vector2 direction)
    {
        OnMove?.Invoke(direction);
    }
}
