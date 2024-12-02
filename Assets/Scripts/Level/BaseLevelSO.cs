using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Scriptable Object/Level")]
public class BaseLevelSO : ScriptableObject
{
    public float phaseOneLength;
    public float phaseTwoLength;
    public int obstaclesQuantity;
    public int animalsQuantity;
    public float tsunamiVelocityPhaseOne;
    public float tsunamiVelocityPhaseTwo;
}
