using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class DirectionArrow : MonoBehaviour
{
    [SerializeField] private AnimalSpawner animalSpawner;
    Transform currentAnimal;
    public bool callOneTime = false;
    [SerializeField] private Transform player;
    private void Start()
    {
        StaticEventHandler.OnPickUpAnimal += OnPickUpAnimal;

        GameManager.Instance.OnGameStateChange += OnGameStateChange;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        StaticEventHandler.OnPickUpAnimal -= OnPickUpAnimal;
        GameManager.Instance.OnGameStateChange -= OnGameStateChange;
    }

    private void OnGameStateChange(GameState gameState)
    {
        if (gameState == GameState.LineOne)
        {
            gameObject.SetActive(true);
            currentAnimal = IsNearPlayer();
        }
        if (gameState == GameState.LineTwo)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnPickUpAnimal()
    {
        if (GameManager.Instance.pickedUpAnimalCount < animalSpawner.spawnedAnimals.Count)
            currentAnimal = IsNearPlayer();
    }

    private void Update()
    {
        transform.position = player.position;
        if (animalSpawner != null && animalSpawner.spawnedAnimals.Count > 0)
            AimArrowAtTarget();
    }

    private void AimArrowAtTarget()
    {

        //float angle = Vector3.SignedAngle(transform.position, currentAnimal.position, Vector3.up);
        //transform.rotation = Quaternion.Euler(90, angle, 0);
        //Debug.Log(angle);
        transform.LookAt(currentAnimal);
    }
    private Transform IsNearPlayer()
    {
        float distance = 1000f;
        Transform nearAnimal = null;
        foreach (var animal in animalSpawner.spawnedAnimals)
        {
            if (animal.GetComponent<Animal>().isFollowingPlayer)
                continue;
            if (Mathf.Abs(animal.transform.position.z - transform.position.z) < distance)
            {
                distance = Vector3.Distance(animal.transform.position, transform.position);
                nearAnimal = animal.transform;
            }
        }
        return nearAnimal;
    }

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckNullValue(this, nameof(animalSpawner), animalSpawner);
        HelperUtilities.ValidateCheckNullValue(this, nameof(player), player);
    }
#endif
    #endregion
}