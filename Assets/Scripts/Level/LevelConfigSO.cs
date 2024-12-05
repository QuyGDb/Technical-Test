using UnityEngine;


[CreateAssetMenu(fileName = "LevelConfig", menuName = "Scriptable Object/LevelConfig", order = 0)]
public class LevelConfigSO : ScriptableObject
{
    [Header("Tsunami Velocity Settings")]
    [Tooltip("Multiplier for tsunami velocity in phase one (line one) based on the level.")]
    public int tsunamiVelocityLineOneMultiplier = 2;

    [Tooltip("Multiplier for tsunami velocity in phase two (line two) based on the level.")]
    public int tsunamiVelocityLineTwoMultiplier = 2;

    [Header("Level Multiplier Settings")]
    [Tooltip("Interval of levels for applying the multiplier in phase one (line one).")]
    public int levelMultiplierLineOneInterval = 2;

    [Tooltip("Interval of levels for applying the multiplier in phase two (line two).")]
    public int levelMultiplierLineTwoInterval = 2;

    [Header("Obstacle and Animal Settings")]
    [Tooltip("Multiplier for the number of obstacles added per level.")]
    public int obstacleLevelMultiplier = 2;

    [Tooltip("Multiplier for the number of animals added per level.")]
    public int animalLevelMultiplier = 1;
}

