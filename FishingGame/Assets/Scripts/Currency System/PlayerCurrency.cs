using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


//Created by Rin
public static class PlayerCurrency
{
    //For normal currency
    public static int playerCash = 0;

    //For in-game purchase
    public static int playerGems = 0;

    public static GameObject CashText;
    public static GameObject GemText;

    public static void UpdateCash(int amount)
    {
        playerCash += amount;
        GameObject.Find("CashText").GetComponent<TextMeshProUGUI>().text = playerCash.ToString();
        Debug.Log(playerCash);
    }

    public static void UpdateGem(int amount)
    {
        playerGems += amount;
        GameObject.Find("GemText").GetComponent<TextMeshProUGUI>().text = playerCash.ToString();
        Debug.Log(playerGems);
    }
}
