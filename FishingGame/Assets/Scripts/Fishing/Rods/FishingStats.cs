using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingStats : MonoBehaviour
{
    public Rod currentRod;
    public int fishingBoost = 0;
    public int frenzyBoost = 0;

    public float PercentFrenzyBoost { get { return currentRod.percentFrenzyBoost + frenzyBoost; } }
    public float FishingSkill { get { return currentRod.percentFishLuck + fishingBoost; } }
}
