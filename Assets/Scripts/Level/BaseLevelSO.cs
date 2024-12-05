using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Scriptable Object/Level")]
public class BaseLevelSO : ScriptableObject
{
    [Header("Phase Length Settings")]
    [Tooltip("The length of phase one (in units).")]
    public float phaseOneLength;

    [Tooltip("The length of phase two (in units).")]
    public float phaseTwoLength;

    [Header("Entity Settings")]
    [Tooltip("The number of obstacles in the level.")]
    public int obstaclesQuantity;

    [Tooltip("The number of animals in the level.")]
    public int animalsQuantity;

    [Header("Tsunami Settings")]
    [Tooltip("Tsunami velocity during phase one.")]
    public float tsunamiVelocityPhaseOne;

    [Tooltip("Tsunami velocity during phase two.")]
    public float tsunamiVelocityPhaseTwo;
}
