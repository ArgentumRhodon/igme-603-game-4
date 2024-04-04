using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BoostTimer : MonoBehaviour
{
    public static BoostTimer Instance { get; private set; }
    public static Boost currentBoost = null;

    public GameObject boostUI;
    private float timeLeft = 0;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void StartTimer(Boost boost)
    {
        if(currentBoost != null)
        {
            CancelBoost();
        }

        currentBoost = boost;

        boostUI.SetActive(true);
        timeLeft = currentBoost.duration * 60;

        BuffPlayer();
    }

    public void AddTime(float time)
    {
        timeLeft += time * 60;
    }

    public void StopTimer()
    {
        boostUI.SetActive(false);
    }

    // https://forums.codeguru.com/showthread.php?356471-Display-Seconds-
    private string FromSeconds(int numOfSeconds)
    {
        int hours = (int)(numOfSeconds / 3600);
        int minutes = (int)(numOfSeconds / 60) % 60;
        int seconds = (int)numOfSeconds % 60;

        string time = "";
        if(hours > 0)
        {
            if(hours <= 9) { time += "0";  }
            time += hours.ToString() + ":";
        }

        if(minutes > 0)
        {
            if(minutes <= 9) { time += "0"; }
            time += minutes.ToString() + ":";
        }
        else
        {
            time += "00:";
        }

        time += seconds > 9 ? seconds.ToString() : "0"+seconds.ToString();

        return time;
    }

    private void UpdateBoostUI()
    {
        int seconds = Mathf.RoundToInt(timeLeft);
        boostUI.GetComponentInChildren<TextMeshProUGUI>().text = FromSeconds(seconds);
    }

    public void BuffPlayer()
    {
        FishingStats stats = FindFirstObjectByType<FishingStats>();

        switch (currentBoost.type)
        {
            case BoostType.Frenzy:
                stats.frenzyBoost = currentBoost.boostSize;
                break;
            case BoostType.Fishing:
                stats.fishingBoost = currentBoost.boostSize;
                break;
        }
    }

    public void CancelBoost()
    {
        StopTimer();

        FishingStats stats = FindFirstObjectByType<FishingStats>();
        switch (currentBoost.type)
        {
            case BoostType.Frenzy:
                stats.frenzyBoost = 0;
                break;
            case BoostType.Fishing:
                stats.fishingBoost = 0;
                break;
        }
        currentBoost = null;
    }

    private void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        }
        else if(currentBoost != null)
        {
            CancelBoost();
        }

        if (boostUI.activeSelf)
        {
            UpdateBoostUI();
        }
    }
}
