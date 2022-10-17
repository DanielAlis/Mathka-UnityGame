using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MathkaGrid : MonoBehaviour
{
    public static MathkaGrid instance;
    public int columns = 0;
    public int rows = 0;
    public float square_offset = 0.0f;
    public GameObject grid_square;
    public Vector2 start_position = new Vector2(0.0f,0.0f);
    public float square_scale = 1.0f;
    public float square_gap = 0.1f;

    public List<GameObject> grid_squares = new List<GameObject>();
    private int selected_grid_data = -1;
    void Start()
    {
        if(grid_square.GetComponent<GridSquare>() == null)
        {
            Debug.LogError("This Game Object need to have GridSquare script attached !");
        }
        CreateGrid();
        SetGridNumber(GameSettings.instance.GetGameMode());
    }
    void Awake(){
        if(instance == null)
        {
            instance = this;
        }
        else{
            Destroy(this);
        }
    }

    private void CreateGrid(){
        SpawnGridSquares();
        SetSquarePosition();
    }

    private void SpawnGridSquares(){
        //0, 1 ,2 
        //3 , 4, 5
        //6, 7 , 8
        int square_index = 0;
        for(int row = 0 ; row < rows ; row++)
        {
           for(int column = 0 ; column <  columns; column++)
           {
                grid_squares.Add(Instantiate(grid_square) as GameObject);
                grid_squares[grid_squares.Count -1].GetComponent<GridSquare>().setSquareIndex(square_index);
                grid_squares[grid_squares.Count -1].transform.SetParent(this.transform); // instantiate this game object as a child of the object holding this script
                grid_squares[grid_squares.Count -1].transform.localScale = new Vector3(square_scale,square_scale,square_scale);
            
                square_index++;
            }
        }
    }

    private void SetSquarePosition(){
        var square_rect = grid_squares[0].GetComponent<RectTransform>();
        Vector2 offset = new Vector2();
        Vector2 square_gap_number = new Vector2(0.0f,0.0f);
        bool row_moved = false;
        offset.x = square_rect.rect.width * square_rect.transform.localScale.x + square_offset;
        offset.y = square_rect.rect.height * square_rect.transform.localScale.y + square_offset;

        int column_number = 0;
        int row_number = 0;

        foreach (GameObject square in grid_squares)
        {
            if(column_number + 1 > columns)
            {
                row_number++;
                column_number = 0;
                square_gap_number.x=0;
                row_moved = false;
            }
            var pos_x_offset = offset.x * column_number + (square_gap_number.x * square_gap);
            var pos_y_offset = offset.y * row_number + (square_gap_number.y * square_gap);

            if(column_number > 0)
            {
                square_gap_number.x++;
                pos_x_offset += square_gap;
            }

            if(row_number > 0 &&  row_moved == false)
            {
                row_moved = true;
                square_gap_number.y++;
                pos_y_offset += square_gap;
            }

            square.GetComponent<RectTransform>().anchoredPosition = new Vector2(start_position.x + pos_x_offset, start_position.y - pos_y_offset);
            column_number++;
        }
    }
    private void SetGridNumber(string level){

        var data = MathkaData.instance.mathka_game[level];
        
        SetGridSquareData(data);

    }
    private void SetGridSquareData(MathkaData.MathkaBoardData data){
        for(int index = 0 ; index < grid_squares.Count; index++)
        {
            grid_squares[index].GetComponent<GridSquare>().SetNumber(data.board[index]);
        }
    }

}
