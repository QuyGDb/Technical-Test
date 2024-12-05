using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticEventHandler
{
    public static event Action<SpawnedObject> OnObjectOverlapped;

    public static void CallObjectOverlappedEvent(SpawnedObject spawnedObject)
    {
        OnObjectOverlapped?.Invoke(spawnedObject);
    }

    public static event Action OnPickUpAnimal;

    public static void CallPickUpAnimalEvent()
    {
        OnPickUpAnimal?.Invoke();
    }
}
