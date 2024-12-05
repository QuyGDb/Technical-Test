using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Atribute : MonoBehaviour
{
    private float stamina;
    private float speed;
    private float income;
    private float money;
    private float staminaPrice;
    private float speedPrice;
    private float incomePrice;
    [SerializeField] Button staminaButton;
    [SerializeField] Button speedButton;
    [SerializeField] Button incomeButton;
    [SerializeField] private TextMeshProUGUI staminaQuantity;
    [SerializeField] private TextMeshProUGUI speedQuantity;
    [SerializeField] private TextMeshProUGUI incomeQuantity;
    [SerializeField] private TextMeshProUGUI staminaPriceTxt;
    [SerializeField] private TextMeshProUGUI speedPriceTxt;
    [SerializeField] private TextMeshProUGUI incomePriceTxt;
    [SerializeField] private TextMeshProUGUI moneyQuantity;

    private void Awake()
    {
        LoadAtributeFromDisk();
    }

    void LoadAtributeFromDisk()
    {
        if (PlayerPrefs.HasKey("Stamina"))
            stamina = PlayerPrefs.GetFloat("Stamina");
        else
            stamina = 1;
        if (PlayerPrefs.HasKey("Speed"))
            speed = PlayerPrefs.GetFloat("Speed");
        else
            speed = 1;
        if (PlayerPrefs.HasKey("Income"))
            income = PlayerPrefs.GetFloat("Income");
        else
            income = 1;
        if (PlayerPrefs.HasKey("Money"))
            money = PlayerPrefs.GetFloat("Money");
        else
            money = 0;
        if (PlayerPrefs.HasKey("StaminaPrice"))
            staminaPrice = PlayerPrefs.GetFloat("StaminaPrice");
        else
            staminaPrice = 10;
        if (PlayerPrefs.HasKey("SpeedPrice"))
            speedPrice = PlayerPrefs.GetFloat("SpeedPrice");
        else
            speedPrice = 10;
        if (PlayerPrefs.HasKey("IncomePrice"))
            incomePrice = PlayerPrefs.GetFloat("IncomePrice");
        else
            incomePrice = 10;

    }
    void SetStartValueForTextUI()
    {
        staminaQuantity.text = stamina.ToString();
        speedQuantity.text = speed.ToString();
        incomeQuantity.text = income.ToString();
        staminaPriceTxt.text = staminaPrice.ToString();
        speedPriceTxt.text = speedPrice.ToString();
        incomePriceTxt.text = incomePrice.ToString();
        moneyQuantity.text = money.ToString();
    }
    private void Start()
    {
        SetStartValueForTextUI();
        staminaButton.onClick.AddListener(StaminaUpgrade);
        speedButton.onClick.AddListener(SpeedUpgrade);
        incomeButton.onClick.AddListener(IncomeUpgrade);
    }
    private void Update()
    {
        IncreaMoney();
    }
    private void StaminaUpgrade()
    {
        stamina += stamina * 0.1f;
        staminaPrice += staminaPrice * 0.3f;
        staminaPriceTxt.text = staminaPrice.ToString("F2");
        staminaQuantity.text = stamina.ToString("F2");
        PlayerPrefs.SetFloat("Stamina", stamina);
        PlayerPrefs.SetFloat("StaminaPrice", staminaPrice);
        PlayerPrefs.Save();
    }

    private void SpeedUpgrade()
    {
        speed += 0.5f;
        speedPrice += speedPrice * 0.3f;
        speedPriceTxt.text = speedPrice.ToString("F2");
        speedQuantity.text = speed.ToString("F2");
        PlayerPrefs.SetFloat("Speed", speed);
        PlayerPrefs.SetFloat("SpeedPrice", speedPrice);
        PlayerPrefs.Save();
    }

    private void IncomeUpgrade()
    {
        income += 1;
        incomePrice += incomePrice * 0.3f;
        incomePriceTxt.text = incomePrice.ToString("F2");
        incomeQuantity.text = income.ToString("F2");
        PlayerPrefs.SetFloat("Income", income);
        PlayerPrefs.SetFloat("IncomePrice", incomePrice);
        PlayerPrefs.Save();

    }

    private void IncreaMoney()
    {
        if (Input.GetMouseButtonDown(0))
        {
            money += income;
            moneyQuantity.text = money.ToString();
            PlayerPrefs.SetFloat("Money", money);
            PlayerPrefs.Save();
        }

        if (Input.touchCount > 0)
        {
            money += income;
            moneyQuantity.text = money.ToString();
            PlayerPrefs.SetFloat("Money", money);
            PlayerPrefs.Save();
        }
    }


}
