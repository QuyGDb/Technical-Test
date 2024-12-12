using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ObstacleSpawner : MultiObjectSpawner
{
    [SerializeField] private GameObject[] obstaclePrefabs = new GameObject[9];
    private int baseObstacleType = 5;
    protected int nearEndRoadOffset = 35;
    private int positionZboundEnd;
    private Queue<Action> OverlappingActionQueue = new Queue<Action>();
    /// <summary>
    /// Respawn the obstacle to a new random position
    /// </summary

    private void Awake()
    {
        StaticEventHandler.OnObstacleOverlap += OnObstacleOverlapped;
    }

    private void OnDestroy()
    {
        StaticEventHandler.OnObstacleOverlap -= OnObstacleOverlapped;
    }
    private void Update()
    {
        if (OverlappingActionQueue.Count == 0)
            return;
        OverlappingActionQueue.Dequeue().Invoke();
    }
    private void OnObstacleOverlapped(SpawnedObject spawned)
    {
        OverlappingActionQueue.Enqueue(() => Respawn(spawned));
        Debug.Log(OverlappingActionQueue.Count);
    }

    public override void SpawnObject(LevelDetails levelDetails)
    {
        List<Vector3> randomSpawnPositionList = GetRandomSpawnPositionList(levelDetails);
        for (int i = 0; i < levelDetails.obstaclesQuantity; i++)
        {
            GameObject randomObstacle = obstaclePrefabs[UnityEngine.Random.Range(0, CalculateObjectTypeAppear(levelDetails.level))];
            Vector3 eulerAngles = new Vector3(0, UnityEngine.Random.Range(0, 360), 0);
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
        //positionZboundEnd = 50;
        int controlledObstacleCount = levelDetails.obstaclesQuantity / 2;
        float distanceBetweenObstacles = (positionZboundEnd - positionZboundStart) / controlledObstacleCount;
        List<Vector3> controlledRandomObstacles = new List<Vector3>();

        for (int i = 0; i < controlledObstacleCount; i++)
        {
            Vector3 controlledPositon = new Vector3(UnityEngine.Random.Range(positionXboundLeft, positionXboundRight),
                0, positionZboundStart + distanceBetweenObstacles * i + UnityEngine.Random.Range(-2, 2));
            controlledRandomObstacles.Add(controlledPositon);
        }
        for (int i = controlledObstacleCount; i < levelDetails.obstaclesQuantity; i++)
        {
            Vector3 randomPositon = new Vector3(UnityEngine.Random.Range(positionXboundLeft, positionXboundRight),
                0, UnityEngine.Random.Range(positionZboundStart, positionZboundEnd));
            controlledRandomObstacles.Add(randomPositon);
        }
        return controlledRandomObstacles;
    }

    protected override IEnumerator RespawnCoroutine(SpawnedObject spawnedObject)
    {
        spawnedObject.gameObject.SetActive(false);
        yield return null;
        spawnedObject.transform.position = new Vector3(UnityEngine.Random.Range(positionXboundLeft, positionXboundRight), 0, UnityEngine.Random.Range(positionZboundStart, positionZboundEnd));
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
