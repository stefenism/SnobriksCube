using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCollisionManager : MonoBehaviour
{
    List<Block> blockList = new List<Block>();
    public LayerMask playerCollisionLayer;
    public LayerMask defaultCollisionLayer;

    private string playerCollisionLayerName = "Ground";
    private string defaultCollisionLayerName = "BackGround";

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
            if(!blockList.Contains(b))
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
            {
                List<BlockRoom> rooms = b.GetRooms();
                foreach(BlockRoom r in rooms)
                {
                    r.gameObject.layer = LayerMask.NameToLayer(playerCollisionLayerName); 
                    r.tiles.gameObject.layer = LayerMask.NameToLayer(playerCollisionLayerName);
                }
            }
                
                // LayerMask.NameToLayer(playerCollisionLayer.getMask());
            else
            {
                List<BlockRoom> rooms = b.GetRooms();
                foreach(BlockRoom r in rooms)
                {
                    r.gameObject.layer = LayerMask.NameToLayer(defaultCollisionLayerName); 
                    r.tiles.gameObject.layer = LayerMask.NameToLayer(defaultCollisionLayerName);
                }
            }
                //LayerMask.LayerToName(defaultCollisionLayer.value);
        }
        
        Debug.Log("default collision layer name: " + LayerMask.NameToLayer(defaultCollisionLayerName));
        Debug.Log("player collision layer name: " + LayerMask.NameToLayer(playerCollisionLayerName));
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
