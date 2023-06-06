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

    public TileCell GetRandomEmptyCell()
    {   
        int index = Random.Range(0,cells.Length);
        int startingIndex = index;      //���� ��ŸƮ ���� ���ϴ� ����

        //�ڸ��� ���� �� ���� ��ĭ�� ������ ���鼭 ����
        while (cells[index].occupied)   //�ش� �ڸ��� ��������
        {
            index++;

            if(index >= cells.Length)
            {
                index = 0;
            }

            if(index == startingIndex)  //�ѹ��� ���ԵǸ� �ڸ��� ���°��̹Ƿ� null ��ȯ
            {
                return null;
            }
        }
        return cells[index];            //���ڸ� ��ȯ
    }
}