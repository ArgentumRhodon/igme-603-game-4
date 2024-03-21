using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    private static GameUI instance;
    [SerializeField] private List<GameObject> UIScreens = new List<GameObject>();

    public static GameUI Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameUI>();

                if (instance == null)
                {
                    Debug.LogWarning("No instance of GameUI found in the scene.");
                }
            }
            return instance;
        }
    }

    // Returns a boolean whether the screen is active
    public bool IsScreenActive(string screenName)
    {
        foreach (GameObject screen in UIScreens)
        {
            if (screen.name == screenName)
            {
                return screen.activeSelf;
            }
        }

        Debug.Log("GameUI.cs: Screen " + screenName + " not found!");
        return false;
    }

    // Set a screen to active or inactive
    public void SetIsScreenActive(string screenName, bool isActive)
    {
        foreach (GameObject screen in UIScreens)
        {
            if (screen.name == screenName)
            {
                screen.SetActive(isActive);
                return;
            }
        }

        Debug.Log("GameUI.cs: Screen " + screenName + " not found!");
    }

    // Set all screens to active or inactive
    public void SetAllScreensActive(bool isActive)
    {
        foreach (GameObject screen in UIScreens)
        {
            screen.SetActive(isActive);
        }
    }

    // Pause / Unpause game. -- For resume button
    public void Pause()
    {
        GameManager.Instance.Pause();
    }

    // Exits the game. -- For quit button
    public void Quit()
    {
        GameManager.Instance.Quit();
    }
}
