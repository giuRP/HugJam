using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Irrigador : Product
{
    [SerializeField]
    private Animator gfxContainerAnim;

    [SerializeField]
    private PlantTileGroundState[] plantTileGroundStates;
    public override void SetAffectedObjects()
    {
        Debug.Log("Find affected objects and check if they have the IChangeStats interface");

        plantTileGroundStates = FindObjectsOfType<PlantTileGroundState>();
    }
    public override void Effects()
    {
        if (isActivated)
        {
            Debug.Log("Is already ativated");

            return;
        }

        //Verifica o CO2
        if (CO2Manager.Instance.currentCo2 + GetCarbonIncrease() > CO2Manager.Instance.maxCo2)
        {
            Debug.Log("Carbono no limite");
            return;
        }

        OnUseProduct?.Invoke();


        //CO2
        CO2Manager.Instance.IncreaseCO2 (GetCarbonIncrease());

        Debug.Log("Irrigando");

        //Irriga todas as plantas
        foreach (PlantTileGroundState plant in plantTileGroundStates)
        {
            plant.SetGroundState(GroundState.WET);
        }

        TriggerProductActivation();
    }
    public override void UndoEffects()
    {
        Debug.Log("Undo an effect");

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
