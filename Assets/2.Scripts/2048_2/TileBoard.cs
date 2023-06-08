using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBoard : MonoBehaviour
{
    public GameManager_ gameManager;
    public Tile tilePrefab;
    public TileState[] tileStates;

    private TileGrid grid;
    private List<Tile> tiles;
    private bool waiting;


    private void Awake()
    {
        grid = GetComponentInChildren<TileGrid>();
        tiles = new List<Tile>(16);
    }

    public void ClearBoard()
    {
        foreach(var cell in grid.cells)
        {
            cell.tile = null;
        }

        foreach(var tile in tiles)
        {
            Destroy(tile.gameObject);
        }
        tiles.Clear();
    }
    public void CreateTile()
    {
        Tile tile = Instantiate(tilePrefab, grid.transform);
        tile.SetState(tileStates[0], 2);
        tile.Spawn(grid.GetRandomEmptyCell());
        tiles.Add(tile);
    }

    private void Update()
    {
        if (!waiting)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                MoveTiles(Vector2Int.up, 0, 1, 1, 1);
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveTiles(Vector2Int.left, 1, 1, 0, 1);
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                MoveTiles(Vector2Int.down, 0, 1, grid.height - 2, -1);
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveTiles(Vector2Int.right, grid.width - 2, -1, 0, 1);
            }
        }
    }

    private void MoveTiles(Vector2Int direction, int startX, int incrementX, int startY, int incrementY)
    {
        bool changed = false;
        for(int x = startX; x >= 0 && x < grid.width; x += incrementX)
        {
            for(int y = startY; y >= 0 && y < grid.height; y += incrementY)
            {
                TileCell cell = grid.GetCell(x, y);

                if(cell.occupied)
                {
                    changed |= MoveTile(cell.tile, direction);      //changed 와 MoveTile의 반환값을 or 연산 한뒤 대입
                }
            }
        }

        if (changed)
        {
            StartCoroutine(WaitForChanges());
        }
    }

    private bool MoveTile(Tile tile, Vector2Int direction)
    {
        TileCell newCell = null;
        TileCell adjacent = grid.GetAdjacentCell(tile.cell, direction);   //해당 인접 방향의 좌표값 반환

        while(adjacent!= null)      //반환된 좌표에 타일이 존재 할 때
        { 
            if(adjacent.occupied)   //타일이 점유된 상태이면
            {
                if (CanMerge(tile, adjacent.tile))  //병합이 가능하면 (숫자가 같을때)
                {
                    Merge(tile, adjacent.tile);     //병합
                    return true;
                }
                break;
            }

            newCell = adjacent;
            adjacent = grid.GetAdjacentCell(adjacent, direction);       //해당 방향으로 새로운 좌표 반환
        }
        if(newCell != null)          
        {
            tile.MoveTo(newCell);   //새로운 칸으로 이동
            return true;
        }
        return false;
    }

    private bool CanMerge(Tile a, Tile b)
    {   //(두 타일 숫자가 같고 b.locked 가 false 이면) 참 아니면 거짓
        return a.number == b.number && !b.locked;    
    }

    private void Merge(Tile a, Tile b)
    {
        tiles.Remove(a);
        a.Merge(b.cell);

        int index = Mathf.Clamp(IndexOf(b.state) + 1,0,tileStates.Length -1 );
        int number = b.number * 2;

        b.SetState(tileStates[index],number);

        gameManager.InCreaseScore(number);
    }

    public int IndexOf(TileState state)
    {
        for(int i = 0; i < tileStates.Length; i++) 
        {
            if(state == tileStates[i])
            {
                return i;
            }
        }
        return -1;
    }
    private IEnumerator WaitForChanges()
    {
        waiting = true;

        yield return new WaitForSeconds(0.1f);

        waiting = false;

        foreach (var tile in tiles)
        {
            tile.locked = false;
        }

        if (tiles.Count != grid.size)
        {
            CreateTile();
        }
        if (CheckForGameOver())
        {
            gameManager.GameOver();
        }
    }

    private bool CheckForGameOver()
    {
        if(tiles.Count != grid.size)
        {
            return false;
        }

        foreach(var tile in tiles) 
        {
            TileCell up = grid.GetAdjacentCell(tile.cell, Vector2Int.up);
            TileCell down = grid.GetAdjacentCell(tile.cell, Vector2Int.down);
            TileCell left = grid.GetAdjacentCell(tile.cell, Vector2Int.left);
            TileCell right = grid.GetAdjacentCell(tile.cell, Vector2Int.right);

            if (up != null && CanMerge(tile, up.tile))
            {
                return false;
            }

            if (down != null && CanMerge(tile, down.tile))
            {
                return false;
            }

            if (left != null && CanMerge(tile, left.tile))
            {
                return false;
            }

            if (right != null && CanMerge(tile, right.tile))
            {
                return false;
            }
        }
        return true;
    }
}

