using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddCO2EventUtil : MonoBehaviour
{
    [SerializeField]
    private Button addCO2Buttom;

    [SerializeField]
    private float co2Amount = 10;

    public static event Action OnAddCO2;

    private void Awake()
    {
        addCO2Buttom = GetComponent<Button>();
    }

    private void OnEnable()
    {
        addCO2Buttom.onClick.AddListener(AddCO2);
    }

    private void OnDisable()
    {
        addCO2Buttom.onClick.RemoveAllListeners();
    }

    private void AddCO2()
    {
        OnAddCO2?.Invoke();
        CO2Manager.Instance.currentCo2 += co2Amount;
    }
}
