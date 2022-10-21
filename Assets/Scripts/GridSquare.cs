using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GridSquare : Selectable,IPointerUpHandler,IPointerEnterHandler ,IPointerExitHandler
{
    public GameObject number_text;
    public GameObject square_image;

    private string path_result = "";
    

    private bool selected_square = false;
    private int square_index = -1;

    public bool isSelected() {return selected_square;}
    public void setSquareIndex(int index)
    {
        square_index = index;
    }
    void Start()
    {
        selected_square = false;
    }

    public void SetNumber(string num)
    {
        number_text.GetComponent<Text>().text = num;
    }
    public void ActiveColor()
    {
        square_image.GetComponent<Image>().color = new Color32(29,226,243,255);
    }
    public void DeActiveColor()
    {
        square_image.GetComponent<Image>().color = new Color32(255,255,255,255);
    }
        

public void OnPointerEnter (PointerEventData eventData) {
        selected_square = true;
        GameEvents.SquareSelectedMethod(square_index);
        var level = GameSettings.instance.GetGameMode();
        var data = MathkaData.instance.mathka_game[level]; 
        if(UserResult.instance.UpdateUserResult(square_index,data.board[square_index])){
        GetComponent<GridSquare>().ActiveColor();
        }

    }

    public void OnPointerUp(PointerEventData pointerEventData){
        UserResult.instance.CheckUserResult();
        for(int index = 0 ; index < MathkaGrid.instance.grid_squares.Count; index++)
        {
            MathkaGrid.instance.grid_squares[index].GetComponent<GridSquare>().DeActiveColor();
        }
    }

    private void OnEnable(){
        GameEvents.OnUpdateSquareNumber += OnSetNumber;
        GameEvents.OnSquareSelected += OnSquareSelected;
    }
    private void OnDisable(){
        GameEvents.OnUpdateSquareNumber -= OnSetNumber;
        GameEvents.OnSquareSelected -= OnSquareSelected;
    }

    public void OnSetNumber(int number){
        if(selected_square){
            var level = GameSettings.instance.GetGameMode();
            var data = MathkaData.instance.mathka_game[level]; 
            path_result += data.board[square_index];

        }
    }
    public void OnSquareSelected(int index){
        if(square_index != index){
            selected_square = false;
        }
    }
}
