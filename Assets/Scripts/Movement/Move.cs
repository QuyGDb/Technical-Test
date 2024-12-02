using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private CharacterController controller;
    private float speed = 5f;
    private MoveEvent moveEvent;
    private void Awake()
    {
        moveEvent = GetComponent<MoveEvent>();
        controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        moveEvent.OnMove += Move_OnMove;
    }
    private void OnDisable()
    {
        moveEvent.OnMove -= Move_OnMove;
    }

    private void Move_OnMove(Vector2 direction)
    {
        Vector3 move = new Vector3(direction.x, 0, direction.y);
        controller.Move(move * speed * Time.deltaTime);
        controller.transform.LookAt(controller.transform.position + move);
    }

}
