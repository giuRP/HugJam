using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class EnergyProduct : Product
{

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

    public override void SetAffectedObjects()
    {
        Invoke(nameof(Effects), 0.1f);
    }
    public override void Effects()
    {
        //CO2
        //float carbonDecease = currentProductStats.carbonDecreaseRateBonus ;
        //CO2Manager.Instance.SetDrecreaseRate(carbonDecease);

        //Machines CO2 modifier  
        Product[] products;
        products = FindObjectsOfType<Product>();
            //ProductsManager.instance.productsOwned.FindAll(n => n.productType == ProductType.MACHINE);
        foreach (Product machine in products)
        {
            if (machine.productType == ProductType.MACHINE)
            {
                machine.SetExtraCarbon(currentProductStats.carbonIncreaseBonus);
                //Co2 reduction feedback
               // ObjectPooler.Instance.SpawnFromPoolAndAddParent("", new Vector2(0,1),Quaternion.identity, machine.transform);
            }
        }

        
    }

    private void ShowSpecialEffects()
    {

    }

    public override void UndoEffects()
    {
        Debug.Log("Undo an effect");

    }
}
