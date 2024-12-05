using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Collections;
using UnityEngine;

public struct LevelDetails
{
    public int level;
    public int phaseOneRoadSegmentCount;
    public int phaseTwoRoadSegmentCount;
    public int phaseOneLength;
    public int phaseTwoLength;
    public int obstaclesQuantity;
    public int animalsQuantity;
    public float tsunamiVelocityPhaseOne;
    public float tsunamiVelocityPhaseTwo;
}
