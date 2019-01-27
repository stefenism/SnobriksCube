using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    private enum PlayerState
    {
        CONTROLLABLE,
        FROZEN,
        DEAD,
    }

    private PlayerState playerState = PlayerState.CONTROLLABLE;
    private BlockRoom currentRoom;
    private Vector2 spawnPosition;

    void Awake()
    {
        spawnPosition = transform.position;
    } 

    public bool playerIsControllable(){return playerState == PlayerState.CONTROLLABLE;}
    public bool playerIsFrozen(){return playerState == PlayerState.FROZEN;}
    public bool playerIsDead(){return playerState == PlayerState.DEAD;}

    public void setPlayerFrozen(){playerState = PlayerState.FROZEN;}
    public void setPlayerControllable(){playerState = PlayerState.CONTROLLABLE;}

    public void setCurrentRoom(BlockRoom newRoom){currentRoom = newRoom;}
    public BlockRoom getCurrentRoom(){return currentRoom;}

    public Block getCurrentBlock()
    {
        Block currentBlock = GameManager.gameDaddy.blockCollision.getBlockWithRoom(currentRoom);
        return currentBlock;
    }
}
