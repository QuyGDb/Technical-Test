using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MultiObjectSpawner
{
    [SerializeField] private GameObject[] obstaclePrefabs = new GameObject[9];
    private int baseObstacleType = 5;
    protected int nearEndRoadOffset = 35;
    private int positionZboundEnd;
    private WaitForSeconds respawnTime = new WaitForSeconds(0.5f);

    /// <summary>
    /// Respawn the obstacle to a new random position
    /// </summary>
    public override void SpawnObject(LevelDetails levelDetails)
    {
        List<Vector3> randomSpawnPositionList = GetRandomSpawnPositionList(levelDetails);
        for (int i = 0; i < levelDetails.obstaclesQuantity; i++)
        {
            GameObject randomObstacle = obstaclePrefabs[Random.Range(0, CalculateObjectTypeAppear(levelDetails.level))];
            Vector3 eulerAngles = new Vector3(0, Random.Range(0, 360), 0);
            Quaternion rotation = Quaternion.Euler(eulerAngles);
            Instantiate(randomObstacle, randomSpawnPositionList[i], rotation, transform);
        }
    }
    protected override int CalculateObjectTypeAppear(int level)
    {
        int count = level / 2;
        if (baseObstacleType + count > 9)
        {
            return 9;
        }
        return baseObstacleType + count;
    }


    protected override List<Vector3> GetRandomSpawnPositionList(LevelDetails levelDetails)
    {

        positionZboundEnd = levelDetails.phaseOneRoadSegmentCount * Settings.roadSegmentLength - nearEndRoadOffset;
        int controlledObstacleCount = levelDetails.obstaclesQuantity / 2;
        float distanceBetweenObstacles = (positionZboundEnd - positionZboundStart) / controlledObstacleCount;
        List<Vector3> controlledRandomObstacles = new List<Vector3>();

        for (int i = 0; i < controlledObstacleCount; i++)
        {
            Vector3 controlledPositon = new Vector3(Random.Range(positionXboundLeft, positionXboundRight),
                0, positionZboundStart + distanceBetweenObstacles * i + Random.Range(-2, 2));
            controlledRandomObstacles.Add(controlledPositon);
        }
        for (int i = controlledObstacleCount; i < levelDetails.obstaclesQuantity; i++)
        {
            Vector3 randomPositon = new Vector3(Random.Range(positionXboundLeft, positionXboundRight),
                0, Random.Range(positionZboundStart, positionZboundEnd));
            controlledRandomObstacles.Add(randomPositon);
        }
        return controlledRandomObstacles;
    }

    protected override IEnumerator RespawnCoroutine(SpawnedObject spawnedObject)
    {
        spawnedObject.gameObject.SetActive(false);
        yield return respawnTime;
        spawnedObject.transform.position = new Vector3(Random.Range(positionXboundLeft, positionXboundRight), 0, Random.Range(positionZboundStart, positionZboundEnd));
        spawnedObject.gameObject.SetActive(true);
    }


    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckEnumerableValues(this, nameof(obstaclePrefabs), obstaclePrefabs);
    }
#endif
    #endregion
}
