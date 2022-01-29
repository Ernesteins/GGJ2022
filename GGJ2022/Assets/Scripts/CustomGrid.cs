using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GridItemType
{
    empty, obstacle, trap, guardian
}

[CreateAssetMenu(fileName = "CustomGrid", menuName = "GGJ2022/CustomGrid")]
public class CustomGrid : ScriptableObject
{
    [SerializeField] float _cellSize = 2;
    [SerializeField] int _cols = 10;
    [SerializeField] int _rows = 10;

    public int cols { get => _cols;}
    public int rows { get => _rows;}
    public float cellSize { get => _cellSize;}
    Vector3 gridPosition;
    GridItemType[,] items;
    public void SetUp(Vector3 pos){
        gridPosition = pos;
        items = new GridItemType[_cols,_rows];
    }
    public void AddToGrid(GridItem item){
        Vector2Int gridPos = GetGridPoint(item.transform.position);
        if(isValid(gridPos)){
            Debug.Log("Added"+item.gameObject.name);
            items[gridPos.x,gridPos.y] = item.Type;
        }
    }
    public void AddToGrid(Vector3 position, GridItemType type){
        Vector2Int gridPos = GetGridPoint(position);
        if(isValid(gridPos)){
            items[gridPos.x,gridPos.y] = type;
        }
    }
    public GridItemType GetItemAt(Vector2Int gridPos){
        return isValid(gridPos)? items[gridPos.x,gridPos.y] : GridItemType.obstacle;
    }
    public Vector3 SnapToGrid(Vector3 worldPos)
    {
        return GetWoldPosition(GetGridPoint(worldPos));
    }
    public bool isValidPoint(Vector3 pos)
    {
        Vector2Int gridPos = GetGridPoint(pos);
        return GetItemAt(gridPos) == GridItemType.empty ;
    }
    bool isValid(Vector2Int gridPos) =>  gridPos.x>=0 && gridPos.x < _cols && gridPos.y>=0 && gridPos.y < _rows;
     Vector2Int GetGridPoint(Vector3 position){
        position -= gridPosition;
        int x = Mathf.RoundToInt(position.x / cellSize);
        int y = Mathf.RoundToInt(position.z / cellSize);
        return new Vector2Int(x,y);
    }
    public Vector3 GetWoldPosition(Vector2Int gridPos){
        Vector3 position = new Vector3(
            (float) gridPos.x * cellSize,
            0,
            (float) gridPos.y * cellSize
        );
        position += gridPosition;
        return position;
    }
}
