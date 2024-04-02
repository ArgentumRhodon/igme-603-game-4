using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class GemPurchase : MonoBehaviour
{ 

    [SerializeField]
    private TextMeshProUGUI numberText;

    [SerializeField]
    private TextMeshProUGUI priceText;

    private int numGems;

    public void Populate(GemDeal gemDeal)
    {
        numberText.text = gemDeal.numGems.ToString() + " Gems";
        float cost = gemDeal.numGems * GemDeal.unitCost - (gemDeal.numGems / 1000);
        priceText.text = cost.ToString("C", CultureInfo.CurrentCulture);

        numGems = gemDeal.numGems;
    }

    public void Purchase()
    {
        PlayerCurrency.UpdateGem(numGems);
    }
}
