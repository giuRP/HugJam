using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance;

    public int money;

    public delegate void MoneyChanged(int currentMoney);
    public MoneyChanged OnMoneyChanged;

    private void Awake()
    {
        instance = this;
    }

    public void SpendMoney(int payedMoney)
    {
        money -= payedMoney;
        Debug.Log("Spend money");
        OnMoneyChanged?.Invoke(money);
    }
    public void GainMoney(int extraMoney)
    {
        money += extraMoney;
        OnMoneyChanged?.Invoke(money);
    }

}
