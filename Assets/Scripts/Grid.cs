using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using UnityEngine.Events;

public class Grid
{
    private int width;
    private int height;
    private float cellSize;
    private Vector3 originPosition;
    private int[,] gridArray;
    private TextMesh[,] debugTextArray;
    public UnityEvent ValueChanged;

    public Grid(int width, int height, float cellSize, Vector3 originPosition)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        gridArray = new int[width, height];
        //debugTextArray = new TextMesh[width,height];
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                //debugTextArray[x,y]= UtilsClass.CreateWorldText(gridArray[x,y].ToString(),null,GetWorldPosition(x,y)+new Vector3(cellSize,cellSize)*.5f,20,Color.black, TextAnchor.MiddleCenter);
                //Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y+ 1), Color.black, 100f);
                //Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x+1, y), Color.black, 100f);
            }
        }
        //Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height),Color.black,100f);
        //Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height),Color.black, 100f);

    }
    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize+originPosition; 
    }
    public void SetValue(int x, int y, int value)
    {
        if(x>=0&& y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = value;
            //debugTextArray[x, y].text = gridArray[x, y].ToString();

        }
    }
    public void SetValue(Vector3 worldPosition, int value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetValue(x, y, value);
    }
    public int GetValue(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            //Debug.Log(gridArray[x, y]);
            return gridArray[x, y];
        }
        return -1;
    }
    public int GetValue(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            //Debug.Log(gridArray[x, y]);
            return gridArray[x, y];
        }
        return -1;
    }
    public Vector3 GetPosition(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * .5f;
    }
    public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
    }
}
