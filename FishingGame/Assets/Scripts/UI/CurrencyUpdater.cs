using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class CurrencyUpdater : MonoBehaviour
{
    public TextMeshProUGUI cashText;
    public TextMeshProUGUI jewelsText;

    private void Start()
    {
        // Subscribe to cash change event
        PlayerCurrency.OnCashChanged += UpdateCashUI;
        // Subscribe to jewels change event
        PlayerCurrency.OnJewelsChanged += UpdateJewelsUI;
    }

    private void UpdateCashUI(int newCashValue)
    {
        cashText.text = newCashValue.ToString();
    }

    private void UpdateJewelsUI(int newJewelsValue)
    {
        jewelsText.text = newJewelsValue.ToString();
    }
}
