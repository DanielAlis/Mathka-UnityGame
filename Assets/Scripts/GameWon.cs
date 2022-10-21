using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameWon : MonoBehaviour
{
    public GameObject win_pop_up;
    public Text current_time;

    void Start()
    {
        win_pop_up.SetActive(false);
        current_time.text = Clock.instance.GetCurrentTimeText().text;
    }

    private void OnBoardCompleted(){
        win_pop_up.SetActive(true);
    }

    private void OnEnable(){
        GameEvents.OnBoardCompleted += OnBoardCompleted;
    }
    private void OnDisable(){
        GameEvents.OnBoardCompleted -= OnBoardCompleted;
    }


}
