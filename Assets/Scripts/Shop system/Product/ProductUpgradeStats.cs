using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Product stats", menuName = "Product stats")]
public class ProductUpgradeStats : ScriptableObject
{
    public Sprite shopIcon;

    public int cost;

    [Header("Animations")]
    public RuntimeAnimatorController animController;

    [Header("Plants")]
    public float growhtTimeBonus;
    public int harvestBonus;

    [Header("Terrain")]
    public int fertilBonus;

    [Header("Carbon")]
    public int carbonMinValue;
    public float carbonDecreaseRateBonus;
    public float carbonIncreaseBonus;

    [Header("Energy")]
    public int energyCostUse;
    public int energyProduction;
}
