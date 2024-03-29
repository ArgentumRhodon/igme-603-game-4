using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ValueEntry
{
    public ScriptableObject item;
    public int value;
}

[CreateAssetMenu(menuName = "ScriptableObjects/ValueTable")]
public class ValueTable : ScriptableObject
{
    public List<ValueEntry> entries;
}
