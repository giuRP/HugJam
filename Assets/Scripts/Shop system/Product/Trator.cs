using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Affects tiles and plants
/// </summary>
public class Trator : Product
{
    [SerializeField]
    private Animator gfxContainerAnim;

    [SerializeField]
    private PlantTileStateMachine[] plantTileStateMachine;

    public override void SetAffectedObjects()
    {
        Debug.Log("Get all the affected objects");

        //Then add to the changeStats
        plantTileStateMachine = FindObjectsOfType<PlantTileStateMachine>();
    }
    public override void Effects()
    {

        if (isActivated)
        {
            Debug.Log("Is already ativated");

            return;
        }

        //Verifica o CO2
        if(CO2Manager.Instance.currentCo2 + GetCarbonIncrease() > CO2Manager.Instance.maxCo2)
        {
            Debug.Log("Carbono no limite");
            return;
        }

        OnUseProduct?.Invoke();

        //Afeta o co2
        CO2Manager.Instance.IncreaseCO2( GetCarbonIncrease());

        //CO2 particles
        ObjectPooler.Instance.SpawnFromPoolAndAddParent("Smoke", new Vector3(0.36f, 1, 0), Quaternion.identity,this.transform, activateTimer);

        //Afeta as plantas, nesse caso todas
        float plantsGrowthTimeDecrease = currentProductStats.growhtTimeBonus * -1;
        foreach (PlantTileStateMachine plantTile in plantTileStateMachine)
        {
            plantTile.AddGrowthModifier(plantsGrowthTimeDecrease);
        }

       TriggerProductActivation();
    }

    public override void UndoEffects()
    {
        //Debug.Log("Undo an effect");
        foreach (PlantTileStateMachine plantTile in plantTileStateMachine)
        {
            plantTile.AddGrowthModifier(0);
        }

        //float carbonDecease = currentProductStats.carbonDecreaseRateBonus;
        //CO2Manager.Instance.SetDrecreaseRate(carbonDecease);
    }

    public override void OnClicked()
    {
        base.OnClicked();

        gfxContainerAnim.SetTrigger("Clicked");
    }

    public override void OnHover()
    {
        base.OnHover();

        gfxContainerAnim.SetTrigger("Hover");
    }

}
