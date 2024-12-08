using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawner : MultiObjectSpawner
{
    public GameObject[] animalPrefabs = new GameObject[5];
    private int baseAnimalsType = 2;
    protected int nearEndRoadOffset = 10;
    [HideInInspector] public List<GameObject> spawnedAnimals = new List<GameObject>();
    private int positionZboundEnd;
    /// <summary>
    /// Need first object start at around positionZboundStart, so offset = 1 to seaparate other objects
    /// </summary>
    private int offset = 1;
    public override void SpawnObject(LevelDetails levelDetails)
    {
        List<Vector3> randomSpawnPositionList = GetRandomSpawnPositionList(levelDetails);
        for (int i = 0; i < levelDetails.animalsQuantity; i++)
        {
            GameObject randomObstacle = animalPrefabs[Random.Range(0, CalculateObjectTypeAppear(levelDetails.level))];
            Vector3 eulerAngles = new Vector3(0, Random.Range(0, 360), 0);
            Quaternion rotation = Quaternion.Euler(eulerAngles);
            GameObject spawnedAnimal = Instantiate(randomObstacle, randomSpawnPositionList[i], rotation, transform);
            spawnedAnimal.GetComponent<Animal>().animalID = i;
            spawnedAnimals.Add(spawnedAnimal);
        }
    }

    protected override int CalculateObjectTypeAppear(int level)
    {
        int count = level / 2;
        if (baseAnimalsType + count > 5)
        {
            return 5;
        }
        return baseAnimalsType + count;
    }

    protected override List<Vector3> GetRandomSpawnPositionList(LevelDetails levelDetails)
    {
        positionZboundEnd = levelDetails.phaseOneRoadSegmentCount * Settings.roadSegmentLength - nearEndRoadOffset;
        int distanceBetweenAnimal = (positionZboundEnd - positionZboundStart) / (levelDetails.animalsQuantity - offset);
        List<Vector3> randomSpawnPositionList = new List<Vector3>();
        for (int i = 0; i < levelDetails.animalsQuantity; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(positionXboundLeft, positionXboundRight),
                0, positionZboundStart + distanceBetweenAnimal * i + Random.Range(-5, 5));
            randomSpawnPositionList.Add(randomPosition);
        }
        return randomSpawnPositionList;
    }

    protected override IEnumerator RespawnCoroutine(SpawnedObject spawnedObject)
    {

        yield return null;
        spawnedObject.transform.position = new Vector3(Random.Range(positionXboundLeft, positionXboundRight), 0, Random.Range(positionZboundStart, positionZboundEnd));
        spawnedObject.gameObject.SetActive(true);
    }

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckEnumerableValues(this, nameof(animalPrefabs), animalPrefabs);
    }
#endif
    #endregion
}
