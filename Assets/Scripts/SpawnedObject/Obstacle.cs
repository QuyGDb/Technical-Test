using System.Collections;
using UnityEngine;

public class Obstacle : SpawnedObject
{
    private int sizeLength = 4;
    public int count = 0;


    protected override void OnEnable()
    {
        count++;
        Debug.Log("Obstacle count: " + count + "frame: " + Time.frameCount + " " + gameObject.name);
        base.OnEnable();
    }

    protected override void ProcessOverlapping()
    {
        Vector3 size = new Vector3(boxCollider.size.z + sizeLength, boxCollider.size.y, boxCollider.size.z + sizeLength);
        Collider[] hitColliders = new Collider[1];
        hitColliders = Physics.OverlapBox(boxCollider.bounds.center, size / 2, Quaternion.identity);
        if (hitColliders.Length > 0)

            if ((layerMask.value & 1 << hitColliders[1].gameObject.layer) > 0 && hitColliders[1].gameObject != gameObject)
            {
                Debug.Log("Obstacle ProcessOverlapping" + hitColliders.Length + "frame: " + Time.frameCount + " " + gameObject.name);

                StaticEventHandler.CallObstacleOverlappedEvent(this);

            }

    }
}

