using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI result;
    [SerializeField] private TextMeshProUGUI pickedUpAnimalCount;
    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private Button nextButton;
    [SerializeField] private Slider playerBar;
    [SerializeField] private Slider tsunamiBar;
    private void OnEnable()
    {
        GameManager.Instance.OnGameStateChange += OnGameStateChange_InGameUI;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStateChange -= OnGameStateChange_InGameUI;
    }

    private void OnGameStateChange_InGameUI(GameState gameState)
    {
        if (gameState == GameState.Lost || gameState == GameState.Win)
        {
            result.gameObject.transform.parent.gameObject.SetActive(true);
            result.gameObject.transform.parent.transform.DOScale(new Vector3(0.5f, 0.5f, 1), 1f).SetEase(Ease.OutBounce);
            AnnounceGameResult(gameState);
        }
    }
    private void Start()
    {
        nextButton.onClick.RemoveAllListeners();
        nextButton.onClick.AddListener(() =>
        {
            Debug.Log("Next Level");
        });
    }
    private void Update()
    {
        ProcessTrackerPositionBar();
    }
    private void AnnounceGameResult(GameState gameState)
    {

        if (gameState == GameState.Lost)
        {
            result.text = "You Lost!";
        }
        if (gameState == GameState.Win)
        {
            result.text = "You Win!";
        }

        pickedUpAnimalCount.text = "Picked Up Animals: " + GameManager.Instance.pickedUpAnimalCount + "/" + GameManager.Instance.currentLevel.animalsQuantity;
        timer.text = "Time: " + GameManager.Instance.timer.ToString("F2");

    }

    private void ProcessTrackerPositionBar()
    {
        playerBar.value = GameManager.Instance.playerTransform.position.z / (GameManager.Instance.currentLevel.phaseOneLength + GameManager.Instance.currentLevel.phaseTwoLength);
        if (GameManager.Instance.tsunamiTransform.position.z > 0)
            tsunamiBar.value = GameManager.Instance.tsunamiTransform.position.z / (GameManager.Instance.currentLevel.phaseOneLength + GameManager.Instance.currentLevel.phaseTwoLength);
    }
}
