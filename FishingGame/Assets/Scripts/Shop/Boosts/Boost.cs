using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BoostType
{
    Frenzy,
    Fishing
}

[CreateAssetMenu(menuName = "ScriptableObjects/Boost")]
public class Boost : ScriptableObject
{
    public int duration;
    public int boostSize;
    public BoostType type;
}
