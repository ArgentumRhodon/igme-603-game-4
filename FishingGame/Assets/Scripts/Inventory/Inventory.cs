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

    TextMeshProUGUI checkoutText;

    //Store all fish amount
    int[] fishAmountList = new int[13];
    //Store all fish price
    int[] fishPriceList = new int[13];

    public void Start()
    {
        checkoutText.text = "CHECK OUT \n _____ \n";
    }

    public void UpdateCheckoutInfo()
    {
        checkoutText = GameObject.Find("CheckoutInfo").GetComponent<TextMeshProUGUI>();

        if(checkoutText != null)
        {
            checkoutText.text = "CHECK OUT \n _____ \n";
            for (int i = 0; i < collectedFishList.Count; i++)
            {
                if (fishAmountList[i] > 0)
                {
                    checkoutText.text += collectedFishList[i].transform.Find("FishName").GetComponent<TextMeshProUGUI>().text + "\n" + fishPriceList[i]*fishAmountList[i] + "\n" + "----" + "\n";
                }
            }

            checkoutText.text += "TOTAL \n _____ \n";
            checkoutText.text += CalculateTotalPrice();

        }
        else
        {
            Debug.Log("null");
        }


    }

    public int CalculateTotalPrice()
    {
        int total = 0;
        for (int i = 0; i < collectedFishList.Count; i++)
        {
            if (fishAmountList[i] > 0)
            {
                total += fishAmountList[i] * fishPriceList[i];
            }

        }

        return total;
    }

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

    }


    public void SellFish(int fishId)
    {
        //Reset Amount 
        GameObject currentFishObject = collectedFishList[fishId - 1];
        
        currentFishObject.transform.Find("Amount").GetComponent<TextMeshProUGUI>().text = "Amount: 0";

        currentFishObject.transform.Find("SellButton").GetComponent<Button>().interactable = false;

        //Player add money
        PlayerCurrency.UpdateCash(fishAmountList[fishId - 1] * fishPriceList[fishId - 1]);
        
        fishAmountList[fishId - 1] = 0;
        UpdateCheckoutInfo();
    }

    public void SellAllFish()
    {
        int total = 0;
        for (int i = 0; i < collectedFishList.Count; i++)
        {
            if (fishAmountList[i] > 0)
            {
                total += fishAmountList[i] * fishPriceList[i];
                fishAmountList[i] = 0;
            }

            collectedFishList[i].transform.Find("Amount").GetComponent<TextMeshProUGUI>().text = "Amount: 0";
            collectedFishList[i].transform.Find("SellButton").GetComponent<Button>().interactable = false;

        }
        PlayerCurrency.UpdateCash(total);
        checkoutText.text = "CHECK OUT \n _____ \n";
    }



}
