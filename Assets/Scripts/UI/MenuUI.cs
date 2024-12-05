using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private GameObject levelPanel;
    [SerializeField] private Button playButton;
    [SerializeField] private Button closeLevelPanel;
    private void Awake()
    {
        levelPanel.SetActive(false);
        playButton.onClick.AddListener(Play);
        closeLevelPanel.onClick.AddListener(CloseLevelPanel);
        if (PlayerPrefs.HasKey("Level"))
            Settings.currentLevel = PlayerPrefs.GetInt("Level");
        else
            Settings.currentLevel = 0;
    }

    private void CloseLevelPanel()
    {
        levelPanel.SetActive(false);
        levelPanel.transform.DOScale(new Vector3(0, 0, 0), 1f).SetEase(Ease.OutBounce);
    }

    private void Play()
    {
        if (levelPanel.activeSelf)
        {
            levelPanel.SetActive(false);
            levelPanel.transform.DOScale(new Vector3(0, 0, 0), 1f).SetEase(Ease.OutBounce);
        }
        else
        {
            if (Settings.currentLevel == 0)
                SceneManager.LoadScene(1);
            else
            {
                levelPanel.SetActive(true);
                levelPanel.transform.DOScale(new Vector3(1, 1, 1), 1f).SetEase(Ease.OutBounce);
            }
        }
    }




}
