using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField tmpInputField;


    private void Awake()
    {
        tmpInputField.onEndEdit.AddListener(OnEndEdit);
    }

    private void OnEndEdit(string inputText)
    {
        if (int.TryParse(inputText, out int result) && result > 0)
        {
            Settings.currentLevel = result;
            Debug.Log("Level set to " + result);
            PlayerPrefs.SetInt("Level", result);
            PlayerPrefs.Save();
        }
        else
        {
            Debug.LogWarning("Invalid input! Please enter a positive integer.");
        }

        tmpInputField.text = "";
        tmpInputField.gameObject.SetActive(false);
    }

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckNullValue(this, nameof(tmpInputField), tmpInputField);
    }
#endif
    #endregion
}