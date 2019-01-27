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

    void Awake()
    {
        BlockRoom[] blockRooms = FindObjectsOfType<BlockRoom>();
        foreach(BlockRoom r in blockRooms)
            rooms.Add(r);
    }

    public void setPhysicsLayer(LayerMask physicsLayer)
    {
        foreach(BlockRoom r in rooms)
            for(int i = 0; i < r.gameObject.transform.childCount; i++)
            {
                r.gameObject.transform.GetChild(i).gameObject.layer = physicsLayer.value;
            }
    }

    public List<BlockRoom> GetRooms(){return rooms;}
}
