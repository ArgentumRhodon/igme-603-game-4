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

    public void Populate(Boost boost, int cost)
    {
        nameText.text = boost.name;
        text1.text = $"{boost.duration} Minutes:";
        string secondLine = boost.type == BoostType.Frenzy ? "Frenzy Chance" : "Fish Luck";
        text2.text = $"+ {boost.boostSize}% {secondLine}";
        priceText.text = cost.ToString();
    }

    public void Purchase()
    {
        Debug.Log("Purchased!");
    }
}
