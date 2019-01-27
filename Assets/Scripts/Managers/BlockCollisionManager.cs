using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCollisionManager : MonoBehaviour
{
    List<Block> blockList = new List<Block>();
    public LayerMask playerCollisionLayer;
    public LayerMask defaultCollisionLayer;

    private FrontFacedBlocks frontFaced;
    void Awake()
    {
        /* Block[] blocks = FindObjectsOfType<Block>();// GetComponents<Block>();
        Debug.Log("Blocks: " + blocks);
        Debug.Log("Blocks.count: " + blocks.Length);
        foreach(Block b in blocks)
        {
            blockList.Add(b);
        } */
        Initialize();
        frontFaced = GetComponentInChildren<FrontFacedBlocks>();
    }

    public void Initialize()
    {
        
        Block[] blocks = FindObjectsOfType<Block>();// GetComponents<Block>();
        Debug.Log("Blocks: " + blocks);
        Debug.Log("Blocks.count: " + blocks.Length);
        foreach(Block b in blocks)
        {
            blockList.Add(b);
        }
        frontFaced = GetComponentInChildren<FrontFacedBlocks>();

        setBlockCollision();
    }

    public void setBlockCollision()
    {
        List<Block> blocks = frontFaced.findFrontFaceBlocks();
        foreach(Block b in blockList)
        {
            if(blocks.Contains(b))
                b.setPhysicsLayer(playerCollisionLayer.value);
            else
                b.setPhysicsLayer(defaultCollisionLayer.value);
        }
        
    }

    public Block getBlockWithRoom(BlockRoom room)
    {
        foreach(Block b in blockList)
        {
            if(b.GetRooms().Contains(room));
                return b;
        }
        return null;
    }




}
