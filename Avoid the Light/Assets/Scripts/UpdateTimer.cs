using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading;

public class UpdateTimer : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    private float seconds = 0;
    private float minutes = 0;
    private float hours = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Initial timer text
        textMeshPro.text = "00:00:00";
    }

    // Update is called once per frame
    void Update()
    {
        RunTimer();
    }

    public void RunTimer()
    {
        // Timer
        seconds += Time.deltaTime;

        // Converting seconds to minutes and hours
        if (seconds >= 60)
        {
            minutes += 1;
            seconds = 0;
        }
        if (minutes >= 60)
        {
            hours += 1;
            minutes = 0;
        }

        // String variables
        string secondsStr = "0";
        string minutesStr = "0";
        string hoursStr = "0";

        // Formatting seconds
        if (((int)seconds) < 10)
        {
            secondsStr = "0" + ((int)seconds).ToString();
        }
        else
        {
            secondsStr = ((int)seconds).ToString();
        }

        // Formatting minutes
        if (minutes < 10)
        {
            minutesStr = "0" + minutes.ToString();
        }
        else
        {
            minutesStr = minutes.ToString();
        }

        // Formatting hours
        if (hours < 10)
        {
            hoursStr = "0" + hours.ToString();
        }
        else
        {
            hoursStr = hours.ToString();
        }

        // Update timer text
        textMeshPro.text = hoursStr + ":" + minutesStr + ":" + secondsStr;
    }
}
