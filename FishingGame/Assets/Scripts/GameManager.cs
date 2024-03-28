using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    [SerializeField] GameObject controlsCanvas;
    [HideInInspector] public bool isPaused = false;
    
    public static GameManager Instance
    {
        get 
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();

                if (instance == null)
                {
                    GameObject singletonObject = new GameObject("Game Manager");
                    instance = singletonObject.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    void Start()
    {
        // Set up game manager.
        if (instance == null)
        {
            // Makes gamemanager persistent.
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Prevents duplicate instances of the game manager.
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // Pause / Unpause game when escape is pressed.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }

        if (!isPaused && !GameUI.Instance.IsScreenActive("Start Screen"))
        {
            // Open Inventory -- Rin
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (GameUI.Instance.IsScreenActive("Inventory UI"))
                {
                    CloseInventory();
                }
                else
                {
                    OpenInventory();
                }

            }
        }
    }

    public void Pause()
    {
        // Don't do in start menu
        if (GameUI.Instance.IsScreenActive("Start Screen")) return;

        isPaused =!isPaused;

        // Pause logic
        if (isPaused)
        {
            // Set Pause Screen Active
            GameUI.Instance.SetIsScreenActive("Pause Screen", true);
            controlsCanvas.SetActive(false);
            Time.timeScale = 0f;
        }
        // Unpause logic
        else if (!isPaused)
        {
            // Set all Screen Inactive (Settings, Options, Pause, etc.)
            GameUI.Instance.SetIsScreenActive("Pause Screen", false);
            GameUI.Instance.SetAllScreensActive(false);
            controlsCanvas.SetActive(true);
            Time.timeScale = 1f;
        }
    }

    public void Quit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }


    //Open Inventory -- Rin
    public void OpenInventory()
    {
        GameUI.Instance.SetIsScreenActive("Inventory UI", true);
    }


    //Close Inventory -- Rin
    public void CloseInventory()
    {
        GameUI.Instance.SetIsScreenActive("Inventory UI", false);
    }
}
