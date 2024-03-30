using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "ScriptableObjects/Rod")]
public class Rod : ScriptableObject
{
    public Sprite image;
    public string title;
    public float castingLength;
    public float percentFishLuck;
    public float percentFrenzyBoost;
}
