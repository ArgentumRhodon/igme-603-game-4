using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    // In case we want to add more than rods to the game
    public List<ValueTable> shops;

    private void Awake()
    {
        shops = new List<ValueTable>();
    }
}
