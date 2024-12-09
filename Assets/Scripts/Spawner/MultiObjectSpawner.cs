using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MultiObjectSpawner : MonoBehaviour
{

    protected float positionXboundLeft = -16.5f;
    protected float positionXboundRight = 3.5f;
    protected int positionZboundStart = 30;


    protected void Respawn(SpawnedObject spawnedObject)
    {
        //obstacle.transform.position = new Vector3(Random.Range(positionXboundLeft, positionXboundRight), 0, Random.Range(positionZboundStart, positionZboundEnd));
        // cant not setactive immediately because obstacle is inactive in same frame
        //obstacle.gameObject.SetActive(true);
        StartCoroutine(RespawnCoroutine(spawnedObject));
    }
    protected abstract IEnumerator RespawnCoroutine(SpawnedObject spawnedObject);


    public abstract void SpawnObject(LevelDetails levelDetails);

    protected abstract List<Vector3> GetRandomSpawnPositionList(LevelDetails levelDetails);

    protected abstract int CalculateObjectTypeAppear(int level);


}
