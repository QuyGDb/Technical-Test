using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelPanelManager : MonoBehaviour
{
    [SerializeField] private GameObject levelBtnPrefab;
    private int levelCount = 12;
    private Button[] levelBtns = new Button[12];
    private TextMeshProUGUI[] levelBtnTexts = new TextMeshProUGUI[12];
    private LevelButton[] levelButtons = new LevelButton[12];
    [SerializeField] private Transform levelContainer;
    [SerializeField] private Button NextLevels;
    [SerializeField] private Button PreviousLevels;
    private int currentPanelIndex;
    private int levelPanelCount;
    private void Awake()
    {
        InitializeButton();
    }
    private void OnEnable()
    {
        if (PlayerPrefs.HasKey("Level"))
            Settings.currentLevel = PlayerPrefs.GetInt("Level");
        else
            Settings.currentLevel = 0;
        Debug.Log("Current Level: " + Settings.currentLevel);
        CalculatelevelPanelCount();
        CalculatePanelIndex();
        CalculateLevelQuantityInLevelButton(currentPanelIndex);
        ProcessLevelEvent(CalculatelevelPanelCount());
    }

    private void InitializeButton()
    {
        for (int i = 0; i < levelCount; i++)
        {
            GameObject levelBtn = Instantiate(levelBtnPrefab, levelContainer);
            levelBtns[i] = levelBtn.GetComponent<Button>();
            levelBtnTexts[i] = levelBtn.GetComponentInChildren<TextMeshProUGUI>();
            levelButtons[i] = levelBtn.GetComponent<LevelButton>();
        }
    }
    private void Start()
    {
        NextLevels.onClick.AddListener(NextLevelsOnClick);
        PreviousLevels.onClick.AddListener(PreviousLevelsOnClick);
    }
    private int CalculatelevelPanelCount()
    {
        float fractionalPart = (Settings.currentLevel / (float)levelCount) - Settings.currentLevel / levelCount;
        if (fractionalPart == 0)
            return 12;
        float currentLevelIndexFloat = fractionalPart * levelCount;
        return Mathf.RoundToInt(currentLevelIndexFloat);
    }
    private void CalculatePanelIndex()
    {
        levelPanelCount = Mathf.CeilToInt((float)Settings.currentLevel / levelCount);
        currentPanelIndex = levelPanelCount;
    }
    private void CalculateLevelQuantityInLevelButton(int currentPanelIndex)
    {
        for (int i = 0; i < levelCount; i++)
        {
            levelBtnTexts[i].text = (i + 1 + ((currentPanelIndex - 1) * levelCount)).ToString();
        }
    }

    private void ProcessLevelEvent(int currentLevelIndex)
    {
        Debug.Log("Current Level Index: " + currentLevelIndex);
        for (int i = 0; i < levelCount; i++)
        {
            if (i < currentLevelIndex)
            {
                levelBtns[i].interactable = true;
                levelButtons[i].lockImage.gameObject.SetActive(false);
            }
            else
            {
                levelBtns[i].interactable = false;
                levelButtons[i].lockImage.gameObject.SetActive(true);
            }
        }

    }
    private void NextLevelsOnClick()
    {

        if (currentPanelIndex < levelPanelCount)
        {
            currentPanelIndex++;
            levelContainer.transform.DOScale(new Vector3(0, 0, 0), 0.2f).SetEase(Ease.OutBounce).OnComplete(() =>
            {
                levelContainer.transform.DOScale(new Vector3(1, 1, 1), 0.2f).SetEase(Ease.OutBounce);
            });
            if (currentPanelIndex == levelPanelCount)
            {
                ProcessLevelEvent(CalculatelevelPanelCount());
            }
            else
                ProcessLevelEvent(currentPanelIndex * levelCount);
            CalculateLevelQuantityInLevelButton(currentPanelIndex);
        }
    }
    private void PreviousLevelsOnClick()
    {
        if (currentPanelIndex > 1)
        {
            currentPanelIndex--;
            levelContainer.transform.DOScale(new Vector3(0, 0, 0), 0.2f).SetEase(Ease.OutBounce).OnComplete(() =>
            {
                levelContainer.transform.DOScale(new Vector3(1, 1, 1), 0.2f).SetEase(Ease.OutBounce);
            });
            ProcessLevelEvent(currentPanelIndex * levelCount);
            CalculateLevelQuantityInLevelButton(currentPanelIndex);
        }
    }

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckNullValue(this, nameof(levelBtnPrefab), levelBtnPrefab);
        HelperUtilities.ValidateCheckNullValue(this, nameof(levelContainer), levelContainer);
        HelperUtilities.ValidateCheckNullValue(this, nameof(NextLevels), NextLevels);
        HelperUtilities.ValidateCheckNullValue(this, nameof(PreviousLevels), PreviousLevels);
    }
#endif
    #endregion
}
