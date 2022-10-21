using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Data;

public class Result : MonoBehaviour
{
    public Text text_result;
    private Animator anim;

    
    public static Result instance;

    private void Awake(){
        if(instance)
        {
            Destroy(instance);
        }
        instance = this;
        text_result = GetComponent<Text>();
    }

    void Start()
    {
        var level = GameSettings.instance.GetGameMode();
        var data = MathkaData.instance.mathka_game[level]; 

        if(data.results != null)
        {
            text_result.text = data.results.First().ToString();
            anim = text_result.GetComponent<Animator>();
        }
       
        
    }


    public void CorrectAnswer()
    {
        var level = GameSettings.instance.GetGameMode();
        if(MathkaData.instance.mathka_game[level].results.Count > 0)
        {
            if(MathkaData.instance.mathka_game[level].results.Count > 1)
            {
                MathkaData.instance.mathka_game[level].results.RemoveAt(0); 
                anim.Play("ScaleUp",-1,0);
                text_result.text = MathkaData.instance.mathka_game[level].results.First().ToString();
            }
            else{
                text_result.text = "You Win Ya Malic";
                GameEvents.OnBoardCompletedMethod();
            }
        }
    }
}

