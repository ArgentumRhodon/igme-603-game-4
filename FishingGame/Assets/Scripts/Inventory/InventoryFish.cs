using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryFish : MonoBehaviour
{

    public int fishPrice;

    public bool isSelected;


    public void ClickOnFishItem()
    {
        if(isSelected)
        {
            isSelected = false;
        }
        else
        {
            isSelected = true;
        }
    }

    //public void 


}
