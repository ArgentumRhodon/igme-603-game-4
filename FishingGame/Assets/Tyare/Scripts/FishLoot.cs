using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FishLoot : ScriptableObject
{
    public Sprite fishLootSprite;
    public string fishName;
    public int dropChance;

    public FishLoot(string fishName, int dropChance)
    {
        this.fishName = fishName;
        this.dropChance = dropChance;
    }
}
