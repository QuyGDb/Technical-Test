using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Animal : SpawnedObject
{
    [HideInInspector] public int animalID;
    private LayerMask pickUpLayer;
    private Animator animator;
    [HideInInspector] public bool isFollowingPlayer = false;
    [SerializeField] private Slider slider;
    [SerializeField] private Image notice;
    [SerializeField] private AnimalType animalType;
    [SerializeField] private GameObject catIconPrefabs;
    private GameObject catIcon;
    protected override void Awake()
    {
        base.Awake();
        pickUpLayer = LayerMask.GetMask("PickUp");
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        slider.transform.DORotate(new Vector3(0, 0, 0), 0.5f);
        notice.transform.DORotate(new Vector3(0, 0, 0), 0.5f);
        slider.gameObject.SetActive(false);
        GameManager.Instance.OnGameStateChange += OnGameStateChange_Animal;
    }


    private void OnDestroy()
    {
        GameManager.Instance.OnGameStateChange -= OnGameStateChange_Animal;
    }

    private void OnGameStateChange_Animal(GameState gameState)
    {
        if (gameState == GameState.LineOne)
        {
            catIcon = Instantiate(catIconPrefabs, GameManager.Instance.catIconContainer.transform);

            RectTransform rectTransform = catIcon.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0, 0.5f);
            rectTransform.anchorMax = new Vector2(0, 0.5f);
            rectTransform.anchoredPosition = new Vector3(CalculateCatIconPosition(), 0, 0);
        }
    }

    private float CalculateCatIconPosition()
    {
        float catPositonpercent = transform.position.z / (float)GameManager.Instance.currentLevel.phaseOneLength;
        float phaseOneLengthPercnet = (float)GameManager.Instance.currentLevel.phaseOneLength / (float)(GameManager.Instance.currentLevel.phaseOneLength + GameManager.Instance.currentLevel.phaseTwoLength);
        float catIconPosition = catPositonpercent * phaseOneLengthPercnet * Settings.catIconContainerLength;
        return catIconPosition;
    }

    protected override void ProcessOverlapping()
    {
        Collider[] hitColliders = Physics.OverlapBox(boxCollider.bounds.center, boxCollider.bounds.size / 2, Quaternion.identity);
        foreach (var hitCollider in hitColliders)
        {
            if ((layerMask.value & 1 << hitCollider.gameObject.layer) > 0 && hitCollider.gameObject != gameObject)
            {
                StaticEventHandler.CallObjectOverlappedEvent(this);
                gameObject.SetActive(false);

            }
        }
    }
    private void Update()
    {
        if (isFollowingPlayer)
        {
            PlayerControl playerControl = gameObject.transform.parent.GetComponent<PlayerControl>();
            if (playerControl != null)
            {
                if (playerControl.joystick.Direction == Vector2.zero)
                {
                    animator.SetBool(Settings.Run, false);
                }
                else
                {
                    animator.SetBool(Settings.Run, true);
                }
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((pickUpLayer.value & 1 << other.gameObject.layer) > 0)
        {
            other.GetComponent<MeshRenderer>().enabled = true;
            StopAllCoroutines();
            StartCoroutine(MakeAnimalFollowPlayer(other.transform.parent));
        }
        if (other.gameObject.CompareTag("Tsunami"))
        {
            catIcon.SetActive(false);
            gameObject.SetActive(false);

        }

    }


    private void OnTriggerExit(Collider other)
    {
        if ((pickUpLayer.value & 1 << other.gameObject.layer) > 0)
        {
            other.GetComponent<MeshRenderer>().enabled = false;
            slider.gameObject.SetActive(false);
            StopAllCoroutines();
        }
    }

    private IEnumerator MakeAnimalFollowPlayer(Transform playerTransform)
    {
        slider.gameObject.SetActive(true);
        slider.DOValue(1, 1f);
        yield return new WaitForSeconds(1f);
        Vector3 behindPlayerLocalPosition = -Vector3.forward * (2 + animalID * 2f);
        gameObject.transform.SetParent(playerTransform);
        ResetAnimalRotation();
        gameObject.transform.DOLocalMove(behindPlayerLocalPosition, 0.5f);
        isFollowingPlayer = true;
        playerTransform.GetComponent<Move>().IncreaseSpeed(animalType);
        notice.gameObject.SetActive(false);
        catIcon.SetActive(false);
        StaticEventHandler.CallPickUpAnimalEvent();
        GameManager.Instance.AddPickedUpAnimal();
    }

    private void ResetAnimalRotation()
    {
        Quaternion rotation = Quaternion.Euler(0, 0, 0);
        gameObject.transform.localRotation = rotation;
    }

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckNullValue(this, nameof(slider), slider);
        HelperUtilities.ValidateCheckNullValue(this, nameof(notice), notice);
        HelperUtilities.ValidateCheckNullValue(this, nameof(catIconPrefabs), catIconPrefabs);
    }
#endif
    #endregion
}
