using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public enum EGameMode
    {
        NOT_SET,
        EASY,
        MEDIUM,
        HARD,
        VERYHARD
    }

    public static GameSettings instance;

    private void Awake(){
        Debug.Log("awake game settings");
        paused = false;
        if (instance == null)
        {
            DontDestroyOnLoad(this);
            instance = this;
        }
        else{
            Destroy(this);
        }
    }

    private EGameMode gameMode;
    private bool paused = false;

    public void SetPaused(bool set_paused) {paused = set_paused;}
    public bool GetPaused() {return paused;}

    void Start()
    {
        gameMode = EGameMode.NOT_SET;
    }
    public void SetGameMode(EGameMode mode)
    {
        gameMode = mode;
    }

    public void SetGameMode(string mode)
    {
        if(mode == "Easy")
        {
            SetGameMode(EGameMode.EASY);
        }
        else if(mode == "Medium")
        {
            SetGameMode(EGameMode.MEDIUM);
        }
        else if(mode == "Hard")
        {
            SetGameMode(EGameMode.HARD);
        }
        else if(mode == "VeryHard")
        {
            SetGameMode(EGameMode.VERYHARD);
        }
        else{
            SetGameMode(EGameMode.NOT_SET);
        }
    }

    public string GetGameMode(){
        switch(gameMode){
            case EGameMode.EASY: return "Easy";
            case EGameMode.MEDIUM: return "Medium";
            case EGameMode.HARD: return "Hard";
            case EGameMode.VERYHARD: return "VeryHard";
        }
        Debug.LogError("ERROR: Game Level is not set");
        return " ";
    }
}
