using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

   public void setEasyGame()
    {
        GameSettings.instance.SetGameMode(GameSettings.EGameMode.EASY);
    }

    public void setMediumGame()
    {
        GameSettings.instance.SetGameMode(GameSettings.EGameMode.MEDIUM);
    }

    public void setHardGame()
    {
        GameSettings.instance.SetGameMode(GameSettings.EGameMode.HARD);
    }

    public void setVeryHardGame()
    {
        GameSettings.instance.SetGameMode(GameSettings.EGameMode.VERYHARD);
    }

    public void ActivateObject(GameObject obj)
    {
        obj.SetActive(true);
    }

    public void DeActivateObject(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void SetPaused(bool paused){
        GameSettings.instance.SetPaused(paused);
    }

    
    public void loadGameScene() {
        LoadScene("GameScene");
    }
}
