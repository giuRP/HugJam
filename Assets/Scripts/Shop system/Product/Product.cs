using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// Products can change other scripts's stats 
/// </summary>
[RequireComponent(typeof(ProductAnimation))]
public abstract class Product : MonoBehaviour, IClickable
{
    [Header("Scene info")]
    public bool canSpawnFirst;
    public bool destroyOnSpawn;
    [Space(10)]

    public UnityEvent OnUseProduct;

    public int upgradeLevelIndex;

    public string productName;
    public string description;

    public ProductType productType;

    public ProductUpgradeStats[] upgradeLevels;

    [SerializeField]
    protected ProductUpgradeStats currentProductStats;

    private ProductAnimation productAnimation;

    protected bool isActivated;

    public int cost;

    [Range(1,100)]
    public float activateTimer;

    protected Timer timer;

    [Header("Caarbon machines modifier")]
    public float extraCarbon;


    public virtual bool TryUpdateLevel()
    {
        if (upgradeLevelIndex == upgradeLevels.Length -1)
            return false;
        int nextLevelIndex = upgradeLevelIndex + 1;

        ProductUpgradeStats productStats = ScriptUtils.Clone<ProductUpgradeStats>(upgradeLevels[nextLevelIndex]);

        if (MoneyManager.instance.money - productStats.cost < 0)
            return false;

        Debug.Log("Update level");
        upgradeLevelIndex += 1;
        currentProductStats = upgradeLevels[upgradeLevelIndex];
        //Success bought 
        MoneyManager.instance.SpendMoney(productStats.cost);

        productAnimation.SetNewAnimator(productStats);

        return true;
    }

    public void Initialize()
    {
        productAnimation = GetComponent<ProductAnimation>();
        timer = GetComponent<Timer>();
        currentProductStats = ScriptUtils.Clone<ProductUpgradeStats>(upgradeLevels[upgradeLevelIndex]);
        cost = currentProductStats.cost;

        productAnimation.SetNewAnimator(currentProductStats);

        

        SetAffectedObjects();
    }
    public virtual void SetAffectedObjects() { }
    public virtual void Effects() { }
    public virtual void UndoEffects() { }

    public virtual void OnClicked()
    {
        Effects();
    }

    public virtual void OnHover()
    {
        switch (productType)
        {
            case ProductType.ENERGY:
                TooltipSystem.instance.ShowTooltip(productName, description);
                break;
            case ProductType.PERSON:
                break;
            case ProductType.MACHINE:
                TooltipSystem.instance.ShowTooltip(productName, description);
                break;
            case ProductType.NATURAL_VEGETATION:
                break;
        }
    }

    public  void OnUnhover()
    {
        switch (productType)
        {
            case ProductType.ENERGY:
                TooltipSystem.instance.HideTooltip();
                break;
            case ProductType.PERSON:
                break;
            case ProductType.MACHINE:
                TooltipSystem.instance.HideTooltip();
                break;
            case ProductType.NATURAL_VEGETATION:
                break;
        }


    }

    public ProductUpgradeStats GetNextUpgradeStats()
    {
        int nextIndex = upgradeLevelIndex + 1;


        ProductUpgradeStats nextUpgrade;
        
        if(nextIndex >= upgradeLevels.Length)
            return currentProductStats;
        else
        {
            nextUpgrade = upgradeLevels[upgradeLevelIndex + 1];
        }
        return nextUpgrade;
    }

    public ProductUpgradeStats GetProductUpgradeStats()
    {
        return currentProductStats;
    }
    
    public void SetExtraCarbon(float newExtraCarbonValue)
    {
        extraCarbon = newExtraCarbonValue;
        Debug.Log("New carbon value" + extraCarbon);
    }

    /// <summary>
    /// Take the base carbon plus the bonus modifier
    /// </summary>
    /// <returns></returns>
    public float GetCarbonIncrease()
    {
        return currentProductStats.carbonIncreaseBonus + extraCarbon;
    }

    protected virtual void TriggerProductActivation()
    {
        StartCoroutine(ProductActivate());
        timer.StartTimer(activateTimer, UndoEffects);
    }

    protected IEnumerator ProductActivate()
    {
        isActivated = true;
        yield return new WaitForSeconds(activateTimer);
        isActivated = false;
    }
}
