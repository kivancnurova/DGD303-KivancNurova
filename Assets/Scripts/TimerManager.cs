using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class TimerManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public int maxMinutes;
    public float elapsedTime;
    private bool isTimerRunning;


    void Start()
    {
        isTimerRunning = true;
    }

    
    void Update()
    {
        if (isTimerRunning)
        {
            elapsedTime += Time.deltaTime;
            
            int minutes = Mathf.FloorToInt(elapsedTime / 60f);
            int seconds = Mathf.FloorToInt(elapsedTime % 60f);

            timerText.text = $"{minutes:00} : {seconds:00}";

            if (minutes >= maxMinutes)
            {
                isTimerRunning = false;
                OnTimerEnd();
            }
        }

        void OnTimerEnd()
        {
            Debug.Log("Timer has ended!");
        }
    }

    public float GetElapsedTime()
    {
        return elapsedTime;
    }
}
