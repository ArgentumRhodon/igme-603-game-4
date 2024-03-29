using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Rod")]
public class Rod : ScriptableObject
{
    public string title;
    public float castingLength;
    public float ease;
    public float frenzyChance;
    public bool playerOwns = false;
}
