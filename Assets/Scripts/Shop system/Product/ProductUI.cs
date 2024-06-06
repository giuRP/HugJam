using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ProductUI : MonoBehaviour
{
    [SerializeField] private Button uiButton;

    [SerializeField] private Image productIcon;
    public TextMeshProUGUI costText;
    public string uiDescription;
    
    //Prefab do produtos
    public Product productPrefab;
    
    //Instancia do produto na cena
    public Product productInstanceRef;
    public bool hasBought;

    public TextMeshProUGUI levelMaxText;

    private void Start()
    {
        uiDescription = productPrefab.description;
        hasBought = false;
        uiButton.onClick.AddListener(OnItemBuy);
        productIcon.sprite = productPrefab.GetProductUpgradeStats().shopIcon;
        costText.text = "" + productPrefab.GetProductUpgradeStats().cost;

        if (productPrefab.destroyOnSpawn)
        {
            OnItemBuy();
            Destroy(this.gameObject);
        }
        else if (productPrefab.canSpawnFirst)
            OnItemBuy();
            
    }

    public void SetProductReference(Product product)
    {
        productInstanceRef = product;
    }

    [ContextMenu("Buy product")]
    public void OnItemBuy()
    {

        if (!hasBought)
        {
            ShopManager.instance.TryBuyItem(this);

        }
        else
        {
           if( !productInstanceRef.TryUpdateLevel())
            {

            }
        }
        if (productInstanceRef != null && productInstanceRef.GetNextUpgradeStats() != null)
        {
            productIcon.sprite = productInstanceRef.GetNextUpgradeStats().shopIcon;
            costText.text = "" + productInstanceRef.GetNextUpgradeStats().cost;
        }
        else
        {
            return;
        }
       
        if (productInstanceRef.upgradeLevelIndex == productInstanceRef.upgradeLevels.Length - 1)
            levelMaxText.gameObject.SetActive(true);

    }

    [ContextMenu("Sell product")]
    public void OnItemSell()
    {
        ShopManager.instance.SellProduct();
    }


}
