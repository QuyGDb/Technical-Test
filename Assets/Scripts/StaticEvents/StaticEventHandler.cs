using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticEventHandler
{

    // be careful with static events, in this case, we dont use 1 event for obstacle and animal overlap, we use 2 different events
    public static event Action<SpawnedObject> OnObstacleOverlap;
    public static void CallObstacleOverlappedEvent(SpawnedObject spawnedObject)
    {
        OnObstacleOverlap?.Invoke(spawnedObject);
    }

    public static event Action<SpawnedObject> OnAnimalOverlapped;

    public static void CallAnimalOverlappedEvent(SpawnedObject spawnedObject)
    {
        OnAnimalOverlapped?.Invoke(spawnedObject);
    }
    public static event Action OnPickUpAnimal;

    public static void CallPickUpAnimalEvent()
    {
        OnPickUpAnimal?.Invoke();
    }
}
