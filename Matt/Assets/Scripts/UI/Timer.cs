﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public Text TimerText;
    public float minutes, seconds;

    // Start is called before the first frame update
    void Start()
    {
        TimerText = GetComponent<Text>() as Text;
    }

    // Update is called once per frame
    void Update()
    {
        minutes = (int)(Time.time / 60f);
        seconds = (int)(Time.time % 60f);
        TimerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}
