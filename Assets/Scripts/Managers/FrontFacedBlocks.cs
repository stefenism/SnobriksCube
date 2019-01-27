using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontFacedBlocks : MonoBehaviour
{
    public BoxCollider collider;
    private Vector3 extents = new Vector3(3,3,.5f);
    List<Block> frontFacingBlocks = new List<Block>();


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("trigger warning");
            findFrontFaceBlocks();
        }
    }
    public List<Block> findFrontFaceBlocks()
    {
        
        Collider[] hitColliders = Physics.OverlapBox(transform.position, extents);
        foreach(Collider c in hitColliders)
        {
            Block block = c.GetComponent<Block>();
            if(block != null && !frontFacingBlocks.Contains(block))
                frontFacingBlocks.Add(block);
        }
        
        //foreach(Block b in frontFacingBlocks)
        //    Debug.Log("b name: " + b.name);    
        return frontFacingBlocks;
    }
}
