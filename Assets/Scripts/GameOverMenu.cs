using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    public Text text_clock;

    void Start()
    {
        text_clock.text = Clock.instance.GetCurrentTimeText().text;
    }

}
