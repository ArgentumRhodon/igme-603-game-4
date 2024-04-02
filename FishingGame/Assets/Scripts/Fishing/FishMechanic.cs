using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Security.Cryptography;

public class FishMechanic : MonoBehaviour
{
    [SerializeField] GameObject catchPrompt;
    [SerializeField] GameObject frenzyPrompt;
    [SerializeField] GameObject escapePrompt;
    [SerializeField] GameObject fishCatchingPrompt;
    [SerializeField] GameObject fishingLine;
    [SerializeField] FishSlider fishSlider;
    [SerializeField] FishingStats fishingStats;
    [SerializeField] TextMeshProUGUI fishText;

    bool isFishTugging = false;
    bool isFrenzyMode = false;
    bool isTugofWar = false;
    bool isCanCast = true;
    
    float catchProgress = 0f;
    float escapeTimer = 0f;

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

            if (isFrenzyMode)
            {
                frenzyMode();
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

        // Fish is caught 
        if (catchProgress >= 1)
        {
            fishSlider.SetFillColor(FillColor.Success);
            fishText.text = "Success!";

            fishingLine.SetActive(false);
            catchPrompt.SetActive(false);

            StartCoroutine(sliderWait());

            isTugofWar = false;

            GetComponent<FishLootBag>().InstantiateLoot(transform.position);
        }
    }

    void frenzyMode()
    {
        // Activates frenzy mode while prompt is active
        frenzyPrompt.SetActive(true);

        if (Input.GetKeyDown(KeyCode.Space) && frenzyPrompt.activeSelf == true)
        {
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
        // Casts line and wait for a bite
        if (Input.GetKeyDown(KeyCode.W))
        {
            isCanCast = false;
            fishingLine.SetActive(true);
            StartCoroutine(fishBite(Random.Range(2f, 9f * (1 - fishingStats.FishingSkill/100))));
        }
    }

    void catchFish()
    {
        float chance = 0;

        if (catchPrompt.activeSelf == true) // checks to see if pormopt is active
        {
            escapeTimer += .05f; // increments the timer before the fish escapes
            if (Input.GetKeyDown(KeyCode.Space) && escapeTimer <= 50)
            {
                catchPrompt.SetActive(false);
                chance = Random.Range(1, 101); // calcuates chance out of 100%

                if (chance <= 5 + fishingStats.PercentFrenzyBoost) //Chance of getting frenzymode
                {
                    isFrenzyMode = true;
                    StartCoroutine(frenzyStart(3));
                }
                else
                {
                    isTugofWar = true;
                }
            }
            if (escapeTimer > 50)
            {
                StartCoroutine(fishEscape());
            }
        }
    }

    IEnumerator fishBite(float randNum)
    {
        yield return new WaitForSeconds(randNum);
        catchPrompt.SetActive(true);
    }

    IEnumerator fishEscape()
    {
        catchPrompt.SetActive(false);
        escapePrompt.SetActive(true);
        yield return new WaitForSeconds(1.6f);
        fishingLine.SetActive(false);
        escapePrompt.SetActive(false);
        isCanCast = true;
        escapeTimer = 0;
    }

    IEnumerator frenzyStart(float timeLength)
    {
        yield return new WaitForSeconds(timeLength);

        fishingLine.SetActive(false);
        frenzyPrompt.SetActive(false);
        isFrenzyMode = false;
        isCanCast = true;
    }

    IEnumerator sliderWait()
    {
        yield return new WaitForSeconds(1.6f);
        fishCatchingPrompt.SetActive(false);
        catchProgress = 0.0f;
        fishSlider.ResetSlider();
        fishText.text = "Wind in the fish!";
        isCanCast = true;
    }
}
