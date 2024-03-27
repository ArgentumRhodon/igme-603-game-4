using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FishMechanic : MonoBehaviour
{
    [SerializeField] GameObject fishPrompt;
    [SerializeField] GameObject fishCatchingPrompt;
    [SerializeField] GameObject fishingLine;
    [SerializeField] FishSlider fishSlider;
    [SerializeField] TextMeshProUGUI fishText;
    bool isFishTugging = false;
    bool isTugofWar = false;
    bool isCanCast = true;
    float catchProgress = 0f;

    // Random switch variables
    float minSwitchInterval = 0.5f; // Minimum interval between switches
    float maxSwitchInterval = 3f; // Maximum interval between switches
    float switchTimer = 0.0f;
    float switchInterval; // Actual interval between switches

    void Start()
    {
        switchInterval = Random.Range(minSwitchInterval, maxSwitchInterval);
        fishText.text = "Wind in the fish!";
    }

    void Update()
    {
        if (!GameManager.Instance.isPaused)
        {
            if (isCanCast)
            {
                castLine();
            }

            catchFish();

            if (isTugofWar)
            {
                TugofWar();
            }
        }
    }

    private void TugofWar()
    {
        fishCatchingPrompt.SetActive(true);

        // Increment switch timer
        switchTimer += Time.deltaTime;

        // Check if it's time to switch the fish tugging status
        if (switchTimer >= switchInterval)
        {
            isFishTugging = !isFishTugging;
            SwitchText();

            // Reset the timer & calculate new interval
            switchTimer = 0.0f;
            switchInterval = Random.Range(minSwitchInterval, maxSwitchInterval);
        }

        // Player winds the fish in
        if (!isFishTugging)
        {
            if (Input.GetKeyDown(KeyCode.Space) && catchProgress < 1)
            {
                catchProgress += 0.05f;
                fishSlider.UpdateSliderValue(catchProgress);
                fishSlider.SetFillColor(FillColor.Normal);
            }
        }
        // Player should wait
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && catchProgress > 0)
            {
                catchProgress -= 0.05f;
                fishSlider.UpdateSliderValue(catchProgress);
                fishSlider.SetFillColor(FillColor.Bad);
            }
        }

        if (catchProgress >= 1)
        {
            fishingLine.SetActive(false);
            fishPrompt.SetActive(false);


            StartCoroutine(sliderWait());


            isTugofWar = false;
            isCanCast = true;

            catchProgress = 0.0f;
            fishSlider.ResetSlider();
            GetComponent<FishLootBag>().InstantiateLoot(transform.position);
        }
    }

    private void SwitchText()
    {
        if (isFishTugging)
        {
            fishText.text = "Stop! It's tugging.";
        }
        else
        {
            fishText.text = "Wind in the fish!";
        }
    }

    void castLine()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            isCanCast = false;
            fishingLine.SetActive(true);
            StartCoroutine(promptCoroutine(Random.Range(2f, 9f)));
        }
    }

    void catchFish()
    {
        if (fishPrompt.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                fishPrompt.SetActive(false);
                isTugofWar = true;
            }
        }
    }

    IEnumerator promptCoroutine(float randNum)
    {
        yield return new WaitForSeconds(randNum);
        fishPrompt.SetActive(true);
    }

    IEnumerator sliderWait()
    {
        yield return new WaitForSeconds(2);
        fishCatchingPrompt.SetActive(false);
    }
}
