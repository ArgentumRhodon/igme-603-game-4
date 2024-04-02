using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.GraphicsBuffer;

public class FishLootBag : MonoBehaviour
{
    public GameObject droppedItemPrefab;
    public List<FishLoot> fishLootList = new List<FishLoot>();

    [SerializeField] FishingStats fishingStats;

    [SerializeField] private float dropForce = 300f;

    //Fish Inventory --Rin
    Inventory playerInventory;

    private void Awake()
    {
        playerInventory = FindObjectOfType<Inventory>();
        if(playerInventory == null)
        {
            Debug.Log("NO");
        }
    }

    FishLoot GetDroppedItem()
    {
        int randNum = Random.Range(1, 101);
        List<FishLoot> possibleItems = new List<FishLoot>();
        foreach (FishLoot item in fishLootList)
        {
            if (randNum <= (item.dropChance + fishingStats.FishingSkill))
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

            //Add fish to the inventory -- Rin
            playerInventory.AddFishLoot(droppedItem.fishLootSprite, droppedItem.sellPrice, droppedItem.fishName,droppedItem.fishId);


            //Vector3 dropDirection = new Vector3(Random.Range(-.5f, .25f), 1f, 0f);
            //fishLootGameObject.GetComponent<Rigidbody2D>().AddForce(dropDirection * dropForce, ForceMode2D.Impulse);

            Destroy(fishLootGameObject, 2);
        }
    }
}
