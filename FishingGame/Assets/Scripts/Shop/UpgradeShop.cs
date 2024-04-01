using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeShop : Shop
{
    public override void PopulateStorePage()
    {
        foreach(ValueEntry item in items)
        {
            Rod rod = (Rod)item.item;
            GameObject storeObject = Instantiate(UIPrefab, this.transform);
            storeObject.GetComponent<RodUpgrade>().Populate(rod, item.value);
        }
    }
}
