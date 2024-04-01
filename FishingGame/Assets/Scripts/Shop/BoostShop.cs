using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostShop : Shop
{
    public override void PopulateStorePage()
    {
        foreach (ValueEntry item in items)
        {
            Boost boost = (Boost)item.item;
            GameObject storeObject = Instantiate(UIPrefab, this.transform);
            storeObject.GetComponent<BoostItem>().Populate(boost, item.value);
        }
    }
}
