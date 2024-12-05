using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    [SerializeField] private GameObject roadSegmentPrefab;
    int phaseOneRoadSegmentCount;
    public void SpawnPhaseOneRoad(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(roadSegmentPrefab, new Vector3(0, 0, i * 100), Quaternion.identity, transform);
        }
        phaseOneRoadSegmentCount = count;
    }
    public void SpawnPhaseTwoRoad(int count)
    {
        for (int i = phaseOneRoadSegmentCount; i < phaseOneRoadSegmentCount + count; i++)
        {
            Instantiate(roadSegmentPrefab, new Vector3(0, 0, i * 100), Quaternion.identity, transform);
        }
    }
}
