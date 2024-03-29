using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shop : MonoBehaviour
{
    private void Start()
    {
        PopulateStorePage();
    }

    public List<ValueEntry> items;
    public GameObject UIPrefab;
    public abstract void PopulateStorePage();
}
