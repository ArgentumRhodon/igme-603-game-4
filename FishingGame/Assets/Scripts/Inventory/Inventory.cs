using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Created by Rin
public class Inventory : MonoBehaviour
{
    //public GameObject fishInstance;
    //public GameObject fishContentList;

    //Store Fish Item in the inventory
    public List<GameObject> collectedFishList;

    //Store all fish amount
    int[] fishAmountList = new int[8];
    //Store all fish price
    int[] fishPriceList = new int[8];

    public void AddFishLoot(Sprite fishSprite,int fishPrice, string fishName,int fishId)
    {
        //Set attributes in the inventory
        GameObject currentFishObject = collectedFishList[fishId - 1];
        currentFishObject.transform.Find("FishImage").GetComponent<Image>().sprite = fishSprite;
        currentFishObject.transform.Find("FishName").GetComponent<TextMeshProUGUI>().text = fishName;
        currentFishObject.transform.Find("SellPrice").GetComponent<TextMeshProUGUI>().text = "Sell Price: " + fishPrice;
        fishAmountList[fishId - 1] += 1;
        currentFishObject.transform.Find("Amount").GetComponent<TextMeshProUGUI>().text = "Amount: " + fishAmountList[fishId - 1];
        fishPriceList[fishId - 1] = fishPrice;

        currentFishObject.transform.Find("SellButton").GetComponent<Button>().interactable = true;



/*        //Generate fish in the inventory
        GameObject fishObject = Instantiate(fishInstance, transform.position, Quaternion.identity);
        fishObject.transform.SetParent(fishContentList.transform);
        //Set sprite and name
        fishObject.GetComponent<Image>().sprite = fishSprite;
        fishObject.GetComponentInChildren<TextMeshProUGUI>().text = fishName;
        //Set price
        fishObject.GetComponent<InventoryFish>().fishPrice = fishPrice;
        //Set RectTransform
        RectTransform rectTransform = fishObject.GetComponent<RectTransform>();
        rectTransform.localPosition = Vector3.zero;
        rectTransform.localScale = Vector3.one;*/
    }


    public void SellFish(int fishId)
    {
        //Reset Amount 
        GameObject currentFishObject = collectedFishList[fishId - 1];
        fishAmountList[fishId - 1] = 0;
        currentFishObject.transform.Find("Amount").GetComponent<TextMeshProUGUI>().text = "Amount: 0";

        currentFishObject.transform.Find("SellButton").GetComponent<Button>().interactable = false;

        //Player add money
        PlayerCurrency.playerCash += fishAmountList[fishId] * fishPriceList[fishId];
    }
}
