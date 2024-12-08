using System.Collections;
using UnityEngine;

public class Obstacle : SpawnedObject
{
    private int sizeLength = 4;
    public int count = 0;
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnEnable()
    {
        count++;
        base.OnEnable();
    }

    protected override void ProcessOverlapping()
    {
        Vector3 size = new Vector3(boxCollider.size.z + sizeLength, boxCollider.size.y, boxCollider.size.z + sizeLength);
        Collider[] hitColliders = Physics.OverlapBox(boxCollider.bounds.center, size / 2, Quaternion.identity);

        foreach (var hitCollider in hitColliders)
        {
            if ((layerMask.value & 1 << hitCollider.gameObject.layer) > 0 && hitCollider.gameObject != gameObject)
            {
                StaticEventHandler.CallObjectOverlappedEvent(this);

            }
        }
    }

}
