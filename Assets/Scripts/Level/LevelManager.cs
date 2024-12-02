using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    [SerializeField] private BaseLevelSO baseLevel;
    


    private void GenarateLevel(int level)
    {


    }
    private LevelDetails CaluculateLevelDetails(int level)
    {
        LevelDetails levelDetails;
        levelDetails.phaseOneLength = CalculatePhaseOneLengthForLevel(level);
        levelDetails.phaseTwoLength = CalculatePhaseTwoLengthForLevel(level);
        levelDetails.obstaclesQuantity = CalculateObstacesForLevel(level);
        levelDetails.animalsQuantity = CalculateAnimalsForLevel(level);
        levelDetails.tsunamiVelocityPhaseOne = CalculatePhaseOneTsunamiVelocityForLevel(level);
        levelDetails.tsunamiVelocityPhaseTwo = CalculatePhaseTwoTsunamiVelocityForLevel(level);
        return levelDetails;
    }

    private int CalculateObstacesForLevel(int level)
    {
        if (level == 0)
        {
            return baseLevel.obstaclesQuantity;
        }
        if (level % 5 == 0 && level >= 1)
        {
            int multiplier = level / 5;
            return baseLevel.obstaclesQuantity + 2 * level + 10 * multiplier;
        }
        if (level >= 1)
        {
            return baseLevel.obstaclesQuantity + 2 * level;
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
            return baseLevel.animalsQuantity + 1 * level;
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
            return baseLevel.tsunamiVelocityPhaseOne + 2 * level;
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
            return baseLevel.tsunamiVelocityPhaseTwo + 2 * level;
        }
        return 0;
    }

    private float CalculatePhaseOneLengthForLevel(int level)
    {
        if (level % 5 == 0 && level >= 1)
        {
            int multiplier = level / 5;
            return baseLevel.phaseOneLength + 100 * multiplier;
        }
        return baseLevel.phaseOneLength;
    }

    private float CalculatePhaseTwoLengthForLevel(int level)
    {
        if (level % 5 == 0 && level >= 1)
        {
            int multiplier = level / 5;
            return baseLevel.phaseTwoLength + 100 * multiplier;
        }
        return baseLevel.phaseTwoLength;
    }


}
