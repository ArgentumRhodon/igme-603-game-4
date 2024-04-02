using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BoostItem : MonoBehaviour
{
    public static Boost currentBoost = null;

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
        if(PlayerCurrency.playerGems < cost || currentBoost != null)
        {
            return;
        }

        currentBoost = boost;
        PlayerCurrency.UpdateGem(-cost);
        BuffPlayer();
    }


    public void BuffPlayer()
    {
        BoostTimer.Instance.StartTimer(boost);

        FishingStats stats = FindFirstObjectByType<FishingStats>();
        switch (boost.type)
        {
            case BoostType.Frenzy:
                stats.frenzyBoost = boost.boostSize;
                Invoke(nameof(ResetBoost), boost.duration * 60);
                break;
            case BoostType.Fishing:
                stats.fishingBoost = boost.boostSize;
                Invoke(nameof(ResetBoost), boost.duration * 60);
                break;
        }
    }

    public void ResetBoost()
    {
        BoostTimer.Instance.StopTimer();

        currentBoost = null;
        FishingStats stats = FindFirstObjectByType<FishingStats>();
        switch (boost.type)
        {
            case BoostType.Frenzy:
                stats.frenzyBoost = 0;
                break;
            case BoostType.Fishing:
                stats.fishingBoost = 0;
                break;
        }
    }


}
