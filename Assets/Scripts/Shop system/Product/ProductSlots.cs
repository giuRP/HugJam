using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ProductSlot
{
    public bool isOccupied;
    public Transform slotPos;
}
public class ProductSlots : MonoBehaviour
{
    public ProductSlot[] productSlots;

    public void AddNewProductSlot(Product newProduct)
    {
        for (int i = 0; i < productSlots.Length; i++)
        {
            if (!productSlots[i].isOccupied)
            {
                ProductSlot selectedSlot = productSlots[i];
                newProduct.transform.position = selectedSlot.slotPos.position;
                productSlots[i].isOccupied = true;
                break;
            }
        }
    }
}
