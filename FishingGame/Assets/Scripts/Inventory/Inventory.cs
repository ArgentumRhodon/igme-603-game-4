using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Created by Rin
public class Inventory : MonoBehaviour
{
    public GameObject fishInstance;
    public GameObject fishContentList;

    public void AddFishLoot(Sprite fishSprite,int fishPrice, string fishName)
    {
        //Generate fish in the inventory
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
        rectTransform.localScale = Vector3.one;
    }
}
