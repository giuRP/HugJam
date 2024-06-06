using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductsManager : MonoBehaviour
{
    public static ProductsManager instance;

    public ProductSlots machinesPeopleSlots;
    public ProductSlots energySlots;
    public ProductSlots vegetationSlots;

    public List<Product> productsOwned;


    private void Awake()
    {
        instance = this;
    }

    public void AddProduct(Product newProductInstance, ProductUI productUI)
    {
        Product _newProductInstance = newProductInstance;

        productsOwned.Add(newProductInstance);

        switch (newProductInstance.productType)
        {
            case (ProductType.ENERGY):
                _newProductInstance = Instantiate(_newProductInstance);
                energySlots.AddNewProductSlot(_newProductInstance);
                break;
            case (ProductType.MACHINE):
                _newProductInstance = Instantiate(_newProductInstance);
                machinesPeopleSlots.AddNewProductSlot(_newProductInstance);
                break;
            case (ProductType.PERSON):
                _newProductInstance = Instantiate(_newProductInstance);
                machinesPeopleSlots.AddNewProductSlot(_newProductInstance);
                break;
            case (ProductType.NATURAL_VEGETATION):
                _newProductInstance = Instantiate(_newProductInstance);
                vegetationSlots.AddNewProductSlot(_newProductInstance);
                break;
        }
        productUI.SetProductReference(_newProductInstance);
        _newProductInstance.Initialize();
    }

}
