using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryUpgrades : Product
{
    [SerializeField]
    private PlantTileStateMachine[] plantTileStateMachine;
    public override void OnClicked()
    {
        
    }

    public override bool TryUpdateLevel()
    {
        if (base.TryUpdateLevel())
        {
            Effects();
            return true;
        }
        return false;
    }

    public override void Effects()
    {
        foreach (PlantTileStateMachine plant in plantTileStateMachine)
        {
            plant.AddExtraBerries(currentProductStats.harvestBonus);
        }
    }

    public override void SetAffectedObjects()
    {
        plantTileStateMachine = FindObjectsOfType<PlantTileStateMachine>();
        Effects();
    }

    public override void UndoEffects()
    {

    }

}



