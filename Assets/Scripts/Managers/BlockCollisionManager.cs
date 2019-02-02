using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCollisionManager : MonoBehaviour
{
    List<Block> blockList = new List<Block>();
    public LayerMask playerCollisionLayer;
    public LayerMask defaultCollisionLayer;

    private string playerCollisionLayerName = "Ground";
    private string defaultCollisionLayerName = "Default";
    private string backGroundCollisionLayerName = "BackGround";

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
        Debug.Log("block collision is being set");
        List<Block> blocks = frontFaced.findFrontFaceBlocks();

        foreach(Block b in blockList)
        {            
            List<BlockRoom> rooms = b.GetRooms();
            if(blocks.Contains(b))
            {
                if(b == GameManager.gameDaddy.iglooBlock)
                    GameManager.gameDaddy.setIglooHouseCollisionLayer(defaultCollisionLayerName);

                foreach(BlockRoom r in rooms)
                {
                    r.gameObject.layer = LayerMask.NameToLayer(defaultCollisionLayerName); 
                    r.tiles.gameObject.layer = LayerMask.NameToLayer(playerCollisionLayerName);
                }
            }
                
                // LayerMask.NameToLayer(playerCollisionLayer.getMask());
            else
            {         
                if(b == GameManager.gameDaddy.iglooBlock)
                    GameManager.gameDaddy.setIglooHouseCollisionLayer(backGroundCollisionLayerName);
                           
                foreach(BlockRoom r in rooms)
                {
                    r.gameObject.layer = LayerMask.NameToLayer(backGroundCollisionLayerName); 
                    r.tiles.gameObject.layer = LayerMask.NameToLayer(backGroundCollisionLayerName);
                }
            }
                //LayerMask.LayerToName(defaultCollisionLayer.value);
        }
    }

    public Block getBlockWithRoom(BlockRoom playerCurrentRoom)
    {
        foreach(Block b in blockList)
        {
            List<BlockRoom> rooms = b.GetRooms();
            
            if(rooms.Contains(playerCurrentRoom))
                return b;
        }
        return null;
    }




}
