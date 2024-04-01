using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BoostItem : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private TextMeshProUGUI text1;
    [SerializeField]
    private TextMeshProUGUI text2;
    [SerializeField]
    private TextMeshProUGUI priceText;

    private Boost boost;
    private int cost;

    public void Populate(Boost boost, int cost)
    {
        nameText.text = boost.name;
        text1.text = $"{boost.duration} Minutes:";
        string secondLine = boost.type == BoostType.Frenzy ? "Frenzy Chance" : "Fish Luck";
        text2.text = $"+ {boost.boostSize}% {secondLine}";
        priceText.text = cost.ToString();
        
        this.boost = boost;
        this.cost = cost;
    }

    public void Purchase()
    {
        if(PlayerCurrency.playerGems < cost)
        {
            return;
        }

        PlayerCurrency.UpdateGem(-cost);
    }
}
