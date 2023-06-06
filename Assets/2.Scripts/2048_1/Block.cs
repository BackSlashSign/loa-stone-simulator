using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int Value;
    public Node Node;
    public Block MergingBlock;
    public bool Merging;
    public Vector2 Pos => transform.position;
    

    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private TextMeshPro _text;
    public void Init(BlockType type)
    {
        Value = type.Value;
        _renderer.color = type.Color;
        _text.text = type.Value.ToString();
    }

    public void SetBlock(Node node)
    {
        if(Node != null)
        {
            Node.OccupiedBlock = null;
        }
        Node = node;
        Node.OccupiedBlock = this;
    }

    public void MergeBlock(Block blockToMergeWith)
    {
        //set the block we are merging with
        MergingBlock = blockToMergeWith;

        //set current node as unoccupied to allow blocks to use it
        Node.OccupiedBlock = null;

        //set the base block as merging, so it does no get used twice
        blockToMergeWith.Merging = true;
    }

    public bool CanMerge(int value) => value == Value && !Merging && MergingBlock == null;
}
