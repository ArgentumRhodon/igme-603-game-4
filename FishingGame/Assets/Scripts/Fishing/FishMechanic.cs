using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FishMechanic : MonoBehaviour
{
    FishSlider fishSlider;
    [SerializeField] TextMeshProUGUI fishText;
    bool isFishTugging = false;
    float catchProgress = 0f;

    // Random switch variables
    float minSwitchInterval = 0.5f; // Minimum interval between switches
    float maxSwitchInterval = 3f; // Maximum interval between switches
    float switchTimer = 0.0f;
    float switchInterval; // Actual interval between switches

    void Start()
    {
        fishSlider = gameObject.GetComponent<FishSlider>();

        switchInterval = Random.Range(minSwitchInterval, maxSwitchInterval);
        fishText.text = "Wind in the fish!";
    }

    void Update()
    {
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
            if (Input.GetKeyDown(KeyCode.W) && catchProgress < 1)
            {
                catchProgress += 0.05f;
                fishSlider.UpdateSliderValue(catchProgress);
            }
        }
        // Player should wait
        else
        {
            if (Input.GetKeyDown(KeyCode.W) && catchProgress > 0)
            {
                catchProgress -= 0.05f;
                fishSlider.UpdateSliderValue(catchProgress);
            }
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
}
