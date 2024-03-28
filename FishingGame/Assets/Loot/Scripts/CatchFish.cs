using System.Collections;
using UnityEngine;

public class CatchFish : MonoBehaviour
{
    [SerializeField] private GameObject fishPrompt;
    [SerializeField] private GameObject fishingLine;



    void Update()
    {
        if (!GameManager.Instance.isPaused)
        {
            castLine();
            catchFish();
        }
        else
        {
            // Does nothing but stops you from being able to cast and catch while paused
            // Debug.Log("Game is paused");
        }
    }

    void castLine()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            fishingLine.SetActive(true);
            StartCoroutine(promptCoroutine(Random.Range(2f, 9f)));
        }
    }

    void catchFish()
    {
        if(fishPrompt.activeSelf == true) 
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GetComponent<FishLootBag>().InstantiateLoot(transform.position);
                fishingLine.SetActive(false);
                fishPrompt.SetActive(false);
            }
        }
    }

    IEnumerator promptCoroutine(float randNum)
    {
        yield return new WaitForSeconds(randNum);
        fishPrompt.SetActive(true);
    }
}
