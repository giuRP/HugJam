using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProductType
{
    ENERGY,
    PERSON,
    MACHINE,
    NATURAL_VEGETATION,
    
}


public class ShopManager : MonoBehaviour
{
    public List<Product> products;

    public static ShopManager instance;

    public ProductUI productUiPrefab;

    public Transform productsPanel;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SetProducts();
    }

    void SetProducts()
    {
        for (int i = 0; i < products.Count; i++)
        {
            CreateProductButton(products[i]);
        }
    }

    void CreateProductButton(Product _product)
    {
        ProductUI productUI = Instantiate(productUiPrefab, Vector3.zero, Quaternion.identity, productsPanel);
        productUI.productPrefab = _product;
        //Debug.Log("Product" + productUI);

    }

    public void TryBuyItem(ProductUI _productUI)//Informs product type
    {
        Product newProduct = _productUI.productPrefab;

        int cost = newProduct.GetProductUpgradeStats().cost;
        int money = MoneyManager.instance.money;
        int result = 0;
        result = money - cost;
        if (newProduct.canSpawnFirst)
        {
            result = 1;
        }

        Debug.Log($"cost: {cost}");
        Debug.Log($"Money preview: {result}");
        //Check money value
        if (result < 0)
        {
            Debug.Log("Can't buy item");
        }
        else
        {
            _productUI.hasBought = true;
            if(!newProduct.canSpawnFirst)
                MoneyManager.instance.SpendMoney(cost);
            ProductsManager.instance.AddProduct(newProduct, _productUI);
        }
    }

    public void SellProduct()//Informs product type
    {
        //Add money value
        Debug.Log("Sold");
    }
}
