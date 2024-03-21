using System.Collections;
using UnityEngine;

public class CatchFish : MonoBehaviour
{
    [SerializeField] private GameObject fishPrompt;
    [SerializeField] private GameObject fishingLine;
    [SerializeField] private GameObject controlCanvas;

    private void Start()
    {
        controlCanvas.SetActive(true);
    }

    void Update()
    {
        castLine();
        catchFish();
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
