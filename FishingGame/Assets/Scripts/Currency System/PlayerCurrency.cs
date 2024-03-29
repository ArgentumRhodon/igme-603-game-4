using System;
using UnityEngine;

//Created by Rin
public static class PlayerCurrency
{
    // For normal currency
    private static int _playerCash = 0;
    public static int PlayerCash
    {
        get { return _playerCash; }
        set 
        {
            _playerCash = value;
            OnCashChanged?.Invoke(_playerCash);
        }
    }

    // For in-game purchases
    private static int _playerJewels = 0;
    public static int PlayerJewels
    {
        get { return _playerJewels; }
        set 
        {
            _playerJewels = value;
            OnJewelsChanged?.Invoke(_playerJewels);
        }
    }

    // Event for cash change -- update UI on changes instead of every frame for efficiency
    public static event Action<int> OnCashChanged;

    // Event for jewels change
    public static event Action<int> OnJewelsChanged;
}