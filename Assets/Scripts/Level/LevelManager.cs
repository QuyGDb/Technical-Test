using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    [SerializeField] private BaseLevelSO baseLevel;
    [SerializeField] private LevelConfigSO levelConfig;
    [SerializeField] private RoadSpawner roadSpawner;
    [SerializeField] private ObstacleSpawner obstacleSpawner;
    [SerializeField] private AnimalSpawner animalSpawner;
    [SerializeField] private FinishUISpawner finishUISpawner;

    private void Start()
    {

        GenarateLevel(1);
        GameManager.Instance.HandleGameState(GameState.Start);
        StartCoroutine(ChangeState());

    }

    public IEnumerator ChangeState()
    {
        yield return new WaitForSeconds(3);
        GameManager.Instance.HandleGameState(GameState.LineOne);
    }
    private void OnEnable()
    {
        GameManager.Instance.OnGameStateChange += OnGameStateChange_LevelManager;
    }
    private void OnDisable()
    {
        GameManager.Instance.OnGameStateChange -= OnGameStateChange_LevelManager;
    }

    private void OnGameStateChange_LevelManager(GameState gameState)
    {
        if (gameState == GameState.LineTwo)
        {
            obstacleSpawner.gameObject.SetActive(false);
        }
    }
    private void GenarateLevel(int level)
    {
        LevelDetails levelDetails = CalculateLevelDetails(level);
        GameManager.Instance.currentLevel = levelDetails;
        roadSpawner.SpawnPhaseOneRoad(levelDetails.phaseOneRoadSegmentCount);
        roadSpawner.SpawnPhaseTwoRoad(levelDetails.phaseTwoRoadSegmentCount);
        obstacleSpawner.SpawnObject(levelDetails);
        animalSpawner.SpawnObject(levelDetails);
        finishUISpawner.SpawnFinishUI(levelDetails);

    }
    private LevelDetails CalculateLevelDetails(int level)
    {
        LevelDetails levelDetails;
        levelDetails.level = level;
        levelDetails.phaseOneRoadSegmentCount = CalculatePhaseOneRoadSegmentsCountForLevel(level);
        levelDetails.phaseOneLength = levelDetails.phaseOneRoadSegmentCount * Settings.roadSegmentLength;
        levelDetails.phaseTwoRoadSegmentCount = CalculatePhaseTwoRoadSegmentsCountForLevel(level);
        levelDetails.phaseTwoLength = levelDetails.phaseTwoRoadSegmentCount * Settings.roadSegmentLength;
        levelDetails.obstaclesQuantity = CalculateObstacesForLevel(level);
        levelDetails.animalsQuantity = CalculateAnimalsForLevel(level);
        levelDetails.tsunamiVelocityPhaseOne = CalculatePhaseOneTsunamiVelocityForLevel(level);
        levelDetails.tsunamiVelocityPhaseTwo = CalculatePhaseTwoTsunamiVelocityForLevel(level);
        return levelDetails;
    }

    private int CalculateObstacesForLevel(int level)
    {
        int baseObstacleInOneSegment = baseLevel.obstaclesQuantity / 2;
        if (level == 0)
        {
            return baseLevel.obstaclesQuantity;
        }
        if (level % levelConfig.levelMultiplierLineOneInterval == 0 && level >= 1)
        {
            int multiplier = level / levelConfig.levelMultiplierLineOneInterval;
            return baseLevel.obstaclesQuantity + levelConfig.obstacleLevelMultiplier * level + baseObstacleInOneSegment * multiplier;
        }
        if (level >= 1)
        {
            return baseLevel.obstaclesQuantity + levelConfig.obstacleLevelMultiplier * level;
        }
        return 0;

    }

    private int CalculateAnimalsForLevel(int level)
    {
        if (level == 0)
        {
            return baseLevel.animalsQuantity;
        }
        if (level >= 1)
        {
            return baseLevel.animalsQuantity + levelConfig.animalLevelMultiplier * level;
        }
        return 0;
    }

    private float CalculatePhaseOneTsunamiVelocityForLevel(int level)
    {
        if (level == 0)
        {
            return baseLevel.tsunamiVelocityPhaseOne;
        }

        if (level >= 1)
        {
            return baseLevel.tsunamiVelocityPhaseOne + levelConfig.tsunamiVelocityLineOneMultiplier * level;
        }
        return 0;
    }
    private float CalculatePhaseTwoTsunamiVelocityForLevel(int level)
    {
        if (level == 0)
        {
            return baseLevel.tsunamiVelocityPhaseTwo;
        }
        if (level >= 1)
        {
            return baseLevel.tsunamiVelocityPhaseTwo + levelConfig.tsunamiVelocityLineTwoMultiplier * level;
        }
        return 0;
    }

    private int CalculatePhaseOneRoadSegmentsCountForLevel(int level)
    {
        if (level % levelConfig.levelMultiplierLineOneInterval == 0 && level >= 1)
        {
            int multiplier = level / levelConfig.levelMultiplierLineOneInterval;
            int baseSegmentCount = (int)baseLevel.phaseOneLength / Settings.roadSegmentLength;
            return baseSegmentCount + multiplier;
        }
        return (int)baseLevel.phaseOneLength / Settings.roadSegmentLength;
    }

    private int CalculatePhaseTwoRoadSegmentsCountForLevel(int level)
    {
        if (level % levelConfig.levelMultiplierLineTwoInterval == 0 && level >= 1)
        {
            int multiplier = level / levelConfig.levelMultiplierLineTwoInterval;
            int baseSegmentCount = (int)baseLevel.phaseTwoLength / Settings.roadSegmentLength;
            return baseSegmentCount + multiplier;
        }
        return (int)baseLevel.phaseTwoLength / Settings.roadSegmentLength;
    }


}
