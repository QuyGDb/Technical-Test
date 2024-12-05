using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MultiObjectSpawner : MonoBehaviour
{

    protected float positionXboundLeft = -16.5f;
    protected float positionXboundRight = 3.5f;
    protected int positionZboundStart = 30;
    protected int positionZboundEnd;

    protected virtual void OnEnable()
    {
        StaticEventHandler.OnObjectOverlapped += StaticEventHandler_ObjectOverlapped;
    }

    protected virtual void OnDisable()
    {
        StaticEventHandler.OnObjectOverlapped -= StaticEventHandler_ObjectOverlapped;
    }
    protected virtual void StaticEventHandler_ObjectOverlapped(SpawnedObject spawnedObject)
    {
        Respawn(spawnedObject);
    }
    protected void Respawn(SpawnedObject spawnedObject)
    {
        //obstacle.transform.position = new Vector3(Random.Range(positionXboundLeft, positionXboundRight), 0, Random.Range(positionZboundStart, positionZboundEnd));
        // cant not setactive immediately because obstacle is inactive in same frame
        //obstacle.gameObject.SetActive(true);
        StartCoroutine(RespawnCoroutine(spawnedObject));
    }
    protected IEnumerator RespawnCoroutine(SpawnedObject spawnedObject)
    {
        spawnedObject.transform.position = new Vector3(Random.Range(positionXboundLeft, positionXboundRight), 0, Random.Range(positionZboundStart, positionZboundEnd));
        yield return null;
        spawnedObject.gameObject.SetActive(true);
    }

    public abstract void SpawnObject(LevelDetails levelDetails);

    protected abstract List<Vector3> GetRandomSpawnPositionList(LevelDetails levelDetails);

    protected abstract int CalculateObjectTypeAppear(int level);


}
