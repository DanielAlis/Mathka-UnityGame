using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Text time_test;

    public void DisplayTime(){
        time_test.text = Clock.instance.GetCurrentTimeText().text;
    }
}
