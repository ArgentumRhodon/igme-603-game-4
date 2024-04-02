using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/GemDeal")]
public class GemDeal : ScriptableObject
{
    public static float unitCost = 1f / 500;
    public int numGems;
}
