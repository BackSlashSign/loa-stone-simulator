using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    
    public Vector2 Pos => transform.position;   // get 축약형 대괄호 안써도됨
    public Block OccupiedBlock;
}
