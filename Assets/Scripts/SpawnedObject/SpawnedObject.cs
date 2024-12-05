using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnedObject : MonoBehaviour
{
    protected BoxCollider boxCollider;
    protected LayerMask layerMask;

    protected virtual void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        layerMask = LayerMask.GetMask("Obstacles");
    }
    protected virtual void OnEnable()
    {
        ProcessOverlapping();
    }

    protected abstract void ProcessOverlapping();


}
