using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGrid : MonoBehaviour
{
    public TileRow[] rows { get; private set; }
    public TileCell[] cells { get; private set; }
    public int size => cells.Length;
    public int height => rows.Length;
    public int width => size / height;

    private void Awake()
    {
        rows= GetComponentsInChildren<TileRow>();
        cells= GetComponentsInChildren<TileCell>();
    }

    private void Start()
    {
         for(int y = 0; y < rows.Length ; y++)
        {
            for(int x = 0; x < rows[y].cells.Length; x++)
            {
                rows[y].cells[x].coordinates = new Vector2Int(x, y);
            }
        }
    }

    public TileCell GetCell(int x, int y)
    {
        if (x >= 0 && x < width && y >= 0 && y < height)
        {
            return rows[y].cells[x];
        }
        else
        {
            return null;
        }
    }

    public TileCell GetCell(Vector2Int coordinates)
    {
        return GetCell(coordinates.x, coordinates.y);
    }

    public TileCell GetAdjacentCell(TileCell cell, Vector2Int direction)
    {
        Vector2Int coordinates = cell.coordinates;
        coordinates.x += direction.x;
        coordinates.y -= direction.y;

        return GetCell(coordinates);
    }

    public TileCell GetRandomEmptyCell()
    {   
        int index = Random.Range(0,cells.Length);
        int startingIndex = index;      //루프 스타트 지점 정하는 변수

        //자리를 정할 때 까지 한칸씩 앞으로 가면서 루프
        while (cells[index].occupied)   //해당 자리가 차있으면
        {
            index++;

            if(index >= cells.Length)
            {
                index = 0;
            }

            if(index == startingIndex)  //한바퀴 돌게되면 자리가 없는것이므로 null 반환
            {
                return null;
            }
        }
        return cells[index];            //빈자리 반환
    }
}
