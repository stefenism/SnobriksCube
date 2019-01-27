using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public enum BlockType
    {
        ONE,
        TWO,
        THREE,
    }

    public BlockType blockType = BlockType.ONE;

    private List<BlockRoom> rooms = new List<BlockRoom>();
}
