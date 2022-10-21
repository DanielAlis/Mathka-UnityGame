using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Clock : MonoBehaviour
{
    private int hour = 0;
    private int min = 0;
    private int sec = 0;

    private Text text_clock;
    private float delta_time;
    private bool stop_clock = false;

    public static Clock instance;

    private void Awake(){
        if(instance)
        {
            Destroy(instance);
        }
        instance = this;
        text_clock = GetComponent<Text>();
        delta_time = 0;
    }

    void Start()
    {
        Debug.Log("start clock");
        stop_clock = false;
    }


    void Update()
    {
      if (GameSettings.instance != null && GameSettings.instance.GetPaused() == false && stop_clock == false)
      {
        delta_time += Time.deltaTime;
        TimeSpan span = TimeSpan.FromSeconds(delta_time);

        string hour = LeadingZero(span.Hours);
        string min = LeadingZero(span.Minutes);
        string sec = LeadingZero(span.Seconds);

        text_clock.text = hour + ":" + min + ":" + sec;
      }   
    }

    private string LeadingZero(int n)
    {
        return n.ToString().PadLeft(2,'0');
    }

    public void OnGameOver(){
        stop_clock = true;
    }

    private void OnEnable(){
        GameEvents.OnGameOver += OnGameOver;
    }
    private void OnDisable(){
        GameEvents.OnGameOver -= OnGameOver;
    }
    public Text GetCurrentTimeText(){
        return text_clock;
    }

}
