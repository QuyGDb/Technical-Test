using System.Collections;
using UnityEngine;

public class Obstacle : SpawnedObject
{
    private int sizeLength = 4;


    protected override void ProcessOverlapping()
    {
        Vector3 size = new Vector3(boxCollider.size.x + sizeLength, boxCollider.size.y, boxCollider.size.z + sizeLength);
        Debug.Log(size);
        Collider[] hitColliders = new Collider[1];
        hitColliders = Physics.OverlapBox(boxCollider.bounds.center, size / 2, Quaternion.identity);
        foreach (var hitCollider in hitColliders)
        {
            Debug.Log(hitCollider.gameObject.name);
            if (hitCollider == boxCollider)
                continue;
            if ((layerMask.value & 1 << hitCollider.gameObject.layer) > 0)
            {
                StaticEventHandler.CallObstacleOverlappedEvent(this);
                break;
            }

        }
    }
}
