using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Data;




public class MathkaEasyData : MonoBehaviour
{
    public static MathkaData.MathkaBoardData getData(){
        MathkaData.MathkaBoardData data = new MathkaData.MathkaBoardData(1,12);
        return data;
        }
}

public class MathkaMediumData : MonoBehaviour
{
    
    public static MathkaData.MathkaBoardData getData(){
        MathkaData.MathkaBoardData data = new MathkaData.MathkaBoardData(13,29);
        return data;
        }
}

public class MathkaHardData : MonoBehaviour
{
    public static MathkaData.MathkaBoardData getData(){
        MathkaData.MathkaBoardData data = new MathkaData.MathkaBoardData(3,100);
        return data;
        }
}

public class MathkaVeryHardData : MonoBehaviour
{
    public static MathkaData.MathkaBoardData getData(){
        MathkaData.MathkaBoardData data = new MathkaData.MathkaBoardData(100,350);
        return data;
        }
}


public class MathkaData : MonoBehaviour
{
    public static MathkaData instance;
    public struct MathkaBoardData
    {
        public string[] board;
        public List<int> results;
        public static  int num_of_results;
        private int MoveRight(int n){
            if((n + 1) % 3 == 0){
                return -1; 
            }
            else{
                return n + 1;
            }
        }
        private int MoveLeft(int n){
            if(n % 3 == 0){
                return -1; 
            }
            else{
                return n - 1;
            }
        }
        private int MoveUp(int n){
            return n+3;
        }
        private int MoveDown(int n){
            return n-3;
        }
        private int SumOfPaths(int n,string str,string visit_index)
        {
            if( n < 0 || n >=9 || visit_index.Contains(n.ToString()))
            {
                return 0;
            }
            else{
                str += board[n];
                visit_index += n;

            }
            if((str.Contains("+") || str.Contains("-")) && str[str.Length-1] != '+' && str[str.Length-1] != '-' )
            {
                DataTable dt = new DataTable();
                int answer = (int)dt.Compute(str, "");
                if(answer >=0 && !results.Contains(answer))
                {
                    results.Add(answer);
                    return 1; 
                }
            }

            
            return 
            SumOfPaths(MoveDown(n),str,visit_index) +
            SumOfPaths(MoveUp(n),str,visit_index) +
            SumOfPaths(MoveLeft(n),str ,visit_index) + 
            SumOfPaths(MoveRight(n),str,visit_index);
    }
        public MathkaBoardData(int min, int max) : this()
        {
            var array_of_number = new int[5];
            string[] array_of_operators = {"+","+","-","-"};
            board = new string[9];
            results = new List<int>();
            num_of_results = 0;
            
            var rnd = new System.Random();
            var list_of_numbers =  Enumerable.Range(min, max).OrderBy(x => rnd.Next()).Take(5).ToList();

            rnd = new System.Random();
            array_of_operators = array_of_operators.OrderBy(x => rnd.Next()).ToArray();

            for (int i = 0 ; i < 9 ; i++ )
            {
                if(i % 2 == 0)
                {
                    board[i] = list_of_numbers.First().ToString();
                    list_of_numbers.RemoveAt(0);
                }
                else{
                    board[i] = array_of_operators[i/2];
                }
            }
            for(int i = 0 ; i < 9 ; i = i+2)
            {
                num_of_results+=SumOfPaths(i,"","");
            }
            results = results.Where(x => x >=0).ToList();
            results = results.Distinct().ToList();
            rnd = new System.Random();
            results = results.OrderBy(x => rnd.Next()).Take(10).ToList();
            num_of_results = results.Count;
        }
    };

    public Dictionary<string,MathkaBoardData> mathka_game;

    void Awake(){
        if(instance == null)
        {
            Debug.LogError("new instance");
            instance = this;
            initGame();
        }
        else{
            Debug.LogError("reusing existing instance");
            Destroy(this);
        }
    }

    void Start()
    {
        
    }

    public void initGame() {
        mathka_game = new Dictionary<string,MathkaBoardData>();
        mathka_game.Add("Easy",MathkaEasyData.getData());
        mathka_game.Add("Medium",MathkaMediumData.getData());
        mathka_game.Add("Hard",MathkaHardData.getData());
        mathka_game.Add("VeryHard",MathkaVeryHardData.getData());
    }

}
