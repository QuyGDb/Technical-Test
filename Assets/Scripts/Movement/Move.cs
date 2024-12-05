using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private CharacterController controller;
    [SerializeField] private float speed = 10f;
    private MoveEvent moveEvent;
    private void Awake()
    {
        moveEvent = GetComponent<MoveEvent>();
        controller = GetComponent<CharacterController>();
        if (PlayerPrefs.HasKey("Speed"))
        {
            speed += PlayerPrefs.GetFloat("Speed");
        }
    }

    private void OnEnable()
    {
        moveEvent.OnMove += Move_OnMove;
        GameManager.Instance.OnGameStateChange += OnGameStateChange_Move;
    }
    private void OnDisable()
    {
        moveEvent.OnMove -= Move_OnMove;
        GameManager.Instance.OnGameStateChange -= OnGameStateChange_Move;
    }
    private void Update()
    {
        if (GameManager.Instance.gameState == GameState.LineTwo)
        {
            controller.Move(Vector3.forward * speed * Time.deltaTime);
            Debug.Log("Move" + speed);
        }
    }
    private void Move_OnMove(Vector2 direction)
    {
        Vector3 move = new Vector3(direction.x, 0, direction.y);
        controller.Move(move * speed * Time.deltaTime);
        controller.transform.LookAt(controller.transform.position + move);
    }
    private void OnGameStateChange_Move(GameState gameState)
    {
        if (gameState == GameState.LineTwo)
        {
            gameObject.transform.DORotate(new Vector3(0, 0, 0), 1f);

        }

    }

    public void IncreaseSpeed(AnimalType animalType)
    {
        switch (animalType)
        {
            case AnimalType.Cat1:
                speed += 1;
                break;
            case AnimalType.Cat2:
                speed += 2;
                break;
            case AnimalType.Cat3:
                speed += 3;
                break;
            case AnimalType.Cat4:
                speed += 4;
                break;
            case AnimalType.Cat5:
                speed += 5;
                break;

        }
    }

}
