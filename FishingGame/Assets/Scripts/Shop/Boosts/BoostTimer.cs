using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BoostTimer : MonoBehaviour
{
    public static BoostTimer Instance { get; private set; }

    public GameObject boostUI;
    private int timeLeft = 0;
    private IEnumerator timerCoroutine;

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
        boostUI.SetActive(true);
        timeLeft = boost.duration * 60;

        timerCoroutine = DoCountdownUI();
        StartCoroutine(timerCoroutine);
    }

    public void StopTimer()
    {
        boostUI.SetActive(false);
        StopCoroutine(timerCoroutine);
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
        timeLeft -= 1;
        boostUI.GetComponentInChildren<TextMeshProUGUI>().text = FromSeconds(timeLeft);
    }

    IEnumerator DoCountdownUI()
    {
        for (; ; )
        {
            UpdateBoostUI();
            yield return new WaitForSeconds(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
