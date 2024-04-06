using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
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
    public Rod rod;
    public GameObject buyUI;
    public GameObject purchasedUI;

    public static List<Rod> purchasedRods;

    private void Start()
    {
        purchasedRods = new List<Rod>();
    }

    public void Populate(Rod rod, int cost, GameObject buyUI = null, GameObject purchasedUI = null)
    {
        image.sprite = rod.image;
        nameText.text = rod.title;
        text1.text = $"+ {rod.percentFishLuck}% Fish Luck";
        text2.text = $"+ {rod.percentFrenzyBoost}% Frenzy Chance";
        priceText.text = cost.ToString();

        this.cost = cost;
        this.rod = rod;
        this.buyUI = buyUI;
        this.purchasedUI = purchasedUI;
    }

    public void Purchase()
    {
        if (PlayerCurrency.playerCash < cost || purchasedRods.Contains(rod))
        {
            return;
        }

        purchasedRods.Add(rod);

        GameObject storeObject = Instantiate(purchasedUI, buyUI.gameObject.transform.parent);
        storeObject.GetComponent<RodUpgrade>().Populate(rod, cost);
        Destroy(buyUI);

        PlayerCurrency.UpdateCash(-cost);
        GameObject.Find("Rod").GetComponent<SpriteRenderer>().sprite = rod.image;
        FindFirstObjectByType<FishingStats>().currentRod = rod;
    }
}
