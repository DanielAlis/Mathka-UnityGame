using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using System;

public class UserResult : MonoBehaviour
{
    private Text text_user_result;
    private int last_index;
    private  List<int> path_index;

    
    public static UserResult instance;

    private void Awake(){
        if(instance)
        {
            Destroy(instance);
        }
        instance = this;
        text_user_result = GetComponent<Text>();
    }

    void Start()
    {
        var level = GameSettings.instance.GetGameMode();
        var data = MathkaData.instance.mathka_game[level]; 
        if(data.results != null)
        {
            text_user_result.text = "";
        }
        path_index = new List<int>();
    }

    public bool UpdateUserResult(int index,string user_result)
    {
        if(validMovement(index))
        {
            text_user_result.text += " " + user_result;
            last_index = index;
            path_index.Add(index);
            return true;
        }
        else {
            return false;
        } 

    }

    public void ResetUserResult()
    {
        text_user_result.text = "";
        last_index = -1;
        path_index.Clear();
    }

    public void CheckUserResult()
    {
        if((text_user_result.text.Contains("+") || text_user_result.text.Contains("-"))
             && text_user_result.text[0] != '+' && text_user_result.text[0] != '-'
        && text_user_result.text[text_user_result.text.Length-1] != '+' && 
        text_user_result.text[text_user_result.text.Length-1] != '-')
        {
        DataTable dt = new DataTable();
        int answer = (int)dt.Compute(text_user_result.text, "");
        int number;
        bool isParsable = Int32.TryParse(Result.instance.text_result.text, out number);
        if(isParsable && answer == number)
        {
            Result.instance.CorrectAnswer();
        }
        else{
            GameEvents.OnWrongNumberMethod();
        }
        }
        ResetUserResult();
        
    }

    private bool validMovement(int index)
    {

        if(last_index == -1)
        {
            return true;
        }
        else if(path_index.Contains(index)){
            return false;
        }
        else if(index == last_index +3 ||index == last_index - 3){
            return true;
        }
        else if(index == last_index - 1){
            if(last_index % 3 != 0 ){
                return true;
            }
        }else if(index == last_index + 1){
                if((last_index + 1 ) % 3 != 0 ){
                    return true;
            }
        }
        return false;
    }
    
}