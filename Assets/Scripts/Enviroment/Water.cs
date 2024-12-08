using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] Transform player;

    private void Update()
    {
        transform.position = new Vector3(player.position.x, -3, player.position.z);
    }

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckNullValue(this, nameof(player), player);
    }
#endif
    #endregion
}
