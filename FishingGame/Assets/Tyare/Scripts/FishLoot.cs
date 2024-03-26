using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FishLoot : ScriptableObject
{
    public Sprite fishLootSprite;
    public string fishName;
    public int dropChance;
    public int sellPrice;

    public FishLoot(string fishName, int dropChance, int sellPrice)
    {
        this.fishName = fishName;
        this.dropChance = dropChance;
        this.sellPrice = sellPrice;
    }
}
