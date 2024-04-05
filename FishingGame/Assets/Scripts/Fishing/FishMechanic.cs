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
    bool isCanEscape = false;
    
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

        // Constant tugging. Can me modifyed to tug harder or softer randomly or for specific fish
        if (catchProgress > 0)
        {
            catchProgress -= 0.00005f;
            fishSlider.UpdateSliderValue(catchProgress);
        }

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
            fishText.color = new Color(0.2725308f, 0.9339623f, 0.2702029f); // Green
            fishSlider.SetFillColor(FillColor.Normal);

            if (Input.GetKeyDown(KeyCode.Space) && catchProgress < 1)
            {
                SoundManager.Instance.StartLoopingSound(1);
                catchProgress += 0.05f;
                fishSlider.UpdateSliderValue(catchProgress);
            }
        }
        // Player should wait
        else
        {
            SoundManager.Instance.StopLoopingSound();

            fishText.color = new Color(0.9333333f, 0.03921568f, 0.1011488f); // Red
            fishSlider.SetFillColor(FillColor.Bad);

            if (Input.GetKeyDown(KeyCode.Space) && catchProgress > 0)
            {
                catchProgress -= 0.025f;
                fishSlider.UpdateSliderValue(catchProgress);
            }
        }

        // Fish is caught 
        if (catchProgress >= 1)
        {
            fishText.color = new Color(0.972549f, 0.9355273f, 0f); // Yellow
            fishSlider.SetFillColor(FillColor.Success);
            fishText.text = "FISH CAUGHT!";

            SoundManager.Instance.StopLoopingSound(); // Stop reeling/thrashing sound
            SoundManager.Instance.PlaySound(2); // Success sound
        
            fishingLine.SetActive(false);
            catchPrompt.SetActive(false);

            StartCoroutine(sliderWait());

            isTugofWar = false;

            GetComponent<FishLootBag>().InstantiateLoot(transform.position);
        }

        // This code and below will allow the fish to escape if the bar goes back down to ~0.
        // only issue is when starting the next cast after the fish is lost. the color and state of the fish text is wrong. FIX LATER
        if (catchProgress >= .1f)
        {
            isCanEscape = true;
            Debug.Log("Can now escape");
        }

        if (catchProgress <= 0.001 && isCanEscape == true)
        {
            fishText.color = new Color(0.7830189f, 0.1231161f, 0.1758734f); // Red
            fishSlider.SetFillColor(FillColor.Bad);
            fishText.text = "FISH ESCAPED!";

            SoundManager.Instance.PlayRandomSound(new int[] {3, 4});

            fishingLine.SetActive(false);
            catchPrompt.SetActive(false);

            StartCoroutine(sliderWait());

            isTugofWar = false;
            Debug.Log("Fish Escaped");
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
            fishText.text = "STOP! It's tugging!";
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
            SoundManager.Instance.PlaySound(0);
            StartCoroutine(fishBite(Random.Range(2f, 9f)));
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
        SoundManager.Instance.PlaySound(6);
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
        SoundManager.Instance.PlayMusic(0);

        yield return new WaitForSeconds(timeLength);

        SoundManager.Instance.StopMusic();

        fishingLine.SetActive(false);
        frenzyPrompt.SetActive(false);
        isFrenzyMode = false;
        isCanCast = true;
    }

    IEnumerator sliderWait()
    {
        if (catchProgress <= 0.001 && isCanEscape == true)
        {
            yield return new WaitForSeconds(1.6f);
            fishCatchingPrompt.SetActive(false);
            catchProgress = 0.0f;
            fishSlider.ResetSlider();
            fishText.text = "Wind in the fish!";
            isCanEscape = false;
            isCanCast = true;
        }
        else
        {
            yield return new WaitForSeconds(1.6f);
            fishCatchingPrompt.SetActive(false);
            catchProgress = 0.0f;
            fishSlider.ResetSlider();
            fishText.text = "Wind in the fish!";
            isCanCast = true;
        }
    }
}
