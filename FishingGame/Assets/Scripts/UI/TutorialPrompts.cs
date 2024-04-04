using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPrompts : MonoBehaviour
{
    [SerializeField] GameObject castLinePrompt;
    [SerializeField] GameObject reelFishPrompt;
    [SerializeField] GameObject catchPrompt;

    void Start()
    {
        // Start with the cast line prompt active & other prompts insactive
        castLinePrompt.SetActive(true);
        reelFishPrompt.SetActive(false);
    }

    void Update()
    {
        // Prompt on how to cast the line
        if (!GameUI.Instance.IsScreenActive("Start Screen") && !GameManager.Instance.isPaused && castLinePrompt.activeSelf && Input.GetKeyDown(KeyCode.W))
        {
            castLinePrompt.SetActive(false);
            reelFishPrompt.SetActive(true);
        } 

        // Prompt on how to start fishing minigame
        if (reelFishPrompt.activeSelf && catchPrompt.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            reelFishPrompt.SetActive(false);
        }
    }
}
