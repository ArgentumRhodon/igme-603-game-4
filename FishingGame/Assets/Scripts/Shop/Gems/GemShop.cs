using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemShop : Shop
{
    public override void PopulateStorePage()
    {
        foreach (ValueEntry item in items) 
        {
            GemDeal gemDeal = (GemDeal)item.item;
            GameObject storeObject = Instantiate(UIPrefab, this.transform);
            storeObject.GetComponent<GemPurchase>().Populate(gemDeal);
        }
    }
}
