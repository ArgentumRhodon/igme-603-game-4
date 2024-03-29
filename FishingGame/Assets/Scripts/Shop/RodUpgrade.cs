using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RodUpgrade : MonoBehaviour
{
    [SerializeField]
    private Image image;
    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private TextMeshProUGUI text1;
    [SerializeField]
    private TextMeshProUGUI text2;
    [SerializeField]
    private TextMeshProUGUI priceText;

    private int cost;

    public void Populate(Rod rod, int cost)
    {
        image.sprite = rod.image;
        nameText.text = rod.name;
        text1.text = $"+ {rod.percentFishLuck}% Fish Luck";
        text2.text = $"+ {rod.percentFrenzyBoost}% Frenzy Chance";
        priceText.text = cost.ToString();

        this.cost = cost;
    }

    public void Purchase()
    {
        if(PlayerCurrency.PlayerCash < cost)
        {
            return;
        }

        PlayerCurrency.PlayerCash -= cost;
    }
}
