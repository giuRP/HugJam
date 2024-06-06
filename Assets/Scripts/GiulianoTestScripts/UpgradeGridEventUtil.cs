using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeGridEventUtil : MonoBehaviour
{
    [SerializeField]
    private Button addGridButtom;

    public static event Action OnUpgradeGrid;

    private void Awake()
    {
        addGridButtom = GetComponent<Button>();
    }

    private void OnEnable()
    {
        addGridButtom.onClick.AddListener(UpgradeGrid);
    }

    private void OnDisable()
    {
        addGridButtom.onClick.RemoveAllListeners();
    }

    private void UpgradeGrid()
    {
        OnUpgradeGrid?.Invoke();
        addGridButtom.gameObject.SetActive(false);
    }
}
