using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinishUISpawner : MonoBehaviour
{
    [SerializeField] private GameObject finishUI;

    public void SpawnFinishUI(LevelDetails levelDetails)
    {
        GameObject finishUIGO = Instantiate(finishUI, new Vector3(-6.5f, 12, levelDetails.phaseOneRoadSegmentCount * 100), Quaternion.identity, transform);
        finishUIGO.GetComponentInChildren<TextMeshProUGUI>().text = "Finish 1";
        finishUIGO = Instantiate(finishUI, new Vector3(-6.5f, 12, (levelDetails.phaseOneRoadSegmentCount + levelDetails.phaseTwoRoadSegmentCount) * 100), Quaternion.identity, transform);
        finishUIGO.GetComponentInChildren<TextMeshProUGUI>().text = "Finish 2";
    }
    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckNullValue(this, nameof(finishUI), finishUI);
    }
#endif
    #endregion
}
