using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishLootBag : MonoBehaviour
{
    public GameObject droppedItemPrefab;
    public List<FishLoot> fishLootList = new List<FishLoot>();

    [SerializeField] private float dropForce = 300f;

    FishLoot GetDroppedItem()
    {
        int randNum = Random.Range(1, 101);
        List<FishLoot> possibleItems = new List<FishLoot>();
        foreach (FishLoot item in fishLootList)
        {
            if (randNum <= item.dropChance)
            {
                possibleItems.Add(item);
            }
        }
        if (possibleItems.Count > 0)
        {
            FishLoot droppedItem = possibleItems[Random.Range(0, possibleItems.Count)];
            return droppedItem;
        }
        Debug.Log("No Fish Caught");
        return null;
    }

    public void InstantiateLoot(Vector3 spawnPos)
    {
        FishLoot droppedItem = GetDroppedItem();
        if(droppedItem != null)
        {
            GameObject fishLootGameObject = Instantiate(droppedItemPrefab, spawnPos, Quaternion.identity);
            fishLootGameObject.GetComponent<SpriteRenderer>().sprite = droppedItem.fishLootSprite;

            Vector3 dropDirection = new Vector3(Random.Range(-.5f, .6f), 1f, 0f);
            fishLootGameObject.GetComponent<Rigidbody2D>().AddForce(dropDirection * dropForce, ForceMode2D.Impulse);

            Destroy(fishLootGameObject, 2);
        }
    }
}
